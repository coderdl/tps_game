using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    class playerProperty
    {
        public string position { set; get; }
        public string rotation { set; get; }
        public int projectile { set; get; }
        public int blood { set; get; }
    }

    [System.Serializable]
    public class MouseInput {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    [SerializeField] float speed;
    [SerializeField] MouseInput MouseController;

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }

    void Start()
    {
        GameManager.Instance.SocketController.bindServer("39.97.50.170", 8080);
        if (GameManager.LoadGameFlag)
        {
            loadPlayer();
        }
    }

    private Crosshair m_Crosshair;
    public Crosshair Crosshair
    {
        get
        {
            if (m_Crosshair == null)
                m_Crosshair = GetComponentInChildren<Crosshair>();
            return m_Crosshair;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    void Awake()
    {
        print("player");
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseController.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseController.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseController.Sensitivity.x);

        Crosshair.LookHeight(mouseInput.y * MouseController.Sensitivity.y);

        sendPlayerMsg(GameManager.PlayerId, GameManager.GameId);        
    }

    public void sendPlayerMsg(string playerId, string gameId)
    {
        string Msg = "{\"type\":\"foundationMsg\",\"playerId\":\"" + playerId + "\",\"position\":\"" +
            "" + transform.position.ToString() + "\",\"rotation\":\"" + transform.rotation.ToString() + "\",\"gameId\":\"" + GameManager.GameId + "\"}";
        GameManager.Instance.SocketController.sendData(Msg);
    }

    public void loadPlayer()
    {
        string playerMsg = GameManager.Instance.SocketController.loadPlayer();
        string RegexStr = @"'\w+': u*'[()\.\- \w,]+'";
        foreach (Match match in Regex.Matches(playerMsg, RegexStr))
        {
            print("match succeed");
            string[] values = match.Value.Split(':');
            if (values[0] == "'blood'")
            {
                Health h = transform.GetComponent<Health>();
                h.damageTaken = 5 - int.Parse(values[1].Trim().Substring(1, values[1].Trim().Length - 2));
            }
            else if (values[0] == "'projectile'")
            {
                WeaponReloader wr = transform.GetComponentInChildren<WeaponReloader>();
                wr.shotsFiredInClip = wr.clipSize - int.Parse(values[1].Trim().Substring(1, values[1].Trim().Length - 2));
            }
            else if (values[0] == "'position'")
            {
                print(float.Parse("-0.1"));
                string[] positionMsg = values[1].Trim().Substring(3, values[1].Trim().Length - 5).Split(',');
                Vector3 pos = new Vector3(float.Parse(positionMsg[0].Trim()), float.Parse(positionMsg[1].Trim()), float.Parse(positionMsg[2].Trim()));
                transform.position = pos;
            }
            else if(values[0] == "'rotation'")
            {
                string[] rotationMsg = values[1].Trim().Substring(3, values[1].Trim().Length - 5).Split(',');
                Quaternion qua = new Quaternion(float.Parse(rotationMsg[0].Trim()), float.Parse(rotationMsg[1].Trim()),
                    float.Parse(rotationMsg[2].Trim()), float.Parse(rotationMsg[3].Trim()));
                transform.rotation = qua;
            }
        }

        print(playerMsg);
    }
}

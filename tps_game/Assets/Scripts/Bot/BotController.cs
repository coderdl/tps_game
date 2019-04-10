using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BotController : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float damping;
    [SerializeField] public float rotspeed;
    [SerializeField] public float attackLagTime;
    [SerializeField] Projectile projectile;
    public string botId;
    float nextFireAllowed;

    private void Start()
    {
        if (GameManager.LoadGameFlag)
        {
            GameManager.Instance.SocketController.bindServer("39.97.50.170", 8080);
            loadBot();
        }
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, player.position, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);

        string Msg = "{\"type\":\"foundationMsg\",\"playerId\":\"Bot" + botId + "\",\"gameId\":" + GameManager.GameId + ",\"position\":\"" + transform.position.ToString() + 
            "\",\"rotation\":\"" + transform.rotation.ToString() + "\"}";
        GameManager.Instance.SocketController.sendData(Msg);

        Fire();
    }

    void Fire()
    {
        Transform muzzle = transform.Find("Mesh/Muzzle");
        if (Time.time < nextFireAllowed)
        {
            return;
        }
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        nextFireAllowed = Time.time + attackLagTime;
    }


    public void loadBot()
    {
        string playerMsg = GameManager.Instance.SocketController.loadPlayer();
        string RegexStr = @"'\w+': u*'[()\.\- \w,]+'";
        foreach (Match match in Regex.Matches(playerMsg, RegexStr))
        {
            string[] values = match.Value.Split(':');
            if (values[0] == "'position'")
            {
                string[] positionMsg = values[1].Trim().Substring(3, values[1].Trim().Length - 5).Split(',');
                Vector3 pos = new Vector3(float.Parse(positionMsg[0].Trim()), float.Parse(positionMsg[1].Trim()), float.Parse(positionMsg[2].Trim()));
                transform.position = pos;
            }
            else if (values[0] == "'rotation'")
            {
                string[] rotationMsg = values[1].Trim().Substring(3, values[1].Trim().Length - 5).Split(',');
                Quaternion qua = new Quaternion(float.Parse(rotationMsg[0].Trim()), float.Parse(rotationMsg[1].Trim()),
                    float.Parse(rotationMsg[2].Trim()), float.Parse(rotationMsg[3].Trim()));
                transform.rotation = qua;
            }
        }
    }
}
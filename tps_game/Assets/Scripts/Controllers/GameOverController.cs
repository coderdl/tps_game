using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject btnObj = GameObject.Find("Canvas/Button");
        Button btn = (Button)btnObj.GetComponent<Button>();

        btn.onClick.AddListener(onClick);
    }
    void onClick()
    {
        GameManager.Instance = null;
        GameManager.Instance.SocketController.bindServer("127.0.0.1", 8080);
        GameManager.GameId = GameManager.Instance.SocketController.newGame();
        GameManager.LoadGameFlag = false;
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject newGameBtn = GameObject.Find("Canvas/NewGame");
        Button newGame = (Button)newGameBtn.GetComponent<Button>();
        newGame.onClick.AddListener(newGameOnclick);

        GameObject loadGameBtn = GameObject.Find("Canvas/LoadGame");
        Button loadGame = (Button)newGameBtn.GetComponent<Button>();
        loadGame.onClick.AddListener(loadGameOnclick);
    }

    void newGameOnclick()
    {
        GameManager.Instance.SocketController.bindServer("39.97.50.170", 8080);
        GameManager.GameId = GameManager.Instance.SocketController.newGame();
        GameManager.LoadGameFlag = false;
        GameManager.Instance = null;
        print("load");
        SceneManager.LoadScene("SampleScene");
    }

    public void loadGameOnclick()
    {
        GameManager.Instance.SocketController.bindServer("39.97.50.170", 8080);
        if (GameManager.Instance.SocketController.canLoadGame())
        {
            GameManager.LoadGameFlag = true;
            GameManager.Instance = null;
            SceneManager.LoadScene("SampleScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

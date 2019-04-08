
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoginController : MonoBehaviour
{
    InputField userName;
    private InputField passWord;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject btnObj = GameObject.Find("Canvas/Button");
        Button btn = (Button)btnObj.GetComponent<Button>();
      
        btn.onClick.AddListener(onClick);

    }

    void onClick()
    {
        userName = (InputField)GameObject.Find("Canvas/UserName").GetComponent<InputField>();
        passWord = (InputField)GameObject.Find("Canvas/PassWord").GetComponent<InputField>();
        GameManager.Instance.SocketController.bindServer("127.0.0.1", 8080);
        if (GameManager.Instance.SocketController.login(userName.text, passWord.text))
        {
            GameManager.PlayerId = userName.text;
            GameManager.Instance = null;
            SceneManager.LoadScene("modeScene");
        }
        else
            return;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

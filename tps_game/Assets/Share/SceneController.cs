using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
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
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

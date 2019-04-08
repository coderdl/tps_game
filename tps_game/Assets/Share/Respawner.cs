
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void Despawn(GameObject obj, float inSeconds)
    {
        obj.SetActive(false);
        GameManager.Instance.Timer.Add(() =>
        {
            obj.SetActive(true);
        }, inSeconds);
    }


    void ActivateGO()
    {

    }
}

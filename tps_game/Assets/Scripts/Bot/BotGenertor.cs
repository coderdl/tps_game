using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGenertor : MonoBehaviour
{
    [SerializeField] public Transform[] caves;
    [SerializeField] public float lagTime;
    [SerializeField] public GameObject bot;
    [SerializeField] public Transform player;

    bool canGenerate;

    // Start is called before the first frame update
    void Start()
    {
        print("bot");
        int indexCave = new System.Random().Next(0, caves.Length);
        Transform monsterCave = caves[indexCave];
        monsterGenerate(monsterCave);
    }

    // Update is called once per frame
    void Update()
    {
        if (canGenerate)
        {
            canGenerate = false;
            int indexCave = new System.Random().Next(0, caves.Length);
            Transform monsterCave = caves[indexCave];
            GameManager.Instance.Timer.Add(() =>
            {
                monsterGenerate(monsterCave);
            }, lagTime);
        }

    }

    private void monsterGenerate(Transform cave)
    {
        bot.GetComponent<BotController>().player = player;
        Transform monster = cave.Find("Mesh");
        GameObject x = Instantiate(bot, monster.position, monster.rotation) as GameObject;
        GameManager.Instance.BotCount += 1;
        x.GetComponent<BotController>().botId = GameManager.Instance.BotCount.ToString();
        GameManager.Instance.SocketController.sendData("{\"type\":\"bot\",\"playerId\":\"bot" + GameManager.Instance.BotCount.ToString() + 
            "\",\"gameId\":"+ GameManager.GameId + "}");
        canGenerate = true;
    }
}

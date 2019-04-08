using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : Destructale
{

    [SerializeField] float inSeconds;
    public override void Die()
    {
        if (transform.tag == "Player")
        {
            GameManager.Instance.SocketController.sendData("{\"type\":\"die\", \"playerId\":\"" + GameManager.PlayerId + "\",\"gameId\":" + GameManager.GameId + "}");
        }
        else
        {
            BotController bot = transform.parent.gameObject.GetComponent<BotController>();
            GameManager.Instance.SocketController.sendData("{\"type\":\"die\", \"playerId\":\"bot" + bot.botId + "\",\"gameId\":" + GameManager.GameId + "}");
        }
        base.Die();
        // GameManager.Instance = null;
        SceneManager.LoadScene("gameOverScene");
    }

    public override void TakeDamage(float amount)
    {
        if (transform.tag == "Player")
        {
            GameManager.Instance.SocketController.sendData("{type:damage, playerId:" + GameManager.PlayerId + ",gameId:" + GameManager.GameId + "}");
            GameManager.Instance.SocketController.sendData("{\"type\":\"takeDamage\", \"playerId\":\"" + GameManager.PlayerId + "\",\"gameId\":" + GameManager.GameId + "}");
        }
        else
        {
            BotController bot = transform.parent.gameObject.GetComponent<BotController>();
            GameManager.Instance.SocketController.sendData("{\"type\":\"takeDamage\", \"playerId\":\"bot" + bot.botId + "\",\"gameId\":" + GameManager.GameId + "}");
        }
        base.TakeDamage(amount);
        if (HitPointsRemaing <= 0)
            Die();
    }

    private void OnEnable()
    {
        Reset();
    }
}

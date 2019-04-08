using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public event System.Action<Player> OnLocalPlayerJoined;
    private GameObject gameObject;


    private static bool loadGameFlag;
    public static bool LoadGameFlag
    {
        get
        {
            return loadGameFlag;
        }
        set
        {
            loadGameFlag = value;
        }
    }

    private static string playerId;
    public static string PlayerId
    {
        get
        {
            return playerId;
        }
        set
        {
            playerId = value;
        }
    }

    private static string gameId;
    public static string GameId
    {
        get
        {
            return gameId;
        }
        set
        {
            gameId = value;
        }
    }

    private int botCount;
    public int BotCount
    {
        get
        {
            return botCount;
        }
        set
        {
            botCount = value;
        }
    }

    private SocketController m_SocketController;
    public SocketController SocketController
    {
        get
        {
            if (m_SocketController == null)
            {
                m_SocketController = gameObject.GetComponent<SocketController>();
            }
            return m_SocketController;
        }
    }

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<InputController>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respawner>();
                m_Instance.gameObject.AddComponent<SocketController>();
                
            }
            return m_Instance;
        }
        set
        {
            m_Instance = value;
        }
    }

    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private Respawner m_Respawner;
    public Respawner Respawner
    {
        get
        {
            if(m_Respawner == null)
            {
                m_Respawner = gameObject.GetComponent<Respawner>();
            }
            return m_Respawner;
        }
    }

    private InputController m_InputController;
    public InputController InputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();
            }
            return m_InputController;
        }
    }

    private Player m_LocalPlayer;
    public Player LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value;
            if(OnLocalPlayerJoined != null)
            {
                OnLocalPlayerJoined(m_LocalPlayer);
            }
        }
    }
}

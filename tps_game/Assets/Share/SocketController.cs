using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;


public class SocketController : MonoBehaviour
{
    Socket clientSocket;

    public void bindServer(string ip, int port)
    {
        IPAddress address = IPAddress.Parse(ip);
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(new IPEndPoint(address, port));
    }

    public void sendData(string data)
    {
        clientSocket.Send(System.Text.Encoding.ASCII.GetBytes(data));
    }



    public bool login(string userName, string passWord)
    {
        string loginMsg = "{\"type\":\"login\",\"userName\":\"" + userName + "\",\"passWord\":\"" + passWord + "\"}";
        sendData(loginMsg);
        string recStr = "";
        byte[] recBytes = new byte[4096];
        int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
        recStr += System.Text.Encoding.ASCII.GetString(recBytes, 0, bytes);
        if (recStr == "0")
            return true;
        else
            return false;
    }

    public string loadPlayer()
    {
        string msg = "{\"type\":\"loadPlayer\",\"playerId\":\"" + GameManager.PlayerId + "\"}";
        sendData(msg);
        string recStr = "";
        byte[] recBytes = new byte[4096];
        int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
        recStr += System.Text.Encoding.ASCII.GetString(recBytes, 0, bytes);
        return recStr;
    }

    public string newGame()
    {
        string loginMsg = "{\"type\":\"newGame\",\"playerId\":\"" + GameManager.PlayerId + "\"}";
        sendData(loginMsg);
        string recStr = "";
        byte[] recBytes = new byte[4096];
        int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
        recStr += System.Text.Encoding.ASCII.GetString(recBytes, 0, bytes);
        return recStr;
    }

    public bool canLoadGame()
    {
        string loginMsg = "{\"type\":\"canLoadGame\",\"playerId\":\"" + GameManager.PlayerId + "\"}";
        sendData(loginMsg);
        string recStr = "";
        byte[] recBytes = new byte[4096];
        int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
        recStr += System.Text.Encoding.ASCII.GetString(recBytes, 0, bytes);
        if (recStr == "0")
            return true;
        else
            return false;
    }
}

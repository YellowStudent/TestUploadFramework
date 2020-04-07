using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class Server : NetBase {
    private Socket serverSocket;
    private List<Client> clientList = new List<Client>();
    Client tempClient;
    private void Awake()
    {
        Bind(NetEvent.ALL_REQUEST,
            NetEvent.SINGLE_REQUEST);
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        serverSocket.Bind(iPEndPoint);
        serverSocket.Listen(0);
        serverSocket.BeginAccept(AcceptCallBack, null);
        DontDestroyOnLoad(gameObject);
    }
    public override void Execute(int eventCode, params object[] message)
    {
        switch (eventCode)
        {
            case NetEvent.ALL_REQUEST:
                RequestAllClient((int)message[0], (int)message[1], message.Length == 3 ? message[2].ToString() : "");
                break;
            case NetEvent.SINGLE_REQUEST:
                RequestClinet((Direction)message[0], (int)message[1], (int)message[2], message.Length == 4 ? message[3].ToString() : "");
                break;
        }
    }
    /// <summary>
    /// 接受响应（接受客户端信息）
    /// </summary>
    /// <param name="ar"></param>
    private void AcceptCallBack(IAsyncResult ar)
    {
        Socket clientSocket = serverSocket.EndAccept(ar);
        Client client = new Client(clientSocket, this);
        client.Start();
        clientList.Add(client);
        serverSocket.BeginAccept(AcceptCallBack, null);
    }
    /// <summary>
    /// 移除客户端
    /// </summary>
    /// <param name="client"></param>
    public void RemoveClient(Client client)
    {
        lock (clientList)
        {
            Debug.Log("移除一位攻击玩家");
            clientList.Remove(client);
        }
    }
    /// <summary>
    /// 发送反馈
    /// </summary>
    /// <param name="client"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    public void SendResponse(Client client, int areaCode, int eventCode, string data="")
    {
        client.Send(areaCode,eventCode,data);
    }
    /// <summary>
    /// 获取客户端
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Client GetClient(Direction direction)
    {
        return clientList.Find((o) =>
       {
           return o.direction == direction;
       });
    }
    /// <summary>
    /// 请求所有客户端
    /// </summary>
    /// <param name="areaCode"></param>
    /// <param name="eventCode"></param>
    /// <param name="data"></param>
    public void RequestAllClient(int areaCode,int eventCode,string data="")
    {
        for (int i = 0; i < clientList.Count; i++)
        {
            SendResponse(clientList[i], areaCode, eventCode, data);
        }
    }
    /// <summary>
    /// 请求客户端
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="areaCode"></param>
    /// <param name="eventCode"></param>
    /// <param name="data"></param>
    public void RequestClinet(Direction dir, int areaCode, int eventCode, string data = "")
    {
        Client temp = GetClient(dir);
        SendResponse(temp, areaCode, eventCode, data);
    }
    private void ShootingResponse(Direction direction)
    {
        tempClient=GetClient(direction);

    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            serverSocket.Close();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("无法关闭和服务器端链接" + ex);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
public class Client  {

    public Direction direction = Direction.None;
    private Socket clientSocket;
    private Server server;
    private Message msg;
    public Client(Socket client,Server sever)
    {
        clientSocket = client;
        this.server = sever;
        msg = new Message();
        Debug.Log("攻击玩家连上主机");
    }
    internal void Start()
    {
        if (server != null && clientSocket != null)
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
    }
    /// <summary>
    /// 收到回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            int count = clientSocket.EndReceive(ar);
            if (count == 0)//断开连接
            {
                Close();
            }
            //ToDo处理接受到数据
            msg.ReadMessage(count, OnProcessMessage);//Message类进行消息解析，然后发给Client的处理信息方法进行后续处理
            Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Close();
        }
    }
    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    public void OnProcessMessage(int areaCode, int eventCode, string data)
    {
        //Debug.Log("接收到数据:" + data);
        ThreadHelper.Instance.ExecuteOnMainThread(() =>
        {
            if (direction == Direction.None && areaCode == AreaCode.NET && eventCode == NetEvent.REGISTER_CLIENT)
            {
                this.direction = JsonUtility.FromJson<NetPackingData>(data).direction;
                Debug.Log(direction);
                return;
            }
            MsgCenter.Instance.Dispatch(areaCode, eventCode, data);
        });
    }
    /// <summary>
    /// 发送给客户端
    /// </summary>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    public void Send(int areaCode, int eventCode, string data = "")
    {
        try
        {
            Debug.Log("准备发送数据" + areaCode + "--" + eventCode + "--" + data);
            byte[] bytes = Message.PackData(areaCode, eventCode, data);
            clientSocket.Send(bytes);
            Debug.Log("发送成功");
        }
        catch (Exception ex)
        {
            Console.WriteLine("无法发送消息" + ex);
        }
    }
    private void Close()
    {
        if (clientSocket != null)
            clientSocket.Close();
        server.RemoveClient(this);

    }
}

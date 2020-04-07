using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
public class Message  {
    private byte[] data = new byte[1024];
    /// <summary>我们存取了多少个字节的数据的在数组里</summary>
    private int startIndex = 0;

    //public void AddCount(int count)
    //{
    //    startIndex += count;
    //}
    public byte[] Data
    {
        get
        {
            return data;
        }
    }
    public int StartIndex
    {
        get
        {
            return startIndex;
        }
    }
    public int RemainSize
    {
        get
        {
            return data.Length - startIndex;
        }
    }
    /// <summary>
    /// 解析数据
    /// </summary>
    /// <param name="newDataAmount"></param>
    /// <param name="processDataCallBack">处理回调信息</param>
    //public void ReadMessage(int newDataAmount, Action<ActionCode , string> processDataCallBack)
    //{
    //    startIndex += newDataAmount;//数据起点+=当前数据长度
    //    while (true)
    //    {
    //        if (startIndex <= 4)//如果说小于4，说明显示字节长度数据都没到位，退出等下个数据合并后处理
    //            return;
    //        int count = BitConverter.ToInt32(data, 0);//返回转换的字节数组中指定位置处的四个字节从 32 位有符号的整数（因为只读取4个字节，所以有开始就够了）。1、真实数据总长度
    //        if ((startIndex - 4) >= count)//总数据长度- 4（这4是记录这个数据的长度）如果数据不全退出
    //        {
    //            //string s = Encoding.UTF8.GetString(data, 4, count);
    //            //Console.WriteLine(count + ":" + s);
    //            ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);//2、请求代码块
    //            string s = Encoding.UTF8.GetString(data, 8, count - 4);//4、请求数据
    //            processDataCallBack(actionCode, s);//执行回调方法
    //            Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);//Count+4表示该数据总长度，复制数组，从哪里开始复制，粘贴数据，从哪里开始粘贴
    //            startIndex -= (count + 4);
    //        }
    //        else
    //            return;
    //    }
    //}
    public void ReadMessage(int newDataAmount, Action<int,int,string> processDataCallBack)
    {
        startIndex += newDataAmount;//数据起点+=当前数据长度
        while (true)
        {
            if (startIndex <= 4)//如果说小于4，说明显示字节长度数据都没到位，退出等下个数据合并后处理
                return;
            int count = BitConverter.ToInt32(data, 0);//返回转换的字节数组中指定位置处的四个字节从 32 位有符号的整数（因为只读取4个字节，所以有开始就够了）。1、真实数据总长度
            if ((startIndex - 4) >= count)//总数据长度- 4（这4是记录这个数据的长度）如果数据不全退出
            {
                int areaCodeBts = BitConverter.ToInt32(data, 4);//2、请求AreaCode
                int eventCodeBts= BitConverter.ToInt32(data, 8);//3、请求EventCode
                string s = Encoding.UTF8.GetString(data, 12, count - 8);//4、请求数据
                processDataCallBack(areaCodeBts, eventCodeBts, s);//执行回调方法
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);//Count+4表示该数据总长度，复制数组，从哪里开始复制，粘贴数据，从哪里开始粘贴
                startIndex -= (count + 4);
            }
            else
                return;
        }
    }
    /// <summary>
    /// 打包数据
    /// </summary>
    /// <param name="actionData"></param>
    /// <param name="data"></param>
    //public static byte[] PackData(ActionCode actionData, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)actionData);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    //return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();//组拼数据长度
    //    byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();
    //    return newBytes.Concat(dataBytes).ToArray<byte>();
    //}
    //public static byte[] PackData(RequestCode requestData,ActionCode actionCode, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)requestData);
    //    byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length+actionCodeBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    //return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();//组拼数据长度

    //    //byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();
    //    //return newBytes.Concat(dataBytes).ToArray<byte>();
    //    return dataAmountBytes.Concat(requestCodeBytes).ToArray().Concat(actionCodeBytes).ToArray().Concat(dataBytes).ToArray();
    //}
    public static byte[] PackData(int areaCode, int eventCode, string data)
    {
        //数据字节
        byte[] areaCodeBts = BitConverter.GetBytes(areaCode);
        byte[] eventCodeBts = BitConverter.GetBytes(eventCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //计算总长度,拼接发送
        int dataAmount = areaCodeBts.Length + eventCodeBts.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(areaCodeBts).ToArray().Concat(eventCodeBts).ToArray().Concat(dataBytes).ToArray();
    }
}

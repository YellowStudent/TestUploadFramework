using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 首先是扩展MonoBehaviour的基类：MonoBase 
/// 此类的就是为了让MonoBehaviour的功能更加强大
/// </summary>
public class MonoBase : MonoBehaviour {

    /// <summary>
    /// 定义一个虚方法
    /// </summary>
    public virtual void Execute(int eventCode,params object[] message)
    {
    }
}

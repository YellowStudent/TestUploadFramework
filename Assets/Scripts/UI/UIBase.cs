using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBase {

    /// <summary>
    /// 自身关心的消息集合
    /// </summary>
    private List<int> list = new List<int>();

    /// <summary>
    /// 绑定一个或多个消息
    /// </summary>
    /// <param name="eventCodes">Event codes.</param>
    protected void Bind(params int[] eventCodes)
    {
        list.AddRange(eventCodes);
        UIManager.Instance.Add(list.ToArray(), this);
    }

    /// <summary>
    /// 解除绑定的消息
    /// </summary>
    protected void UnBind()
    {
        UIManager.Instance.Remove(list.ToArray(), this);
        list.Clear();
    }
    public virtual void OnApplicationQuit()
    {
        if (list.Count != 0&&UIManager.Instance!=null)
            UnBind();
    }
    /// <summary>
    /// 自动移除绑定的消息
    /// </summary>
    public virtual void OnDestroy()
    {
        if (list.Count != 0 && UIManager.Instance != null)
            UnBind();
    }

    /// <summary>
    /// 发消息
    /// </summary>
    /// <param name="areaCode">Area code.</param>
    /// <param name="eventCode">Event code.</param>
    /// <param name="message">Message.</param>
    public void Dispatch(int areaCode, int eventCode, params object[] message)
    {
        MsgCenter.Instance.Dispatch(areaCode, eventCode, message);
    }

    /// <summary>
    /// 设置面板显示
    /// </summary>
    /// <param name="active"></param>
    protected void SetPanelActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

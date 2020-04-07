using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;
public class ThreadHelper : MonoSingleton<ThreadHelper>
{
    //private object obj = new object();
    //private event Action currentAction;//创建一个委托事件
    //private void Update()
    //{
    //    lock (obj)
    //    {
    //        if (currentAction != null)
    //        {
    //            currentAction();
    //            currentAction = null;
    //        }
    //    }
    //}
    //public void AddThreadFunc(Action info)
    //{
    //    lock (obj)
    //    {
    //        if (currentAction == null)
    //        {
    //            currentAction = info;
    //        }
    //        else
    //        {
    //            currentAction += info;
    //        }
    //    }
    //}
    //public void AddThreadFunc(Action info, float time)
    //{
    //    //StartCoroutine(Wait(time, info));
    //    Thread.Sleep((int)(time * 1000));
    //    AddThreadFunc(info);
    //}
    public struct DelayedItem
    {
        public Action DelayedAction { get; set; }

        public DateTime DelayedTime { get; set; }
    }

    private Action currentAction;

    private List<DelayedItem> delayedActionList = new List<DelayedItem>();

    private object currentActionLocker = new object();

    public override void Init()
    {
        base.Init();
        delayedActionList = new List<DelayedItem>();
        currentActionLocker = new object();
    }
    private void Update()
    {
        CheckCurrentAction();

        CheckDelayedAction();
    }
    /// <summary>
    /// 在主线程中执行
    /// </summary>
    /// <param name="action">行为</param>
    public void ExecuteOnMainThread(Action action)
    {
        lock (currentActionLocker)
        {
            if (currentAction == null)
                currentAction = action;
            else
                currentAction += action;//向委托链条添加委托实例
        }
    }
    public void ExecuteOnMainThread(Action action, float time)
    {
        lock (delayedActionList)
        {
            delayedActionList.Add(new DelayedItem() { DelayedAction = action, DelayedTime = DateTime.Now.AddSeconds(time) });
        }
    }
    private void CheckCurrentAction()
    {
        lock (currentActionLocker)
        {
            if (currentAction != null)
            {
                currentAction();
                currentAction = null;
            }
        }
    }
    private void CheckDelayedAction()
    {
        lock (delayedActionList)
        {
            //判断每项是否达到执行时间
            for (int i = delayedActionList.Count - 1; i >= 0; i--)
            {
                //如果没有到达 判断下一项
                if (delayedActionList[i].DelayedTime > DateTime.Now) continue;
                //执行当前项 并从列表中移除
                delayedActionList[i].DelayedAction();//执行
                delayedActionList.RemoveAt(i);//从列表中移除
            }
        }
    }
}

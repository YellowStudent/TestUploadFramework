using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : ManagerBase {
    static NetManager instance;
    public static NetManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("没有");
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
}

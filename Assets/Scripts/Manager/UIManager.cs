using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase {
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("没有");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreManager : ManagerBase {
    static GameCoreManager instance;
    public static GameCoreManager Instance
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

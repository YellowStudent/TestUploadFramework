using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : ManagerBase {
    static CharacterManager instance;
    public static CharacterManager Instance
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

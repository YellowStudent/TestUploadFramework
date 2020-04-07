using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AudioManager : ManagerBase
{
    static AudioManager instance;
    public static AudioManager Instance
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


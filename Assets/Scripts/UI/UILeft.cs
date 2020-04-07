using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILeft : UIBase {

    private void Awake()
    {
        Bind(UIEvent.START_GAME);
        
    }
    public void Start()
    {
        SetPanelActive(false);
    }
    public override void Execute(int eventCode, params object[] message)
    {
        switch (eventCode)
        {
            case UIEvent.START_GAME:
                SetPanelActive(true);
                break;
        }
    }
}

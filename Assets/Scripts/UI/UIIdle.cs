using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIIdle : UIBase {
    private bool isActive;
    private float timeCount;
    public float waitingTime = 3;
    private MyButton activeBtn;
    private void Awake()
    {
        Bind(UIEvent.START_GAME);
        Init();
    }
    void Update()
    {
        if (isActive)
        {
            timeCount += Time.deltaTime;
            if (timeCount > waitingTime)
            {
                Debug.Log("成功激活");
                ResetParameter();
                Dispatch(AreaCode.GAME, GameCoreEvent.GAME_START);
            }
        }
    }
    private void Init()
    {
        isActive = false;
        timeCount = 0;
        activeBtn = TransformHelper.FindTransform(transform, "Active_Button").GetComponent<MyButton>();
        activeBtn.OnPointerDownEvent += ButtonDown;
        activeBtn.OnPointerUpEvent += ButtonUp;
    }
    public override void Execute(int eventCode, params object[] message)
    {
        switch(eventCode)
        {
            case UIEvent.START_GAME:
                SetPanelActive(true);
                break;
        }
    }
    private void ButtonUp()
    {
        ResetParameter();
    }
    private void ButtonDown()
    {
        isActive = true;
    }
    private void ResetParameter()
    {
        isActive = false;
        timeCount = 0;
    }
    public override void OnDestroy()
    {
        activeBtn.OnPointerDownEvent -= ButtonDown;
        activeBtn.OnPointerUpEvent -= ButtonUp;
        base.OnDestroy();
    }
}

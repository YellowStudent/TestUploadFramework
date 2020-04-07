using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button {
    
    public event Action OnPointerDownEvent;
    public event Action OnPointerUpEvent;
    public override void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent();
    }
}
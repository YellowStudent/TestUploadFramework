using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : UIBase {
    public int hp;

    private void Awake()
    {
        Bind(UIEvent.START_GAME,
            UIEvent.DAMAGE);
        hp = 20;
        SetPanelActive(false);
    }
    public override void Execute(int eventCode, params object[] message)
    {
        switch (eventCode)
        {
            case UIEvent.START_GAME:
                SetPanelActive(true);
                break;
            case UIEvent.DAMAGE:
                Damage();
                break;
        }
    }
    private void Damage()
    {
        hp -= 20;
        //特效TODO

        if(hp<=0)
        {
            Debug.Log("Death");
            //通知客户端停止游戏
            Dispatch(AreaCode.NET, NetEvent.ALL_REQUEST, AreaCode.GAME, GameCoreEvent.GAME_OVER);
            //自身结束游戏
            //GameObjectPool.Instance.CollectAllObject();
            SceneManager.LoadScene("Game");
        }
    }
}

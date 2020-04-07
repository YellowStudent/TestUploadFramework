using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 消息处理中心类：MessageCenter 
/// 只是负责消息的转发
/// </summary>
public class MsgCenter : MonoBase
{
    public static MsgCenter Instance = null;
    /// <summary>
    /// 中专中心，添加各类管理器
    /// </summary>
    void Awake()
    {
        Instance = this;
        ThreadHelper.Instance.Init();

        gameObject.AddComponent<AudioManager>();
        gameObject.AddComponent<GameCoreManager>();
        gameObject.AddComponent<UIManager>();
        gameObject.AddComponent<NetManager>();
        gameObject.AddComponent<CharacterManager>();


        DontDestroyOnLoad(gameObject);
        
    }
    private void Start()
    {
        SceneManager.LoadScene("Game");
    }
    /// <summary>
    /// 发送消息 系统里面所有的发消息 都通过这个方法来发
    ///   根据不同的模块 来转发给 不同的模块 
    ///     通过 areaCode识别模块
    /// 
    ///     第二个参数：事件码 作用？用来区分 做什么事情的
    ///         比如说 第一个参数 识别到是角色模块 但是角色模块有很多功能 比如 移动 攻击 死亡 逃跑...
    ///             就需要第二个参数 来识别 具体是做哪一个动作
    /// </summary>
    public void Dispatch(int areaCode, int eventCode,params object[] message)
    {
        switch (areaCode)
        {
            case AreaCode.AUDIO:
                AudioManager.Instance.Execute(eventCode, message);
                break;

            case AreaCode.GAME:
                GameCoreManager.Instance.Execute(eventCode, message);
                break;

            case AreaCode.CHARACTER:
                CharacterManager.Instance.Execute(eventCode, message);
                break;

            case AreaCode.NET:
                NetManager.Instance.Execute(eventCode, message);
                break;

            case AreaCode.UI:
                UIManager.Instance.Execute(eventCode, message);
                break;

            //case AreaCode.SCENE:
            //    SceneMgr.Instance.Execute(eventCode, message);
            //    break;

            default:
                break;
        }
    }
}

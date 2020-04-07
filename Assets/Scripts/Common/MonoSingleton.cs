using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T> 
{
    /// <summary>
    /// 我想要单例的对象
    /// </summary>
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //在场景中查找
                instance = FindObjectOfType<T>();
                if (instance==null)
                {
                    //说明脚本没有附加到游戏对象
                    instance = new GameObject("Singleton of " + typeof(T).Name).AddComponent<T>();
                }
               Instance.Init();
            }
            return instance;
        }
    }
    public void Awake()
    {
        instance = this as T;
    }

    public virtual void Init() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

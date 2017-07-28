using UnityEngine;

/// <summary>
/// 单例工具类
/// </summary>
/// <typeparam name="T">需要使用单例的类</typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;          //在场景中查找是否已有该类型的对象
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();       //创建一个空物体，挂载该对象
                    m_Instance.Init();      //如果子类中需要初始化，则在子类中重写
                }
            }
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this as T;
    }

    public virtual void Init() { }

    /// <summary>
    /// 程序结束时，设为null，以便释放垃圾
    /// </summary>
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}
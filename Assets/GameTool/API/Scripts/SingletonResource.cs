using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonResource<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject dialog = (GameObject)Instantiate(Resources.Load(path+ typeof(T).ToString()));
                    instance = dialog.GetComponent<T>();
                    DontDestroyOnLoad(dialog);
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        CheckInstance();
    }

    protected static string path = "";

    protected bool CheckInstance()
    {
        if (this == Instance) { return true; }
        Destroy(this);
        return false;
    }
}

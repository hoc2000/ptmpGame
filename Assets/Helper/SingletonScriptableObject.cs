using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    static T _instance = null;

    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.Load<T>("Data/" + typeof(T).Name);
            return _instance;
        }
    }
}

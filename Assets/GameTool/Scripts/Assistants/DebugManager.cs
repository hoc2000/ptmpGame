using System;
using UnityEngine;

public class DebugManager : SingletonMonoBehaviour<DebugManager>
{
    public bool DEBUG = true;



    public void Log(object message)
    {
        if (this.DEBUG)
        {
            UnityEngine.Debug.Log(message);
        }

    }

    public void LogError(object message)
    {
        if (this.DEBUG)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}

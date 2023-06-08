using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDDebug 
{
    public static void Log(string msg) {
#if !ENV_PROD
        Debug.Log(msg);
#endif
    }


    public static void LogError(string msg)
    {
#if !ENV_PROD
        Debug.LogError(msg);
#endif
    }

    public static void LogWarning(string msg)
    {
#if !ENV_PROD
        Debug.LogWarning(msg);
#endif
    }

    public static void LogFormat(string msg, params object[] args) {
#if !ENV_PROD
        Debug.LogFormat(msg, args);
#endif
    }

}

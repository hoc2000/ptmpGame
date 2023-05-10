using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFollowDevice : MonoBehaviour
{
    public float Aspect;
    public bool GizmosUpdate;
    public bool IsInvert;
    public float CurCamSize;
#if UNITY_EDITOR
    public static Action OnSolutionChanged;
#endif

    public List<ResolutionInfor> Resolutions = new List<ResolutionInfor>
    {
        new ResolutionInfor {Aspect = 2732f / 2048f}, // iPad
        new ResolutionInfor {Aspect = 2208f / 1242f}, // iPhone
        new ResolutionInfor {Aspect = 2437f / 1125f}, // iPhoneX
    };

    private CanvasScaler _canvasScaler;
    //[SerializeField] private Camera _cam;

    private void Awake()
    {
        // _cam = Camera.main;
        _canvasScaler = GetComponent<CanvasScaler>();

        var canvas = gameObject.GetComponent<Canvas>();
        // canvas.worldCamera = _cam;
        FixCamSizeFollowScreen();


        //Debug.Log(Screen.width + "/" + Screen.height + " = " + ((float)Screen.width / (float)Screen.height));
#if UNITY_EDITOR
        OnSolutionChanged += Update;
#endif
    }

#if UNITY_EDITOR
    public void Update()
    {
        FixCamSizeFollowScreen();
    }
#endif

    [ContextMenu("Fix cam zide follow screen")]
    private void FixCamSizeFollowScreen()
    {
        if (this != null && !enabled)
            return;

        Aspect = (float)Screen.width / (float)Screen.height;
        for (int i = 0; i < Resolutions.Count; i++)
        {
            if (i == Resolutions.Count - 1)
            {
                if (Aspect >= Resolutions[i].Aspect)
                {
                    if (_canvasScaler == null)
                        _canvasScaler = GetComponent<CanvasScaler>();

                    _canvasScaler.matchWidthOrHeight = Resolutions[i].Scaler;
                }
            }
            else if (Aspect >= Resolutions[i].Aspect && Aspect < Resolutions[i + 1].Aspect)
            {
                if (_canvasScaler == null)
                    _canvasScaler = GetComponent<CanvasScaler>();

                _canvasScaler.matchWidthOrHeight = Resolutions[i].Scaler;
            }
        }
        //if (IsInvert)
        //{
        //    //Aspect = 1 / Camera.main.aspect;
        //}
        //else
        //{
        //    //Aspect = Camera.main.aspect;
        //}

        //for (int i = 0; i < Resolutions.Count - 1; i++)
        //{
        //    if (Aspect >= Resolutions[i].Aspect && Aspect < Resolutions[i + 1].Aspect)
        //    {
        //        if (_canvasScaler == null)
        //            _canvasScaler = GetComponent<CanvasScaler>();

        //        _canvasScaler.matchWidthOrHeight = Resolutions[i].Scaler + (Aspect - Resolutions[i].Aspect) /
        //                                           (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) *
        //                                           (Resolutions[i + 1].Scaler - Resolutions[i].Scaler);

        //        //if (_cam == null)
        //        //   _cam = Camera.main;

        //        //if (_cam.orthographic)
        //        //{
        //        //    _cam.orthographicSize = Resolutions[i].CamSize + (Aspect - Resolutions[i].Aspect) /
        //        //                            (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) *
        //        //                            (Resolutions[i + 1].CamSize - Resolutions[i].CamSize);
        //        //}
        //        //else
        //        //{
        //        //    _cam.fieldOfView = Resolutions[i].PerspectiveSize + (Aspect - Resolutions[i].Aspect) /
        //        //                            (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) *
        //        //                            (Resolutions[i + 1].PerspectiveSize - Resolutions[i].PerspectiveSize);
        //        //}

        //        return;
        //    }
        //}

        // CurCamSize = _cam.orthographicSize;
    }


    [ContextMenu("OrderResolutions")]
    public void OrderResolutions()
    {
        Resolutions.OrderBy(s => s.Aspect);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (GizmosUpdate)
        {
            FixCamSizeFollowScreen();
        }
    }
#endif
}

[Serializable]
public class ResolutionInfor
{
    public float Aspect;
    //public float CamSize;
    //public float PerspectiveSize = 60;
    public float Scaler;
}
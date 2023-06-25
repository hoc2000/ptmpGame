using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakerTimeline : MonoBehaviour
{
    [SerializeField] float timeShake;
    [SerializeField] float valueShake;
    private void OnEnable()
    {
        CameraShake.Instance.Shakedown(timeShake, valueShake);
    }
}

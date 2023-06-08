using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : SingletonMonoBehaviour<VibrationManager>
{
    public void VibrateSelection()
    {
        Vibrate(HapticTypes.Selection);
    }
    public void VibrateSuccess()
    {
        Vibrate(HapticTypes.Success);
    }
    public void VibrateWarning()
    {
        Vibrate(HapticTypes.Warning);
    }
    public void VibrateFailure()
    {
        Vibrate(HapticTypes.Failure);
    }
    public void VibrateLight()
    {
        Vibrate(HapticTypes.LightImpact);
    }
    public void VibrateMedium()
    {
        Vibrate(HapticTypes.MediumImpact);
    }
    public void VibrateHeaving()
    {
        Vibrate(HapticTypes.HeavyImpact);
    }
    public void VibrateRigid()
    {
        Vibrate(HapticTypes.RigidImpact);
    }
    public void VibrateSoft()
    {
        Vibrate(HapticTypes.SoftImpact);
    }

    public void Vibrate(HapticTypes type)
    {
        if (Gamedata.I.Vibrate)
        {
            MMVibrationManager.Haptic(type);
            //Debug.LogError(" Vibration "+ type.ToString());
        }
    }


}

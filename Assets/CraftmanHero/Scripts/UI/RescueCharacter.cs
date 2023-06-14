using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueCharacter : SkinItem
{
    public override void SellectClick()
    {
        base.SellectClick();
        for (int i = 0; i < SkinPopup.instance.rescueCharacters.Count; i++)
        {
            SkinPopup.instance.rescueCharacters[i].skinItem.sprite = unselectedSkin;
        }
        skinItem.sprite = selectedSkin;
        SkinPopup.instance.SellectSkin(resourceItem, false, true,false);
        SkinPopup.instance.ChangeSkin(resourceItem);

    }
}

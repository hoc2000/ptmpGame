using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumSkinCharacter : SkinItem
{
    public override void SellectClick()
    {
        base.SellectClick();
        for (int i = 0; i < SkinPopup.instance.premiumCharacters.Count; i++)
        {
            SkinPopup.instance.premiumCharacters[i].skinItem.sprite = unselectedSkin;
        }
        skinItem.sprite = selectedSkin;
        SkinPopup.instance.SellectSkin(resourceItem, true, false,false);
        Debug.Log(Gamedata.HasCharacter(resourceItem.detail) + " nhan vat nay co ko");

    }
}

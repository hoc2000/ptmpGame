using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] private bool isAds;
	[SerializeField] GameObject adsIcon;
	[SerializeField] ResourceDetail resourceDetail;
	[SerializeField] Item resourceItem;
	private void Start()
    {
		if (!isAds)
		{
			adsIcon.SetActive(false);
		}
		else
		{
			adsIcon.SetActive(true);

		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(Constants.TAG.PLAYER))
		{			
            if (!isAds)
            {
				//Gamedata.TemporarySelectedCharacter = resourceDetail;
				Player.Instance.characterMovement2D._idWeapon = 1;
				Player.Instance.SetSkinCharacterWithWeapon();
				gameObject.SetActive(false);
			}
			else
            {
                GetComponent<ShowRewardedController>().Show();
            }
		}
	}

	public void GetSword(bool success)
	{
		if (success)
		{
			Gamedata.getWeapon = true;
			Player.Instance.characterMovement2D._idWeapon = 1;
			Player.Instance.SetSkinCharacterWithWeapon();
			Player.Instance.characterMovement2D.inputManager.leftClick = false;
			Player.Instance.characterMovement2D.inputManager.rightClick = false;
			gameObject.SetActive(false);
        }
	}
}

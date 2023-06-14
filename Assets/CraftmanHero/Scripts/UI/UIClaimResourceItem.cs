using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIClaimResourceItem : MonoBehaviour
{
    public GameObject staticImage;
    public GameObject spineAnim;

    [SerializeField]
    private TextMeshProUGUI txtQuantity;

    public void AssignClaimResourceItem(SpinRewardItem item)
    {
       
            if (item.resourceItems[0].resourceItem.type != ResourceType.Character)
            {
                //spineAnim.SetActive(false);
                staticImage.SetActive(true);
                if (item.resourceItems[0].resourceItem.spriteUI != null)
                {
                    staticImage.GetComponent<Image>().sprite = item.resourceItems[0].resourceItem.spriteUI;
                }
                else
                {
                    Sprite spr = DataHolder.Instance.defaultResourceTypeSprites[item.resourceItems[0].resourceItem.type];
                    if (spr != null)
                    {
                        staticImage.GetComponent<Image>().sprite = spr;
                    }
                }
                
            }
            else
            {
                //spineAnim.SetActive(true);
                //staticImage.SetActive(false);
                //if (item.resourceItems[0].resourceItem.countable)
                //{
                //    bool hasCharacter = Gamedata.HasCharacter(item.resourceItems[0].resourceItem.detail);
                //    txtQuantity.gameObject.SetActive(true);
                //    txtQuantity.text = "YOU CAN TRY FREE THIS CHARACTER";
                //    txtQuantity.GetComponent<Text>().color = Color.yellow;
                //    if (hasCharacter)
                //    {
                //        txtQuantity.text = "YOU CAN USE CHARACTER";
                //    }
                //}
                //else
                //{
                //    txtQuantity.gameObject.SetActive(false);
                //}
                //CharacterInfo characterInfo = DataHolder.Instance.characters[item.resourceItems[0].resourceItem.detail];
                //if (characterInfo != null)
                //{
                //    SkeletonGraphic skeletonGraphic = spineAnim.GetComponent<SkeletonGraphic>();
                //    skeletonGraphic.initialSkinName = characterInfo.skinName;
                //    skeletonGraphic.Initialize(true);
                //    skeletonGraphic.startingAnimation = characterInfo.animName;
                //    skeletonGraphic.Skeleton.SetSlotsToSetupPose();
                //}
            
        }
    }
       
}

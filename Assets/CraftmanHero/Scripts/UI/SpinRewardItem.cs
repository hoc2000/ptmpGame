using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRewardItem : MonoBehaviour
{
    public List<SlotGiftSpin> resourceItems;

    public int percentItem;
    public TypeGift typeGift;
    public enum TypeGift
    {
        resource, gift
    }
    [System.Serializable]
    public class SlotGiftSpin
    {
        public Item resourceItem;
        public float percent;
    }

   
    
}

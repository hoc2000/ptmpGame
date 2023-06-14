using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardItem : MonoBehaviour
{

    List<DailyRewardResourceItem> drResourceItems;

    private void Awake()
    {
        drResourceItems = new List<DailyRewardResourceItem>();

    }
    public void Claim(int factor)
    {
        List<Item> claimResourceItems = new List<Item>();
        Item resourceItem = new Item();
        foreach (DailyRewardResourceItem drResourceItem in drResourceItems)
        {
            if (drResourceItem.resourceItem.detail == ResourceDetail.None)
            {
                int realQuantity = drResourceItem.resourceItem.quantity * factor;
                Item claimResourceItem = (Item)(drResourceItem.resourceItem).Clone();

                claimResourceItem.quantity = realQuantity;
                claimResourceItems.Add(claimResourceItem);
            }
            else
            {
                claimResourceItems.Add(drResourceItem.resourceItem);
            }
        }
        Gamedata.ClaimResourceItems(claimResourceItems);
        if (resourceItem.type == ResourceType.Coin)
        {
            Gamedata.CoinReceived += resourceItem.quantity;

        }

    }
}

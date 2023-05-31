using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCollection.asset", menuName = "GameGuys/ItemCollection")]
public class ItemCollection : ScriptableObject
{
    public List<Item> items;
}

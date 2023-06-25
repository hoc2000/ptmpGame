using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



[CreateAssetMenu(fileName = "DataHolder.asset", menuName = "GameGuys/DataHolder")]

public class DataHolder : SingletonScriptableObject<DataHolder>
{
    public ResourceDetailCharacterInfoDictionary characters;
    public ResourceDetailCharacterInfoDictionary charactersWithWeapon;
    public ResourceTypeSpriteDictionary defaultResourceTypeSprites;
    public ResourceTypeSpriteListDictionary resourceSpriteListDictionary;

    public Item RandomCharacter(Item ri)
    {
        ResourceDetail randomCharacter = this.characters.Keys.Where(c => !Gamedata.HasCharacter(c)).OrderBy(x => UnityEngine.Random.Range(0, 10000)).FirstOrDefault();
        if (randomCharacter != ResourceDetail.None)
        {
            Gamedata.ClaimCharacter(randomCharacter);
            Item ret = (Item)ri.Clone();
            ret.AssignCharacter(randomCharacter);
            return ret;
        }
        return null;
    }   public Item RandomCharacterWithWeapon(Item ri)
    {
        ResourceDetail randomCharacter = this.charactersWithWeapon.Keys.Where(c => !Gamedata.HasCharacter(c)).OrderBy(x => UnityEngine.Random.Range(0, 10000)).FirstOrDefault();
        if (randomCharacter != ResourceDetail.None)
        {
            Gamedata.ClaimCharacter(randomCharacter);
            Item ret = (Item)ri.Clone();
            ret.AssignCharacter(randomCharacter);
            return ret;
        }
        return null;
    }
    public Sprite GetSpriteResource(ResourceType resourceType, int number)
    {
        switch (resourceType)
        {
            case ResourceType.Coin:
                Sprite[] spriteCoins = resourceSpriteListDictionary[ResourceType.Coin].sprites;
                int indexCoins = Math.Min(number / 50, spriteCoins.Length - 1);
                return spriteCoins[indexCoins];
            case ResourceType.Gem:
                Sprite[] spriteGems = resourceSpriteListDictionary[ResourceType.Gem].sprites;
                int indexGem = Math.Min(number / 50, spriteGems.Length - 1);
                return spriteGems[indexGem];
        }
        return null;
    }
}




[Serializable]
public class ResourceTypeSpriteDictionary : SerializableDictionaryBase<ResourceType, Sprite>
{

}

[Serializable]
public class ResourceTypeSpriteListDictionary : SerializableDictionaryBase<ResourceType, SpriteArray>
{

}


[Serializable]
public class ResourceDetailCharacterInfoDictionary : SerializableDictionaryBase<ResourceDetail, CharacterInfo>
{
}

[Serializable]
public class ResourceDetailCharacterWeaponInfoDictionary : SerializableDictionaryBase<ResourceDetail, CharacterInfo>
{

}
[Serializable]
public class SpriteArray
{
    public Sprite[] sprites;
}
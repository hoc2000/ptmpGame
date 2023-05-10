using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My.Tool;

public class CoinTxt : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateText();

        //this.RegisterListener(EventID.UpdateText, (sender, param) => { UpdateText();});
    }
    private void OnDisable()
    {
        //this.RegisterListener(EventID.UpdateText, (sender, param) => { UpdateText(); });
    }
    void UpdateText()
    {
        //gameObject.GetComponent<Text>().text = GameData.Coin.ToString();
    }
}

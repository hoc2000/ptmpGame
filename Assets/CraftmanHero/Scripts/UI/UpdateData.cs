using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using My.Tool;
public class UpdateData : MonoBehaviour
{
    public DataType dataType;

    [SerializeField] TextMeshProUGUI textData;

    private void OnEnable()
    {
        if (textData == null)
        {
            textData = GetComponent<TextMeshProUGUI>();
        }
        UpdateText();

        this.RegisterListener(EventID.UpdateData, (sender, param) => UpdateText());
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.UpdateData, (sender, param) => UpdateText());
    }
    void UpdateText()
    {
        switch (dataType)
        {
            case DataType.Coin:
                textData.text = Gamedata.I.Coin.ToString();
                break;
            case DataType.Life:
                textData.text = Gamedata.I.Life.ToString();
                break;
        }
    }
}
public enum DataType
{
    Coin,
    Life
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField]
    private TabItem[] tabs;

    [SerializeField]
    private Sprite sprTabNormal;

    [SerializeField]
    private Sprite sprTabSelected;
    


    [SerializeField]
    private int defaultTabIndex;

    private int selectedTabIndex = -1;

    [SerializeField]
    private PremiumSkinCharacter premiumShopCharacter;

    //[SerializeField]
    //private ShopCharacterController shopCharacterController;

    //[SerializeField]
    //private ShopRescueCharacter shopRescueCharacter;



    private void Awake()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            int x = i;
            tabs[i].btnTab.onClick.AddListener(() => { SelectTab(x); });
        }
    }

    private void OnEnable()
    {
        //selectedTabIndex = -1;

        //Invoke("OpenTab", 0.1f);
    }


    private void Start()
    {
        SelectTab(defaultTabIndex);

    }
    private void OpenTab()
    {
        SelectTab(defaultTabIndex);

    }

    public void SelectTab(int tabIndex)
    {
        if (selectedTabIndex != tabIndex)
        {
            if (selectedTabIndex >= 0)
            {

                UpdateTabStatus(tabs[selectedTabIndex].btnTab, false);
                tabs[selectedTabIndex].detailView.SetActive(false);
                UpdateTabStatus(tabs[tabIndex].btnTab, true);
                tabs[tabIndex].detailView.SetActive(true);
            }
            else
            {

                for (int i = 0; i < tabs.Length; i++)
                {
                    bool selected = i == tabIndex;
                    UpdateTabStatus(tabs[i].btnTab, selected);
                    tabs[i].detailView.SetActive(selected);

                }
            }
            selectedTabIndex = tabIndex;
            UpdateTab(tabIndex);

        }
    }

    public void UpdateTab(int tabIndex)
    {
        if (tabIndex == 0)
        {

            premiumShopCharacter.SellectClick();

        }
        else if (tabIndex == 1)
        {
            //shopCharacterController.OnClick();

        }
        else if (tabIndex == 2)
        {
            //shopRescueCharacter.OnClick();

        }
    }
    private void UpdateTabStatus(Button btnTab, bool selected)
    {
        btnTab.image.sprite = selected ? sprTabSelected : sprTabNormal;
        //btnTab.GetComponentInChildren<Text>().color = selected ? Color.white : Color.black;

    }

    [Serializable]
    public class TabItem
    {
        public Button btnTab;
        public GameObject detailView;
    }
}

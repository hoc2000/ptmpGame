using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using WidogameFoundation.Ads;

public class ShowHideBannerController : MonoBehaviour
{
    [SerializeField]
    private bool showOnEnabled;
    private void OnEnable()
    {
#if !ENV_CREATIVE
        if (showOnEnabled && !Gamedata.IsRemoveAds)
        {
            AdsMediationController.Instance.ShowBanner(BannerPosition.Top);
        }
        else
        {
            AdsMediationController.Instance.HideBanner();
        }
#endif
    }

    private void OnDisable()
    {
#if !ENV_CREATIVE
        if (showOnEnabled)
        {
            AdsMediationController.Instance.HideBanner();
        }
        else
        {
            AdsMediationController.Instance.ShowBanner(BannerPosition.Top);
        }
#endif
    }
}

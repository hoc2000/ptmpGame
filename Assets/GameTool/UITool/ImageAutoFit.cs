using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAutoFit : MonoBehaviour
{
    public bool AutoDestroy = true;

    private void Start()
    {
        var targetImage = this.GetComponent<Image>();
        var imageWidth = targetImage.sprite.rect.width;
        var imageHeight = targetImage.sprite.rect.height;
        var imageRatio = imageWidth / imageHeight;

        var targetTransform = this.GetComponent<RectTransform>();
        var currentWidth = targetTransform.rect.width;
        var currentHeight = targetTransform.rect.height;
        var currentRatio = currentWidth / currentHeight;

        if (currentRatio >= imageRatio)
        {
            this.transform.localScale *= currentRatio / imageRatio;
        }
        else
        {
            this.transform.localScale *= imageRatio / currentRatio;
        }

        if (AutoDestroy)
        {
            Destroy(this);
        }
    }
}

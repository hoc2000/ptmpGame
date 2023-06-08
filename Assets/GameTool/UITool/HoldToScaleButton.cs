using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace My.Tool.UI
{
    public class HoldToScaleButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerEnterHandler
    {
        [SerializeField]
        Vector3 ScaleValue = new Vector3(1.2f, 1.2f, 1.2f);

        Vector3 mBaseScale;
        bool mOnHold = false;
        Button mButton = null;

        void Awake()
        {
            mBaseScale = transform.localScale;
            mButton = GetComponent<Button>();
        }

        private void OnDisable()
        {
            mOnHold = false;
            transform.localScale = mBaseScale;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            mOnHold = true && mButton.interactable;
            if (mOnHold)
            {
                transform.localScale = ScaleValue;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (mOnHold)
            {
                transform.localScale = mBaseScale;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            mOnHold = false;
            transform.localScale = mBaseScale;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (mOnHold)
            {
                transform.localScale = ScaleValue;
            }
        }
    }
}
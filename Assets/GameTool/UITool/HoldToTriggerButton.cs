using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace My.Tool.UI
{
    [RequireComponent(typeof(Button))]
    public class HoldToTriggerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        float RateTime = 0.15f;

        Button mButton;
        bool mOnHold = false;
        float mHoldingTime = 0;

        private void Awake()
        {
            mButton = this.GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!mOnHold || !mButton.interactable) return;

            mHoldingTime += Time.deltaTime;

            if (mHoldingTime >= RateTime)
            {
                mHoldingTime -= RateTime;
                mButton.onClick.Invoke();
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            mHoldingTime = 0;
            mOnHold = true && mButton.interactable;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            mOnHold = false;
        }
    }
}
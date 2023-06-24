//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using System;


//namespace My.Tool
//{
//    public class AnimUiMgr : MonoBehaviour
//    {
//        [Header("Made By Alexandros Thang")]

//        public AnimType animTypeOpen;
//        public AnimType animTypeClose;
//        public bool useAnimChildren;
//        public bool useTimeChildren;

//        [Header("Time Anim Scale Button")]
//        [Space(10)]
//        [SerializeField] float timeScale = 0.2f;

//        [Header("Time Delay")]
//        [Space(10)]
//        [SerializeField] float timeDelayOpen = 0.1f;
//        [SerializeField] float timeDelayClose = 0.2f;
//        [SerializeField] float timeDelayOpenChildren = 0.1f;


//        [Header("Object Anim")]
//        [SerializeField] AnimUI[] animObj;


//        public Action OnClose;

//        private WaitForSecondsRealtime waitForSecondsRealtimeScale;
//        private WaitForSecondsRealtime waitForSecondsRealtimeDelay;
//        private void OnEnable()
//        {
//            Init();
//        }

//        void Init()
//        {
//            waitForSecondsRealtimeScale = new WaitForSecondsRealtime(timeScale);
//            waitForSecondsRealtimeDelay = new WaitForSecondsRealtime(timeDelayOpen);
//            switch (animTypeOpen)
//            {
//                case AnimType.sequence:
//                    StartCoroutine(AnimPopupSequence());
//                    break;
//                case AnimType.together:
//                    StartCoroutine(AnimPopupTogether());
//                    break;
//                case AnimType.custom:
//                    StartCoroutine(AnimPopupCustom());
//                    break;
//            }

//        }
//        #region Anim Type
//        IEnumerator AnimPopupSequence()
//        {
//            for (int i = 0; i < animObj.Length; i++)
//            {
//                animObj[i].parentObj.transform.localScale = Vector3.zero;
//            }

//            for (int i = 0; i < animObj.Length; i++)
//            {
//                yield return waitForSecondsRealtimeDelay;
//                animObj[i].parentObj.transform.DOScale(Vector3.one, timeScale).SetEase(Ease.OutBack);

//                StartCoroutine(AnimChildren(i));
//            }
//        }
//        IEnumerator AnimPopupTogether()
//        {
//            for (int i = 0; i < animObj.Length; i++)
//            {
//                animObj[i].parentObj.transform.localScale = Vector3.zero;
//            }

//            animObj[0].parentObj.transform.DOScale(Vector3.one, timeScale).SetEase(Ease.OutBack);
//            yield return waitForSecondsRealtimeDelay;

//            for (int i = 1; i < animObj.Length; i++)
//            {
//                animObj[i].parentObj.transform.DOScale(Vector3.one, timeScale).SetEase(Ease.OutBack);
//                StartCoroutine(AnimChildren(i));
//            }
//        }

//        IEnumerator AnimPopupCustom()
//        {
//            for (int i = 0; i < animObj.Length; i++)
//            {
//                animObj[i].parentObj.transform.localScale = Vector3.zero;
//            }

//            for (int i = 0; i < animObj.Length; i++)
//            {
//                if (i == 0)
//                {
//                    animObj[i].parentObj.SetActive(true);
//                }

//                yield return new WaitForSecondsRealtime(animObj[i].timeDelay);

//                animObj[i].parentObj.transform.DOScale(Vector3.one, animObj[i].timeScale).SetEase(animObj[i].easeAnim);


//                StartCoroutine(AnimChildren(i));
//            }
//        }

//        IEnumerator AnimChildren(int i)
//        {
//            if (useAnimChildren)
//            {

//                if (animObj[i].childrenObj.Length != 0)
//                {
//                    for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                    {
//                        animObj[i].childrenObj[j].transform.localScale = Vector3.zero;
//                    }

//                    if (useTimeChildren)
//                    {
//                        yield return new WaitForSecondsRealtime(timeDelayOpenChildren);
//                    }
//                    yield return waitForSecondsRealtimeDelay;
//                    for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                    {
//                        animObj[i].childrenObj[j].transform.DOScale(Vector3.one, timeScale).SetEase(Ease.OutBack);
//                        if (useTimeChildren)
//                        {
//                            yield return new WaitForSecondsRealtime(animObj[i].timeDelayChildren);
//                        }
//                    }
//                }
//                else
//                {
//                    //Debug.LogError("==> Children Is Null");
//                }
//            }
//        }


//        #endregion


//        #region Anim Close Panel
//        public void CloseAnim()
//        {
//            switch (animTypeClose)
//            {
//                case AnimType.sequence:
//                    StartCoroutine(CloseAnimPopupSequence());
//                    break;
//                case AnimType.together:
//                    StartCoroutine(CloseAnimTogether());
//                    break;
//                case AnimType.custom:
//                    StartCoroutine(CloseAnimCustom());
//                    break;
//            }
//        }
//        IEnumerator CloseAnimTogether()
//        {
//            for (int i = 1; i < animObj.Length; i++)
//            {
//                // StartCoroutine(AnimChildrenClose(i));
//                //AnimChildrenClose(i);

//                if (useAnimChildren)
//                {
//                    if (animObj[i].childrenObj.Length != 0)
//                    {

//                        for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                        {
//                            animObj[i].childrenObj[j].transform.DOScale(Vector3.zero, timeScale).SetEase(Ease.InBack);
//                        }
//                        //yield return waitForSecondsRealtimeDelay;
//                    }
//                }
//                //yield return new WaitForSecondsRealtime(animObj[i].timeDelay);
//                animObj[i].parentObj.transform.DOScale(Vector3.zero, animObj[i].timeScale).SetEase(Ease.InBack);
//            }
//            yield return new WaitForSecondsRealtime(timeDelayClose);
//            animObj[0].parentObj.SetActive(false);
//            if (OnClose != null)
//            {
//                OnClose();
//            }

//        }
//        IEnumerator CloseAnimCustom()
//        {
//            for (int i = 1; i < animObj.Length; i++)
//            {
//                // StartCoroutine(AnimChildrenClose(i));
//                //AnimChildrenClose(i);

//                if (useAnimChildren)
//                {
//                    if (animObj[i].childrenObj.Length != 0)
//                    {

//                        for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                        {
//                            animObj[i].childrenObj[j].transform.DOScale(Vector3.zero, timeScale).SetEase(Ease.InBack);
//                        }
//                        yield return waitForSecondsRealtimeDelay;
//                    }
//                }
//                yield return new WaitForSecondsRealtime(animObj[i].timeDelay);
//                animObj[i].parentObj.transform.DOScale(Vector3.zero, animObj[i].timeScale).SetEase(Ease.InBack);
//            }
//            yield return new WaitForSecondsRealtime(animObj[0].timeDelay);
//            animObj[0].parentObj.SetActive(false);
//            if (OnClose != null)
//            {
//                OnClose();
//            }

//        }
//        IEnumerator CloseAnimPopupSequence()
//        {
//            for (int i = 1; i < animObj.Length; i++)
//            {
//                // StartCoroutine(AnimChildrenClose(i));
//                //AnimChildrenClose(i);

//                if (useAnimChildren)
//                {
//                    if (animObj[i].childrenObj.Length != 0)
//                    {

//                        for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                        {
//                            animObj[i].childrenObj[j].transform.DOScale(Vector3.zero, timeScale).SetEase(Ease.InBack);
//                        }
//                        yield return waitForSecondsRealtimeDelay;
//                    }
//                }
//                yield return waitForSecondsRealtimeDelay;
//                animObj[i].parentObj.transform.DOScale(Vector3.zero, animObj[i].timeScale).SetEase(Ease.InBack);
//            }
//            yield return waitForSecondsRealtimeDelay;
//            animObj[0].parentObj.SetActive(false);
//            if (OnClose != null)
//            {
//                OnClose();
//            }

//        }
//        void AnimChildrenClose(int i)
//        {
//            if (useAnimChildren)
//            {
//                if (animObj[i].childrenObj.Length != 0)
//                {
//                    //yield return waitForSecondsRealtimeDelay;
//                    for (int j = 0; j < animObj[i].childrenObj.Length; j++)
//                    {
//                        animObj[i].childrenObj[j].transform.DOScale(Vector3.zero, timeScale).SetEase(Ease.InBack);
//                    }
//                }
//                else
//                {
//                    Debug.LogError("==> Children Is Null");
//                }
//            }
//        }
//        #endregion

//        public enum AnimType
//        {
//            sequence,
//            together,
//            custom
//            //parent
//        }
//    }

//    [Serializable]
//    public class AnimUI
//    {
//        public GameObject parentObj;
//        public Ease easeAnim = Ease.OutBack;
//        public float timeDelay;
//        public float timeScale;
//        public float timeDelayChildren;
//        public GameObject[] childrenObj;
//    }
//}


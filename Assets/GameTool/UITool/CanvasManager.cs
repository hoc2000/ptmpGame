using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;

namespace My.Tool.UI
{
    public enum eUILayer
    {
        Background = 0,
        Menu,
        Popup,
        AlwaysOnTop
    }

    public class CanvasManager : Tool.SingletonMono<CanvasManager>
    {
        static Canvas _UICanvas;

        public static Canvas UICanvas
        {
            get { return _UICanvas; }
        }

        public static float ScreenScale { get; protected set; }

        static RectTransform _UIRectTrans;
        public static RectTransform UIRectTrans { get { return _UIRectTrans; } }
        static RectTransform _AdsRectTrans;
        public static RectTransform AdsRectTrans { get { return _AdsRectTrans; } }

        static string DefaultDataPath;
        static Dictionary<string, Stack<BaseUIMenu>> UICached = new Dictionary<string, Stack<BaseUIMenu>>();
        static List<List<BaseUIMenu>> OpenedUIStack = new List<List<BaseUIMenu>>();


#if UNITY_EDITOR
        static bool sFinishAwake = false;
#endif
        protected virtual void Awake()
        {
            _UICanvas = this.GetComponent<Canvas>();
            _UIRectTrans = new GameObject("UI", typeof(RectTransform)).GetComponent<RectTransform>();
            _UIRectTrans.SetParent(this.transform);
            SetFullScreenRect(_UIRectTrans);
            _AdsRectTrans = Instantiate(_UIRectTrans, this.transform);
            _AdsRectTrans.name = "Ads";

            var layers = System.Enum.GetNames(typeof(eUILayer));
            for (int i = 0; i < layers.Length; ++i)
            {
                var newLayer = new GameObject(layers[i], typeof(RectTransform));
                newLayer.transform.SetParent(_UIRectTrans.transform);
                SetFullScreenRect(newLayer.GetComponent<RectTransform>());
                OpenedUIStack.Add(new List<BaseUIMenu>());
            }

            ScreenScale = UICanvas.pixelRect.size.y / UICanvas.scaleFactor / 1080;


#if UNITY_EDITOR
            sFinishAwake = true;
#endif
        }

        void SetFullScreenRect(RectTransform target)
        {
            target.transform.localScale = Vector3.one;
            target.transform.localEulerAngles = Vector3.zero;
            target.transform.localPosition = Vector3.zero;
            target.anchorMin = Vector2.zero;
            target.anchorMax = Vector2.one;
            target.offsetMin = Vector2.zero;
            target.offsetMax = Vector2.zero;
        }
        public static void SetAdsBannerSizeByRatio(bool top, float ratioByWidth)
        {
            SetAdsBannerSize(top, Mathf.CeilToInt(_AdsRectTrans.rect.width * ratioByWidth));
        }

        public static void SetAdsBannerSize(bool top, int height)
        {
            _UIRectTrans.offsetMin = new Vector2(_UIRectTrans.offsetMin.x, top ? 0 : height);
            _UIRectTrans.offsetMax = new Vector2(_UIRectTrans.offsetMin.x, top ? -height : 0);
            _AdsRectTrans.offsetMin = new Vector2(_AdsRectTrans.offsetMin.x, top ? _UIRectTrans.rect.height : 0);
            _AdsRectTrans.offsetMax = new Vector2(_AdsRectTrans.offsetMin.x, top ? 0 : -_UIRectTrans.rect.height);
        }

        public static void SetAdsBannerSize(bool top, int height, eUILayer layer)
        {
            var layerTrans = _UIRectTrans.GetChild((int)layer).GetComponent<RectTransform>();
            layerTrans.offsetMin = new Vector2(_UIRectTrans.offsetMin.x, top ? 0 : height);
            layerTrans.offsetMax = new Vector2(_UIRectTrans.offsetMin.x, top ? -height : 0);
            _AdsRectTrans.offsetMin = new Vector2(_AdsRectTrans.offsetMin.x, top ? _UIRectTrans.rect.height : 0);
            _AdsRectTrans.offsetMax = new Vector2(_AdsRectTrans.offsetMin.x, top ? 0 : -_UIRectTrans.rect.height);
        }

        public static void SetBannerBackgroundColor(Color input)
        {
            Image img = _AdsRectTrans.GetComponent<Image>();
            if (img == null)
            {
                img = _AdsRectTrans.gameObject.AddComponent<Image>();
            }

            img.color = input;
        }

        public static void SetBannerBackgroundSprite(Sprite input)
        {
            Image img = _AdsRectTrans.GetComponent<Image>();
            if (img == null)
            {
                img = _AdsRectTrans.gameObject.AddComponent<Image>();
            }

            img.sprite = input;
        }

        public static BaseUIMenu TryCacheUI(string identifier)
        {
            if (!UICached.ContainsKey(identifier))
            {
                UICached[identifier] = new Stack<BaseUIMenu>();

                var prefab = Resources.Load<BaseUIMenu>(DefaultDataPath + identifier);
                var cached = Instantiate(prefab, _UIRectTrans.GetChild((int)prefab.UILayer));
                cached.UIIdentifier = identifier;
                UICached[identifier].Push(cached);
                return cached;
            }
            else if (UICached[identifier].Count <= 0)
            {
                var prefab = Resources.Load<BaseUIMenu>(DefaultDataPath + identifier);
                var cached = Instantiate(prefab, _UIRectTrans.GetChild((int)prefab.UILayer));
                cached.UIIdentifier = identifier;
                UICached[identifier].Push(cached);
#if UNITY_EDITOR
                //if (cached.IsUnique) Debug.LogError(string.Format("UI {0} is Unique!!!", identifier));
#endif
                return cached;
            }

            return null;
        }

        public static System.Action<BaseUIMenu> EventOnMenuPushed;
        public static System.Action<BaseUIMenu> EventOnMenuPopped;

        public static BaseUIMenu Push(string identifier, object[] initParams)
        {
            TryCacheUI(identifier);

            BaseUIMenu menu = UICached[identifier].Pop();
            if (menu.UILayer == eUILayer.Menu && OpenedUIStack[(int)eUILayer.Popup].Count > 0)
            {
                PopAllLayer(eUILayer.Popup);
            }
            if (menu != null)
            {
                menu.gameObject.SetActive(true);
                OpenedUIStack[(int)menu.UILayer].Add(menu);
                menu.Init(initParams);
                menu.ResetActiveTime();
                menu.transform.SetAsLastSibling();
            }


            if (EventOnMenuPushed != null)
            {
                EventOnMenuPushed(menu);
            }
            if (menu != null)
            {
                return menu;
                //Debug.LogError(11111);
            }
            else
            {
                Push(identifier, initParams);
                return null;
            }

            //return menu;

        }

        public static void PopTop(eUILayer layer)
        {
            if (OpenedUIStack[(int)layer].Count <= 0)
            {
                return;
            }

            var layerGroup = OpenedUIStack[(int)layer];
            BaseUIMenu menu = layerGroup[layerGroup.Count - 1];
            menu.Pop();
        }

        public static bool PopSelf(BaseUIMenu menu, bool destroy = false)
        {
            if (OpenedUIStack[(int)menu.UILayer].Count <= 0)
            {
                return true;
            }

            var layerGroup = OpenedUIStack[(int)menu.UILayer];
            var index = layerGroup.FindIndex((x) => x == menu);
            if (index >= 0)
            {
                if (menu.UILayer == eUILayer.Menu)
                    //AFramework.Analytics.TrackingManager.I.TrackMenuActiveTime(menu.UIIdentifier, menu.MenuActiveTime);
                    layerGroup.RemoveAt(index);
                if (destroy)
                {
                    Destroy(menu.gameObject);
                }
                else
                {
                    menu.gameObject.SetActive(false);
                    UICached[menu.UIIdentifier].Push(menu);
                }

                if (EventOnMenuPopped != null)
                {
                    EventOnMenuPopped(menu);
                }

                return true;
            }

            return false;
        }

        public static bool Pop(string identifier)
        {
            BaseUIMenu menu = null;
            for (int i = 0; i <= (int)eUILayer.AlwaysOnTop && menu == null; ++i)
            {
                menu = OpenedUIStack[i].Find((x) => x.UIIdentifier == identifier);
            }

            return menu != null ? PopSelf(menu) : false;
        }

        public static void PopAllLayer(eUILayer layer)
        {
            List<BaseUIMenu> popList = new List<BaseUIMenu>(OpenedUIStack[(int)layer].ToArray());
            for (int i = popList.Count - 1; i >= 0; --i)
            {
                BaseUIMenu menu = popList[i];
                menu.Pop();
            }
        }

        public static bool IsPopupShown()
        {
            return OpenedUIStack[(int)eUILayer.Popup].Count > 0;
        }

        public static BaseUIMenu GetCurrentMenu(eUILayer topLayer = eUILayer.AlwaysOnTop)
        {
            for (int i = (int)topLayer; i >= 0; --i)
            {
                if (OpenedUIStack[i].Count > 0)
                {
                    return OpenedUIStack[i][OpenedUIStack[i].Count - 1];
                }
            }

            return null;
        }

        public static BaseUIMenu GetCurrentMenuByLayer(eUILayer layer)
        {
            int i = (int)layer;
            if (OpenedUIStack[i].Count > 0)
            {
                return OpenedUIStack[i][OpenedUIStack[i].Count - 1];
            }

            return null;
        }

        public static BaseUIMenu IsSpecificUIShown(string identifier)
        {
            for (int i = 0; i < OpenedUIStack.Count; ++i)
            {
                var currentStack = OpenedUIStack[i];
                for (int j = 0; j < currentStack.Count; ++j)
                {
                    if (currentStack[j].UIIdentifier == identifier)
                    {
                        return currentStack[j];
                    }
                }
            }

            return null;
        }

        public static int GetUIStackCount(eUILayer layer)
        {
            int i = (int)layer;
            return OpenedUIStack[i].Count;
        }

        public static BaseUIMenu GetMenu(string identifier, bool autoCreated = true)
        {
            var result = IsSpecificUIShown(identifier);
            if (result != null) return result;
            if (UICached.ContainsKey(identifier) && UICached[identifier].Count > 0)
            {
                result = UICached[identifier].Peek();
            }
            else if (autoCreated)
            {
                TryCacheUI(identifier);
                result = UICached[identifier].Peek();
            }

            return result;
        }

        public static void AddUIToCache(BaseUIMenu menu)
        {
            if (menu.UIIdentifier == null)
                menu.UIIdentifier = menu.name;
            if (!UICached.ContainsKey(menu.UIIdentifier))
                UICached[menu.UIIdentifier] = new Stack<BaseUIMenu>();
            UICached[menu.UIIdentifier].Push(menu);
            menu.gameObject.SetActive(false);
        }

        public static BaseUIMenu Init(string dataPath, string defaultMenuIdentifier)
        {
#if UNITY_EDITOR
            if (!sFinishAwake) Debug.LogError("[ERROR] CanvasManager priority is not set correctly!!!");
#endif
            DefaultDataPath = dataPath;
            return Push(defaultMenuIdentifier, null);
        }

        public static void SetRenderCamera(Camera newCamera)
        {
            _UICanvas.worldCamera = newCamera;
        }

        float mLastBackeyTime = -1;

        private void Update()
        {
            var topMenuLayer = GetCurrentMenuByLayer(eUILayer.Menu);
            if (topMenuLayer != null) topMenuLayer.UpdateActiveTime(Time.unscaledDeltaTime);

#if UNITY_ANDROID || UNITY_EDITOR
            if ((sSystemLoadingPopup == null || !sSystemLoadingPopup.activeSelf) && Application.isFocused && Input.GetKey(KeyCode.Escape) && mLastBackeyTime < Time.unscaledTime)
            {
                mLastBackeyTime = Time.unscaledTime + 0.15f;
                var topMenu = GetCurrentMenu();
                if (topMenu != null)
                {
                    topMenu.HandleSafeChoice();
                }
            }
#endif
        }

        public static GameObject sSystemLoadingPopup = null;

        public static void ShowSystemLoadingPopup(bool show)
        {
            if (sSystemLoadingPopup == null)
            {
                sSystemLoadingPopup = new GameObject("SystemLoadingPopup");
                sSystemLoadingPopup.transform.SetParent(UICanvas.transform);
                sSystemLoadingPopup.transform.localPosition = Vector2.zero;
                sSystemLoadingPopup.transform.localScale = Vector2.one;
                var rect = sSystemLoadingPopup.AddComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.anchoredPosition = Vector2.zero;
                sSystemLoadingPopup.AddComponent<SystemLoadingPopup>();
            }

            sSystemLoadingPopup.SetActive(show);
        }
        public static bool IsSystemLoadingScreenShowing() { return sSystemLoadingPopup != null ? sSystemLoadingPopup.gameObject.activeSelf : false; }

        public static void DestroyAllUICanDestroy()
        {
            List<KeyValuePair<string, Stack<BaseUIMenu>>> listClear =
                new List<KeyValuePair<string, Stack<BaseUIMenu>>>();
            foreach (var group in UICached)
            {
                var list = new List<BaseUIMenu>();
                while (group.Value.Count > 0)
                {
                    var menu = group.Value.Pop();

                    var check = OpenedUIStack[(int)menu.UILayer].Contains(menu);
                    if (menu.CanDestroy && !check)
                    {
                        listClear.Add(group);
                        Destroy(menu.gameObject);
                    }
                    else
                    {
                        Debug.Log(menu.UIIdentifier);
                        list.Add(menu);
                    }
                }

                foreach (var menu in list)
                    group.Value.Push(menu);
            }

            foreach (var pair in listClear)
            {
                if (pair.Value.Count <= 0)
                {
                    // Debug.Log("Destroy " + pair.Key);
                    UICached.Remove(pair.Key);
                }
            }
        }

        public static BaseUIMenu[] GetAllOpenedUI()
        {
            List<BaseUIMenu> result = new List<BaseUIMenu>();
            for (int i = 0; i < OpenedUIStack.Count; ++i)
            {
                var childList = OpenedUIStack[i];
                for (int j = 0; j < childList.Count; ++j)
                {
                    result.Add(childList[j]);
                }
            }
            return result.ToArray();
        }
    }
}
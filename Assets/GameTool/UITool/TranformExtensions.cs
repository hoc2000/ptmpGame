using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Tool.ExtensionMethods
{
    public static class TranformExtensions
    {

        public static T[] GetComponentsInChildrenFD<T>(this Transform trans)
        {
            List<T> list = new List<T>();
            T component;
            for (int i = 0; i < trans.childCount; i++)
            {
                component = trans.GetChild(i).GetComponent<T>();
                if (component != null)
                {
                    list.Add(component);
                }
            }

            return list.ToArray();
        }

        public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            aName = aName.ToLower();
            foreach (Transform child in aParent)
            {
                if (child.name.ToLower() == aName)
                    return child;
                var result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static Transform FindDeepChildLower(this Transform aParent, string aName)
        {
            aName = aName.ToLower();
            foreach (Transform child in aParent)
            {
                if (child.name.ToLower() == aName)
                    return child;
                var result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static GameObject[] FindDeepChildsWithStartName(this Transform aParent, string startName)
        {
            startName = startName.ToLower();
            List<GameObject> result = new List<GameObject>();
            foreach (Transform child in aParent)
            {
                if (child.name.ToLower().StartsWith(startName))
                {
                    result.Add(child.gameObject);
                }
                else
                {
                    var childResult = child.FindDeepChildsWithStartName(startName);
                    result.AddRange(childResult);
                }
            }
            return result.ToArray(); ;
        }

        public static Transform FindDeepChildWithStartName(this Transform aParent, string startName)
        {
            startName = startName.ToLower();
            foreach (Transform child in aParent)
            {
                if (child.name.ToLower().StartsWith(startName))
                    return child;
                var result = child.FindDeepChildWithStartName(startName);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static GameObject[] FindChildsSameDeep(this Transform trans, string startName, bool includeInactive)
        {
            Transform result = FindDeepChildWithStartName(trans, startName);
            List<GameObject> list = new List<GameObject>();
            if (result != null)
            {
                Transform aParent = result.parent;
                Transform obj;
                for (int i = 0; i < aParent.childCount; i++)
                {
                    obj = aParent.GetChild(i);
                    if ((!includeInactive || obj.gameObject.activeSelf) && obj.name.StartsWith(startName))
                    {
                        list.Add(obj.gameObject);
                    }
                }
            }

            return list.ToArray();
        }

        public static int GetChildCount(this Transform trans, bool includeInactive)
        {
            if (includeInactive)
            {
                return trans.childCount;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < trans.childCount; ++i)
                {
                    if (trans.GetChild(i).gameObject.activeSelf)
                    {
                        ++count;
                    }
                }
                return count;
            }
        }
    }
}
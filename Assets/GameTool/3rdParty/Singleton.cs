using UnityEngine;
using System;

namespace My.Tool
{
    /// <summary>
    /// Singleton base class
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static readonly T singleton = new T();

        public static T instance
        {
            get
            {
                return singleton;
            }
        }

        public static T I
        {
            get { return instance; }
        }
    }

    /// <summary>
    /// Singleton for mono behavior object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T singleton;

        public static bool IsInstanceValid() { return singleton != null; }

        void Reset()
        {
            gameObject.name = typeof(T).Name;
        }

        public static T instance
        {
            get
            {
                if (SingletonMono<T>.singleton == null)
                {
                    SingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
                    if (SingletonMono<T>.singleton == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "[@" + typeof(T).Name + "]";
                        SingletonMono<T>.singleton = obj.AddComponent<T>();
                    }
                }

                return SingletonMono<T>.singleton;
            }
        }

        public static T I
        {
            get { return instance; }
        }

    }

    /// <summary>
    /// Singleton for mono behavior object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ManualSingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; private set; }

        public static bool IsInstanceValid() { return instance != null; }

        public static T I
        {
            get { return instance; }
        }

        void Reset()
        {
            gameObject.name = typeof(T).Name;
        }

        protected virtual void Awake()
        {
            if (instance == null)
                instance = (T)(MonoBehaviour)this;
        }

        protected void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }

    public class SingletonUI<T> : UI.BaseUIComp where T : UI.BaseUIComp
    {
        private static T singleton;

        public static bool IsInstanceValid() { return singleton != null; }

        void Reset()
        {
            gameObject.name = typeof(T).Name;
        }

        public static T instance
        {
            get
            {
                if (SingletonUI<T>.singleton == null)
                {
                    SingletonUI<T>.singleton = (T)FindObjectOfType(typeof(T));
                    if (SingletonUI<T>.singleton == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "[@" + typeof(T).Name + "]";
                        SingletonUI<T>.singleton = obj.AddComponent<T>();
                    }
                }

                return SingletonUI<T>.singleton;
            }
        }

        public static T I
        {
            get { return instance; }
        }
    }
}


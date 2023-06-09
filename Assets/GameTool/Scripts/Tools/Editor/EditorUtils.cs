﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

namespace EditorUtils
{
    public class EditorCoroutine
    {
        public static EditorCoroutine start(IEnumerator _routine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            coroutine.start();
            return coroutine;
        }

        readonly IEnumerator routine;
        EditorCoroutine(IEnumerator _routine)
        {
            routine = _routine;
        }

        public void start()
        {
            EditorApplication.update += update;
            isPlaying = true;
        }
        public void stop()
        {
            EditorApplication.update -= update;
            isPlaying = false;
        }

        bool isPlaying = false;
        public bool IsPlaying()
        {
            return isPlaying;
        }

        void update()
        {
            if (!routine.MoveNext())
            {
                stop();
            }
        }
    }

    public abstract class MetaEditor : Editor
    {
        public SerializedObject mySerializedObject;
        protected virtual void OnEnable()
        {

            mySerializedObject = new SerializedObject(metaTarget);
        }

        public Object metaTarget
        {
            get
            {
                try
                {
                    return target;
                }
                catch (System.Exception)
                {
                    return FindTarget();
                }
            }
        }
        public ReorderableList GetReorderableList(SerializedProperty property)
        {
            ReorderableList result = null;
            result = new ReorderableList(property.serializedObject, property, true, true, true, true);
            return result;

        }
        public abstract Object FindTarget();

        public System.Action onRepaint = delegate { };

        public void RepaintIt()
        {
            Repaint();
            onRepaint.Invoke();
        }
    }

    public class PrefVariable
    {
        string key = "";
        public PrefVariable(string _key)
        {
            key = _key;
        }

        public int Int
        {
            get
            {
                return EditorPrefs.GetInt(key);
            }
            set
            {
                EditorPrefs.SetInt(key, value);
            }
        }

        public float Float
        {
            get
            {
                return EditorPrefs.GetFloat(key);
            }
            set
            {
                EditorPrefs.SetFloat(key, value);
            }
        }

        public string String
        {
            get
            {
                return EditorPrefs.GetString(key);
            }
            set
            {
                EditorPrefs.SetString(key, value);
            }
        }

        public bool Bool
        {
            get
            {
                return EditorPrefs.GetBool(key);
            }
            set
            {
                EditorPrefs.SetBool(key, value);
            }
        }
    }
}

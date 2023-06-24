using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool.ExtensionMethods;
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

namespace My.Tool.UI
{
    public class BaseUIComp : MonoBehaviour
    {
#if UNITY_EDITOR
        // This method is called once when we add component do game object
        public void AutoReference()
        {
            bool hasChange = false;
            // Magic of reflection
            // For each field in your class/component we are looking only for those that are empty/null
            foreach (var field in this.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)/*.Where(field => field.GetValue(this) == null)*/)
            {
                if (field.IsStatic || field.IsNotSerialized) continue;

                if (field.FieldType.IsArray)
                {
                    try
                    {
                        System.Array array;
                        if (field.FieldType.GetElementType() == typeof(GameObject))
                        {
                            GameObject[] holder = transform.FindDeepChildsWithStartName(field.Name);

                            array = System.Array.CreateInstance(typeof(GameObject), holder.Length);
                            for (int i = 0; i < holder.Length; i++)
                            {
                                array.SetValue(holder[i].gameObject, i);
                            }
                        }
                        else
                        {
                            var data = transform.GetComponentsInChildren(field.FieldType.GetElementType(), true).ToList();
                            for (int i = 0; i < data.Count; i++)
                            {
                                if (!data[i].name.StartsWith(field.Name))
                                {
                                    data.RemoveAt(i);
                                    --i;
                                }
                            }
                            array = System.Array.CreateInstance(field.FieldType.GetElementType(), data.Count);
                            for (int i = 0; i < data.Count; i++)
                            {
                                array.SetValue(data[i], i);
                            }
                        }
                        field.SetValue(this, array);
                        hasChange = true;
                    }
                    catch (System.Exception e)
                    {

                    }
                    continue;
                }

                // Now we are looking for object (self or child) that have same name as a field
                Transform obj;
                /*if (transform.name == field.Name)
                {
                    obj = transform;
                }
                else*/
                {
                    obj = transform.FindDeepChildLower(field.Name);// Or you need to implement recursion to looking into deeper childs
                }

                // If we find object that have same name as field we are trying to get component that will be in type of a field and assign it
                if (obj != null)
                {
                    if (field.FieldType == typeof(GameObject))
                    {
                        field.SetValue(this, obj.gameObject);
                    }
                    else
                    {
                        field.SetValue(this, obj.GetComponent(field.FieldType));
                    }
                    hasChange = true;
                }
            }
            if (hasChange)
            {
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [UnityEditor.CustomEditor(typeof(BaseUIComp), true)]
    public class BaseUICompEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            OnCustomInspectorGUI();
        }

        protected virtual void OnCustomInspectorGUI()
        {
            if (GUILayout.Button("Auto Reference"))
            {
                foreach (BaseUIComp gameObject in targets)
                    gameObject.AutoReference();
            }
        }
    }
#endif
}
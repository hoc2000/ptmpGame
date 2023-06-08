using UnityEngine;
using System;
using System.Collections.Generic;
namespace My.Tool
{
    public static class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            WrapperArray<T> wrapper = JsonUtility.FromJson<WrapperArray<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        public class WrapperArray<T>
        {
            public T[] array;
        }

        public static List<T> getJsonList<T>(string json)
        {
            string newJson = "{ \"list\": " + json + "}";
            WrappeList<T> wrapper = JsonUtility.FromJson<WrappeList<T>>(newJson);
            return wrapper.list;
        }

        [Serializable]
        public class WrappeList<T>
        {
            public List<T> list;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public List<T> Items;
        }

        public static List<T> FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(List<T> array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(List<T> array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }
    }
}
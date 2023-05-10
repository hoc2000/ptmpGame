using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if (UNITY_WINRT && !UNITY_EDITOR)
using Reflection = MarkerMetro.Unity.WinLegacy.Reflection.ReflectionExtensions;
#endif
namespace My.Tool
{
    public static class Utility
    {
        public static string TimeFormat(float time)
        {
            int timeInt = Mathf.CeilToInt(time);
            int hour = timeInt / 3600;
            int minute = (timeInt % 3600) / 60;
            int second = timeInt - hour * 3600 - minute * 60;

            return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }

        public static System.DateTime GetCurrentTime()
        {
            return System.DateTime.UtcNow;
        }

        public static System.DateTime GetCurrentDate()
        {
            return GetCurrentTime().Date;
        }

        public static double GetCurrentTimeMillisecond()
        {
            return System.DateTime.UtcNow.Subtract(System.DateTime.MinValue).TotalMilliseconds;
        }

        public static double GetCurrentTimeSecond()
        {
            return System.DateTime.UtcNow.Subtract(System.DateTime.MinValue).TotalSeconds;
        }
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        private static int sVersionNumber = -1;
        public static int GetVersionNumber()
        {
            if (sVersionNumber < 0)
            {
                var versionArray = Application.version.Split('.');
                sVersionNumber = 0;
                int multiplier = 1;
                for (int i = versionArray.Length - 1; i >= 0; --i)
                {
                    sVersionNumber += int.Parse(versionArray[i]) * multiplier;
                    multiplier *= 10;
                }
            }
            return sVersionNumber;
        }

        private static string sDeviceUDID = "";
        public static string GetUDID()
        {
            if (sDeviceUDID.Length <= 0)
            {
                sDeviceUDID = SystemInfo.deviceUniqueIdentifier;
            }
            return sDeviceUDID;
        }

        private static string sSavePath = "";
        public static string GetSavePath()
        {
            if (sSavePath.Length <= 0)
            {
                sSavePath = Application.persistentDataPath + '/';
            }
            return sSavePath;
        }

        public static string GetUrlFilename(string url)
        {
            if (url == null) return null;
            int questionMark = url.IndexOf('?');
            var temp = questionMark >= 0 ? url.Remove(questionMark) : url;
            var lastSlast = temp.LastIndexOf('/');
            if (lastSlast < 0) return null;
            return temp.Substring(lastSlast + 1);
        }

        public static TValue GetValueOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
             TKey key,
             TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static bool HasInternet()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;//TODO
        }

        static bool sLastInternetState = true;
        static float sLastInternetCheckTime = -999;
        public static bool DelayHasInternet()
        {
            if (Mathf.Abs(Time.fixedUnscaledTime - sLastInternetCheckTime) >= 3)
            {
                sLastInternetCheckTime = Time.fixedUnscaledTime;
                sLastInternetState = HasInternet();
            }
            return sLastInternetState;
        }

        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "";
        }

        public static float NormalizeFloat(float value, int fractNum)
        {
            return Mathf.RoundToInt(value * Mathf.Pow(10, fractNum)) / Mathf.Pow(10, fractNum);
        }

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
            return dst as T;
        }

        public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
        {
            // the vector that we want to measure an angle from
            Vector3 referenceForward = vec1;
            Vector3 referenceRight = Vector3.Cross(Vector3.up, referenceForward);
            // the vector of interest
            Vector3 newDirection = vec2;
            float angle = Vector3.Angle(newDirection, referenceForward);
            // Determine if the degree value should be negative.  Here, a positive value
            // from the dot product means that our vector is on the right of the reference vector   
            // whereas a negative value means we're on the left.
            float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));
            return sign * angle;
        }

        public static float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
        {
            // angle in [0,180]
            float angle = Vector3.Angle(a, b);
            float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

            // angle in [-179,180]
            float signed_angle = angle * sign;

            // angle in [0,360] (not used but included here for completeness)
            //float angle360 =  (signed_angle + 180) % 360;

            return signed_angle;
        }

        public static Vector2 WorldToScreenPointProjected(Camera camera, Vector3 worldPos)
        {
            Vector3 camNormal = camera.transform.forward;
            Vector3 vectorFromCam = worldPos - camera.transform.position;
            float camNormDot = Vector3.Dot(camNormal, vectorFromCam);
            if (camNormDot <= 0)
            {
                // we are behind the camera forward facing plane, project the position in front of the plane
                Vector3 proj = (camNormal * camNormDot * 1.01f);
                worldPos = camera.transform.position + (vectorFromCam - proj);
            }

            return RectTransformUtility.WorldToScreenPoint(camera, worldPos);
        }

        public static bool IsPrefab(GameObject obj)
        {
            return obj.scene.rootCount == 0;
        }

        public static void ShuffleList<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        public static T[] ResizeArray<T>(T[] input, int length, T defaultValue)
        {
            T[] output = new T[length];
            int index = 0;
            for (; index < input.Length && index < length; ++index)
            {
                output[index] = input[index];
            }
            for (; index < length; ++index)
            {
                output[index] = defaultValue;
            }
            return output;
        }

        public static bool CompareArray<T>(T[] arrayA, T[] arrayB)
        {
            if ((arrayA == null && arrayB != null) || (arrayA != null && arrayB == null)) return false;
            if (arrayA.Length != arrayB.Length) return false;

            for (int i = 0; i < arrayA.Length; i++)
            {
                if (!arrayA[i].Equals(arrayB[i])) return false;
            }

            return true;
        }

        //Usage GetParameterNameXXXX(new { variable });
        public static string GetParameterNameSlow<T>(T item) where T : class
        {
            if (item == null)
                return string.Empty;

            return item.ToString().TrimStart('{').TrimEnd('}').Split('=')[0].Trim();
        }
        public static string GetParameterNameFast<T>(T item) where T : class
        {
            if (item == null)
                return string.Empty;

            return typeof(T).GetProperties()[0].Name;
        }

        public static void CopyRectTransform(RectTransform from, RectTransform to)
        {
            to.anchorMin = from.anchorMin;
            to.anchorMax = from.anchorMax;
            to.anchoredPosition = from.anchoredPosition;
            to.sizeDelta = from.sizeDelta;
        }

#if UNITY_EDITOR
        public static List<T> LoadAllPrefabsOfType<T>(string path) where T : UnityEngine.Object
        {
            if (path != "")
            {
                if (path.EndsWith("/"))
                {
                    path = path.TrimEnd('/');
                }
            }

            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

            //loop through directory loading the game object and checking if it has the component you want
            List<T> prefabComponents = new List<T>();
            foreach (System.IO.FileInfo fileInfo in fileInf)
            {
                string fullPath = fileInfo.FullName.Replace(@"\", "/");
                string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
                Object data = UnityEditor.AssetDatabase.LoadMainAssetAtPath(assetPath);
                if (data is T)
                {
                    prefabComponents.Add(data as T);
                }
            }
            return prefabComponents;
        }
#endif

        public static IEnumerator CRDelayFunction(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            if (callback != null) callback();
        }

        public static Mesh CreateCube(Vector3 size, bool save = false)
        {
            float length = size.x;
            float width = size.y;
            float height = size.z;
            Vector3[] c = new Vector3[8];
            c[0] = new Vector3(-length * .5f, -width * .5f, height * .5f);
            c[1] = new Vector3(length * .5f, -width * .5f, height * .5f);
            c[2] = new Vector3(length * .5f, -width * .5f, -height * .5f);
            c[3] = new Vector3(-length * .5f, -width * .5f, -height * .5f);

            c[4] = new Vector3(-length * .5f, width * .5f, height * .5f);
            c[5] = new Vector3(length * .5f, width * .5f, height * .5f);
            c[6] = new Vector3(length * .5f, width * .5f, -height * .5f);
            c[7] = new Vector3(-length * .5f, width * .5f, -height * .5f);

            Vector3[] vertices = {
                c[0], c[1], c[2], c[3], // Bottom
	            c[7], c[4], c[0], c[3], // Left
	            c[4], c[5], c[1], c[0], // Front
	            c[6], c[7], c[3], c[2], // Back
	            c[5], c[6], c[2], c[1], // Right
                c[7], c[6], c[5], c[4] // Top
            };

            int[] triangles = {
                3, 1, 0,        3, 2, 1,        // Bottom	
	            7, 5, 4,        7, 6, 5,        // Left
	            11, 9, 8,       11, 10, 9,      // Front
	            15, 13, 12,     15, 14, 13,     // Back
	            19, 17, 16,     19, 18, 17,	    // Right
                23, 21, 20,     23, 22, 21, // Top
            };

            //5) Define each vertex's Normal
            Vector3 up = Vector3.up;
            Vector3 down = Vector3.down;
            Vector3 forward = Vector3.forward;
            Vector3 back = Vector3.back;
            Vector3 left = Vector3.left;
            Vector3 right = Vector3.right;


            Vector3[] normals = new Vector3[]
            {
                down, down, down, down,             // Bottom
	            left, left, left, left,             // Left
	            forward, forward, forward, forward,	// Front
	            back, back, back, back,             // Back
	            right, right, right, right,         // Right
	            up, up, up, up                      // Top
            };

            //6) Define each vertex's UV co-ordinates
            Vector2 uv00 = new Vector2(0f, 0f);
            Vector2 uv10 = new Vector2(1f, 0f);
            Vector2 uv01 = new Vector2(0f, 1f);
            Vector2 uv11 = new Vector2(1f, 1f);

            Vector2[] uvs = new Vector2[]
            {
                uv11, uv01, uv00, uv10, // Bottom
	            uv11, uv01, uv00, uv10, // Left
	            uv11, uv01, uv00, uv10, // Front
	            uv11, uv01, uv00, uv10, // Back	        
	            uv11, uv01, uv00, uv10, // Right 
	            uv11, uv01, uv00, uv10  // Top
            };

            Mesh newMesh = new Mesh();

            newMesh.Clear();

            newMesh.vertices = vertices;
            newMesh.triangles = triangles;
            newMesh.normals = normals;
            newMesh.uv = uvs;

#if UNITY_EDITOR
            UnityEditor.MeshUtility.Optimize(newMesh);
            if (save)
            {
                save = false;

                var savePath = "Assets/" + string.Format("Cube_{0}", System.DateTime.UtcNow.Ticks) + ".asset";
                Debug.Log("Saved Mesh to:" + savePath);
                UnityEditor.AssetDatabase.CreateAsset(newMesh, savePath);
            }
#endif
            return newMesh;
        }

        public static string GetGoogleSheetCSVData(string fileId, string sheetId)
        {
            try
            {
                var httpClient = new System.Net.Http.HttpClient();
                httpClient.BaseAddress = new System.Uri(string.Format("https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}", fileId, sheetId));
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/csv"));
                var response = httpClient.GetAsync("").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (System.Exception e) { return ""; }
            return "";
        }
    }

    public static class Encoding
    {
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }

    public static class ReflectionWrapper
    {
        public static FieldInfo GetField(ref System.Type type, string name)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.GetField(type, name);
#else
            return type.GetField(name);
#endif
        }

        public static FieldInfo[] GetFields(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.GetFields(type);
#else
            return type.GetFields();
#endif
        }

        public static bool IsClass(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.IsClass(type);
#else
            return type.IsClass;
#endif
        }

        public static bool IsEnum(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.IsEnum(type);
#else
            return type.IsEnum;
#endif
        }

        public static bool IsValueType(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.IsValueType(type);
#else
            return type.IsValueType;
#endif
        }

        public static System.Type GetBaseType(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.GetBaseType(type);
#else
            return type.BaseType;
#endif
        }

        public static MemberInfo[] GetMembers(ref System.Type type)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.GetMembers(type);
#else
            return type.GetMembers();
#endif
        }

        public static bool IsAssignableFrom(ref System.Type current, ref System.Type toCompare)
        {
#if (UNITY_WINRT && !UNITY_EDITOR)
            return Reflection.IsAssignableFrom(current, toCompare);
#else
            return current.IsAssignableFrom(toCompare);
#endif
        }
    }
}
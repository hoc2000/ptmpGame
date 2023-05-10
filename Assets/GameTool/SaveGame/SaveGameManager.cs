using UnityEngine;
using System.Collections;
using My.Tool;
using System.IO;
using System.Collections.Generic;
using com.spacepuppy.Collections;

namespace My.Tool
{
    public interface ISaveData
    {
        object GetData();
        void SetData(string data);
        void OnAllDataLoaded();
        void RegisterSaveData();
        bool DataChanged { get; set; }
    }

    public class SaveGameManager : Tool.Singleton<SaveGameManager>
    {
        [System.Serializable]
        public class SaveGameDictionary : SerializableDictionaryBase<string, ISaveData> { }
        [System.Serializable]
        public class LoadGameDictionary : SerializableDictionaryBase<string, string> { }

        const string MANDATORY_SAVE_NAME = "mwovjtpamcjaytifnhyqlbprths";
        const string OPTIONAL_SAVE_NAME = "nalgowuthvnapqyewngoapwvz";

        public delegate object ObjectDataCallback();
        public delegate void StringDataCallback(string data);

        [UnityEngine.SerializeField()]
        private SaveGameDictionary mMandatory = new SaveGameDictionary();
        [UnityEngine.SerializeField()]
        private SaveGameDictionary mOptional = new SaveGameDictionary();

        public void RegisterMandatoryData(string name, ISaveData data)
        {
            mMandatory[name] = data;
        }

        public void RegisterOptionalData(string name, ISaveData data)
        {
            mOptional[name] = data;
        }

        public void Save(bool mandatory = true, bool optional = true, bool hasBackup = true)
        {
#if USE_GPGS_SAVEGAME
            if(GooglePlayServiceManager.instance.OnLoadFromCloud)
            {
                return;
            }
#endif
            if (mandatory)
            {
                try
                {
                    bool hasChanged = false;
                    foreach (string key in mMandatory.Keys)
                    {
                        hasChanged |= mMandatory[key].DataChanged;
                    }

                    if (hasChanged)
                    {
                        LoadGameDictionary temp = new LoadGameDictionary();
                        bool checkValid = false;
                        foreach (string key in mMandatory.Keys)
                        {
                            temp[key] = JsonUtility.ToJson(mMandatory[key].GetData());
                            checkValid = true;
                            mMandatory[key].DataChanged = false;
                        }

                        if (checkValid)
                        {
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(temp));
                            SaveToFile(MANDATORY_SAVE_NAME, data, hasBackup);
                        }
                    }
                }
                catch (System.Exception)
                {
                }
            }

            if (optional)
            {
                try
                {
                    bool hasChanged = false;
                    foreach (string key in mOptional.Keys)
                    {
                        hasChanged |= mOptional[key].DataChanged;
                    }

                    if (hasChanged)
                    {
                        LoadGameDictionary temp = new LoadGameDictionary();
                        bool checkValid = false;
                        foreach (string key in mOptional.Keys)
                        {
                            temp[key] = JsonUtility.ToJson(mOptional[key].GetData());
                            checkValid = true;
                            mOptional[key].DataChanged = false;
                        }

                        if (checkValid)
                        {
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(temp));
                            SaveToFile(OPTIONAL_SAVE_NAME, data, hasBackup);
                        }
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }

        public void Load(bool mandatory = true, bool optional = true, bool notification = true)
        {
            LoadGameDictionary loadDictionary = null;
            if (mandatory)
            {
                try
                {
                    byte[] data = null;
                    if (!LoadFromFile(MANDATORY_SAVE_NAME, ref data, true))
                    {
                        LoadFromFile("_" + MANDATORY_SAVE_NAME, ref data, true);
                    }
                    loadDictionary = JsonUtility.FromJson<LoadGameDictionary>(data == null ? "{}" : System.Text.Encoding.UTF8.GetString(data, 0, data.Length));
                }
                catch (System.Exception)
                {
                    loadDictionary = null;
                }
                foreach (string key in mMandatory.Keys)
                {
                    mMandatory[key].SetData(loadDictionary != null && loadDictionary.ContainsKey(key) && loadDictionary[key] != null ? loadDictionary[key] : "");
                }
            }

            if (optional)
            {
                try
                {
                    byte[] data = null;
                    if (!LoadFromFile(OPTIONAL_SAVE_NAME, ref data, false))
                    {
                        LoadFromFile("_" + OPTIONAL_SAVE_NAME, ref data, false);
                    }
                    loadDictionary = JsonUtility.FromJson<LoadGameDictionary>(data == null ? "{}" : System.Text.Encoding.UTF8.GetString(data, 0, data.Length));
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                    loadDictionary = null;
                }
                foreach (string key in mOptional.Keys)
                {
                    mOptional[key].SetData(loadDictionary != null && loadDictionary.ContainsKey(key) && loadDictionary[key] != null ? loadDictionary[key] : "");
                }
            }
            if (notification)
            {
                if (mandatory)
                {
                    foreach (var item in mMandatory.Values)
                    {
                        item.OnAllDataLoaded();
                    }
                }
                if (optional)
                {
                    foreach (var item in mOptional.Values)
                    {
                        item.OnAllDataLoaded();
                    }
                }
            }
        }

        public bool SaveToFile(string fileName, byte[] data, bool hasBackup = true)
        {
            try
            {
                string savepath = Tool.Utility.GetSavePath();
                if (hasBackup)
                {
                    if (File.Exists(savepath + "_" + fileName))
                    {
                        File.Delete(savepath + "_" + fileName);
                    }
                    if (File.Exists(savepath + fileName))
                    {
                        File.Move(savepath + fileName, savepath + "_" + fileName);
                    }
                }

                //simple encrypt using UDID/decrypt
                SimpleEncrypt(ref data);

                File.WriteAllBytes(savepath + fileName, data);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
            return true;
        }

        //public bool LoadMandatoryFile(ref byte[] data)
        //{
        //    return LoadFromFile(MANDATORY_SAVE_NAME, ref data);
        //}

        //public bool SaveToMandatoryFile(byte[] data)
        //{
        //    return SaveToFile(MANDATORY_SAVE_NAME, data);
        //}

        public bool LoadFromFile(string fileName, ref byte[] data, bool hasBackup = false)
        {
            try
            {
                string savepath = Tool.Utility.GetSavePath();
                if (File.Exists(savepath + fileName))
                {
                    data = File.ReadAllBytes(savepath + fileName);
                }
                else if (File.Exists(savepath + "_" + fileName))
                {
                    data = File.ReadAllBytes(savepath + "_" + fileName);
                }
                else
                {
                    return false;
                }

                SimpleEncrypt(ref data);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
            return true;
        }

        void SimpleEncrypt(ref byte[] data)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes(Tool.Utility.GetUDID());
            for (uint i = 0; i < data.Length; i++)
                data[i] ^= key[i % key.Length];
        }

        public string StringToEncryptBase64(string data)
        {
            byte[] encryptData = System.Text.Encoding.UTF8.GetBytes(data);
            SimpleEncrypt(ref encryptData);
            return System.Convert.ToBase64String(encryptData);
        }

        public string EncryptBase64ToString(string data)
        {
            try
            {
                byte[] decryptData = System.Convert.FromBase64String(data);
                SimpleEncrypt(ref decryptData);
                return System.Text.Encoding.UTF8.GetString(decryptData, 0, decryptData.Length);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return "";
            }
        }

        public void DeleteAll()
        {
            DeleteSave(MANDATORY_SAVE_NAME);
            DeleteSave(OPTIONAL_SAVE_NAME);
            mMandatory.Clear();
            mOptional.Clear();
        }

        public bool DeleteSave(string fileName)
        {
            try
            {
                string savepath = Tool.Utility.GetSavePath();
                if (File.Exists(savepath + fileName))
                {
                    File.Delete(savepath + fileName);
                }
                if (File.Exists(savepath + "_" + fileName))
                {
                    File.Delete(savepath + "_" + fileName);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
            return true;
        }
    }
}
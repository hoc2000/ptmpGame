using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MoreGameButton : MonoBehaviour
{
  
	[SerializeField]
	RawImage[] icons;
	[SerializeField]
	Text[] names;
	[SerializeField]
	Button[] buttons;


	void Start ()
	{
		
		StartCoroutine (GetMoreGame ());
	}

	IEnumerator GetMoreGame ()
	{
		string urlJson = "https://dl.dropboxusercontent.com/s/fiposty0ywaiygk/line-balls-export.json?dl=0";
		WWW request = new WWW (urlJson);
		yield return request;

		if (request.error == null) {
			ConfigData data = JsonUtility.FromJson<ConfigData> (request.text);

			UpdateData (0, data.featured);

			UpdateData (1, data.moreGames[Random.Range(0,data.moreGames.Length)]);

			UpdateData (2, data.moreGames[Random.Range(0,data.moreGames.Length)]);
		}
	}

	void UpdateData (int index, MoreGameInfo info)
	{
		StartCoroutine (LoadImage (index,info.icon));

		buttons [index].onClick.AddListener (() => {
			OnPressMoreGameButton (info.bundleID);
		});
		names [index].text = info.name;	
	}

	IEnumerator LoadImage (int index, string url)
	{
		WWW request = new WWW (url);
		yield return request;

		if (request.error == null) {			
			icons [index].texture = request.texture;
			icons [index].enabled = true;
		}
	}

	public void OnPressMoreGameButton (string androidPackage)
	{
		Debug.Log (androidPackage);
#if UNITY_IOS

        //Application.OpenURL("https://itunes.apple.com/app/id"+iosAppID);

#elif UNITY_ANDROID
        Application.OpenURL("market://details?id=" + androidPackage);
#endif
	}


}

[System.Serializable]
public class ConfigData
{
	public MoreGameInfo featured;
	public MoreGameInfo[] moreGames;
}

[System.Serializable]
public class MoreGameInfo
{
	public string bundleID;
	public string name;
	public string icon;
	public string cover;
}
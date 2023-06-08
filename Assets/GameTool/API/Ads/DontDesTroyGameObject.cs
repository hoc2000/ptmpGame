 
using System;
using UnityEngine;

public class DontDesTroyGameObject : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
	}
}

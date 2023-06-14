using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAnim : MonoBehaviour
{
    public float textureScrollSpeed = 8f;
    //public float textureLengthScale = 3;
    //public float distance = 30;
    public Material mat;

    //public Vector2 ScrollSpeed;

    private void OnEnable()
    {
        //GetComponent<SpriteRenderer>().material.SetVector("_ScrollSpeed", ScrollSpeed);
        mat.mainTextureOffset = new Vector2(0, 0);
    }
    //void Start()
    //{
    //    //mat = gameObject.GetComponent<Material>();
    //}

    //// Update is called once per frame
    void Update()
    {
        //mat.mainTextureScale = new Vector2(1, 1);// distance / textureLengthScale);;
        mat.mainTextureOffset += new Vector2(0, Time.deltaTime * textureScrollSpeed);
    }
}

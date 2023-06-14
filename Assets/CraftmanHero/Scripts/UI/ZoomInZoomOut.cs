using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInZoomOut : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    public float zoomAnt;


    private void Start()
    {
        mainCam.GetComponent<Camera>();
    }
    private void Update()
    {
        mainCam.orthographicSize = zoomAnt;
    }

    public void SliderZoom(float zoom)
    {
        zoomAnt = zoom;
    }
}

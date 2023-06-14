using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNormal : MonoBehaviour
{
    public Transform startTrans, endTrans;
    public GameObject laserMiddle;

    public bool isEnable;

    public float timeShow;
    public float timeDeActive;

    public GameObject beamStart;
    public GameObject beamEnd;
    public GameObject beam;
    public LineRenderer line;

    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;
    public float textureLengthScale = 3;

    private void Start()
    {
        laserMiddle.SetActive(false);
        StartCoroutine(ShootingLaser());
    }
    public IEnumerator ShootingLaser()
    {
        yield return new WaitForSeconds(timeDeActive);
        laserMiddle.SetActive(true);
        yield return new WaitForSeconds(timeDeActive);
        laserMiddle.SetActive(false);
        StartCoroutine(ShootingLaser());

    }
    private void Update()
    {
        ShootLaze();
    }
    //private void Update()
    //{
    //    time += Time.deltaTime;

    //    if(time >= 3f)
    //    {
    //        laserMiddle.SetActive(true);
    //        if(time >= 6f)
    //        {
    //            laserMiddle.SetActive(false);
    //            time = 0f;
    //        }
    //    }
    //}

    void ShootLaze()
    {
        ShootBeamInDir(startTrans.position, endTrans.position);
    }
    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = dir;

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
}

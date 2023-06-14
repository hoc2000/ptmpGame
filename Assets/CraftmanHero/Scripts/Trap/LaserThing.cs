using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserThing : MonoBehaviour
{
    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public enum LaserType
    {
        NORMAL,
        RANDOM,
        NO_ROTATE,
        NO_ROTATE_TIMER
    }
    public float speed;
    public float leftMaxAngle = -65f;
    public float rightMaxAngle = 65f;
    public LaserType laserType = LaserType.NORMAL;
    public LineRenderer lineRenderer;
    public Transform rotatePoint;
    public Transform laserStart;
    public GameObject hitEffect;
    public GameObject hitEffectCreate;
    Direction rotateDirection;

    float angle = 0;

    public bool isDisable;

    bool getRandom;
    float randomTimer;
    bool canRotate;
    public float _activateTime = 5;
    float activateTime = 5;
    bool waiting;
    public float _waitingTime = 2;
    float waitingTime = 2;
    bool shootable;

    public float shootingTimer = 4;

    [SerializeField] float disLaser = 10;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask noPlayerMask;

    public int[] hitLayers = { 9, 10 };
    void Start()
    {
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position });
        rotateDirection = Direction.LEFT;
        isDisable = false;
        hitEffectCreate = Instantiate(hitEffect);
        hitEffectCreate.SetActive(false);
        shootable = true;
        if (laserType == LaserType.NO_ROTATE_TIMER)
        {
            StartCoroutine(CountdownShootable());
        }
    }
    IEnumerator CountdownShootable()
    {
        while (true)
        {
            if (Mathf.Abs(transform.position.x - PlayerPos.instance.transform.position.x) < 20)
            {
                shootable = true;
                lineRenderer.gameObject.SetActive(true);
                hitEffectCreate.SetActive(true);
            }
            yield return new WaitForSeconds(shootingTimer);
            shootable = false;
            lineRenderer.gameObject.SetActive(false);
            hitEffectCreate.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
    }
    public void OnTurningOffLaser()
    {
        isDisable = true;
        lineRenderer.gameObject.SetActive(false);
        hitEffectCreate.SetActive(false);
    }
    public void OnTurningOnLaser()
    {
        isDisable = false;
        lineRenderer.gameObject.SetActive(true);
        hitEffectCreate.SetActive(true);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - PlayerPos.instance.transform.position.x) < 20)
        {
            if (!isDisable)
            {
                if (laserType == LaserType.NORMAL)
                {
                    Rotate();
                }
                else if (laserType == LaserType.RANDOM)
                {
                    Random2Rotate();
                }
                else
                {

                }
                if (shootable)
                {
                    ShootingLaser();
                }
            }

        }
    }
    void Rotate()
    {
        if (rotateDirection == Direction.LEFT)
        {
            if (!waiting)
            {
                angle -= Time.deltaTime * speed;
                rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
            }
            if (angle < leftMaxAngle)
            {
                waiting = true;
                if (waiting)
                {
                    waitingTime -= Time.deltaTime;
                    if (waitingTime <= 0)
                    {
                        waitingTime = _waitingTime;
                        waiting = false;
                        rotateDirection = Direction.RIGHT;
                    }
                }
            }
        }
        else
        {
            if (!waiting)
            {
                angle += Time.deltaTime * speed;
                rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
            }
            if (angle > rightMaxAngle)
            {
                waiting = true;
                if (waiting)
                {
                    waitingTime -= Time.deltaTime;
                    if (waitingTime <= 0)
                    {
                        waitingTime = _waitingTime;
                        waiting = false;
                        rotateDirection = Direction.LEFT;
                    }
                }
            }
        }
    }
    void Random2Rotate()
    {
        if (!waiting && activateTime >= 0)
        {
            activateTime -= Time.deltaTime;
        }
        else
        {
            waiting = true;
        }
        if (waiting)
        {
            waitingTime -= Time.deltaTime;
            if (waitingTime <= 0)
            {
                activateTime = Random.Range(_activateTime - 2, _activateTime);
                waitingTime = _waitingTime;
                waiting = false;
            }
        }
        if (!waiting)
        {
            if (rotateDirection == Direction.LEFT)
            {
                angle -= Time.deltaTime * speed;
                rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
                if (angle < leftMaxAngle)
                {
                    rotateDirection = Direction.RIGHT;
                }
            }
            else
            {
                angle += Time.deltaTime * speed;
                rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
                if (angle > rightMaxAngle)
                {
                    rotateDirection = Direction.LEFT;
                }
            }
        }
    }

    void RandomRotate()
    {
        if (!getRandom)
        {
            randomTimer = Random.Range(3f, 7f);
            getRandom = true;
        }
        if (randomTimer > 0)
        {
            randomTimer -= Time.deltaTime;
        }
        else
        {
            if (rotateDirection == Direction.LEFT)
            {
                rotateDirection = Direction.RIGHT;
            }
            else
            {
                rotateDirection = Direction.LEFT;
            }
            getRandom = false;
        }
        if (rotateDirection == Direction.LEFT)
        {
            angle -= Time.deltaTime * speed;
            rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
            if (angle < leftMaxAngle)
            {
                rotateDirection = Direction.RIGHT;
            }
        }
        else
        {
            angle += Time.deltaTime * speed;
            rotatePoint.localRotation = Quaternion.Euler(0, 0, angle);
            if (angle > rightMaxAngle)
            {
                rotateDirection = Direction.LEFT;
            }
        }

    }
    void ShootingLaser()
    {
        if (!lineRenderer.gameObject.activeInHierarchy)
        {
            lineRenderer.gameObject.SetActive(true);
        }
        Vector3 target = new Vector3();
        RaycastHit2D ray = Physics2D.Raycast(laserStart.position, -rotatePoint.up, disLaser, layerMask);
        if (ray)
        {
            target = ray.point;
            //Vector2 direction = (target - laserStart.position);
            if (ray.collider.CompareTag(Constants.TAG.PLAYER))
            {
                PlayerPos player = ray.collider.GetComponent<PlayerPos>();
                if (player != null)
                {
                    //player.DieInstant();
                    Debug.Log("kKOKOKO");
                }
                ray = Physics2D.Raycast(laserStart.position, -rotatePoint.up, disLaser, noPlayerMask);
                if (ray)
                {
                    target = ray.point;
                }
                else
                {
                    target = laserStart.position - rotatePoint.up * disLaser;
                }

            }
        }
        else
        {
            target = laserStart.position - rotatePoint.up * disLaser;
        }
        CreateEffect(target);
    }
    void CreateEffect(Vector2 target)
    {
        if (!hitEffectCreate.activeInHierarchy)
        {
            hitEffectCreate.SetActive(true);
        }
        hitEffectCreate.transform.position = target;
        lineRenderer.SetPositions(new Vector3[] { laserStart.position, target });
    }
}

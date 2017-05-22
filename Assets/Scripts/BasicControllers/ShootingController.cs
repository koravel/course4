using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public Vector3 point;
    private float lastFireTime;
    public float burstDelay;

    void Start()
    {
        lastFireTime = Time.time;
    }

    public void Fire(bool canFire)
    {
        if (canFire)
        {
            if (Time.time > (lastFireTime + burstDelay))
            {
                lastFireTime = Time.time;
                shot.gameObject.GetComponent<BulletController>().target = point;
                shot.gameObject.GetComponent<BulletController>().sourseTransform = shotSpawn;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
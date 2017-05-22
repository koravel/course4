using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject shot;
    public Vector3 point;
    private float lastFireTime;
    public float burstDelay;

    void Start()
    {
        lastFireTime = Time.time;
    }

    private void Fire(bool canFire,Vector3 direction)
    {
        if (canFire)
        {
            if (Time.time > (lastFireTime + burstDelay))
            {
                lastFireTime = Time.time;
                shot.gameObject.GetComponent<BulletController>().target = direction;
                shot.gameObject.GetComponent<BulletController>().sourseObject = gameObject;
                Instantiate(shot, gameObject.transform.position, gameObject.transform.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    public void FireOnClick(bool canFire)
    {
        Fire(canFire, point);
    }

    public void FireInDirection(bool canFire, Vector2 direction)
    {
        Fire(canFire, direction);
    }
}
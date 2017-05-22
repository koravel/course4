using System.Collections.Generic;
using UnityEngine;

public class RangeController : MeleeController
{
    public int gunCount;
    public float shootingDelay;
    public List<GameObject> guns;
    public string bulletPrefab;

    public override void FixedUpdate()
    {
        if(movementController.MoveToObject(distanceToMove, distanceOfVision))
        {
            movementController.MoveFromObject(distanceToMove);
        }
    }

    public override void Update() 
    {
        base.Update();
        foreach (var item in guns)
        {
            if (item != null)
            {
                GameObject searchingObj = GameObject.FindGameObjectWithTag("Player");
                if(searchingObj != null)
                {
                    item.GetComponent<ShootingController>().point = searchingObj.transform.position;
                    item.GetComponent<ShootingController>().FireOnClick(Vector2.Distance(transform.position, searchingObj.transform.position) <= guns[0].GetComponent<ShootingController>().shot.GetComponent<BulletController>().speed * guns[0].GetComponent<ShootingController>().shot.GetComponent<DestroyByTime>().lifeTime);
                }
            }
        }

    }
}

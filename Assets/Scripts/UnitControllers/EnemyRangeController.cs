using UnityEngine;

public class EnemyRangeController : EnemyController
{
    public int gunCount;
    public float shootingDelay;

    public override void FixedUpdate()
    {
        if(movementController.MoveToObject(distanceToMove))
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
                    item.GetComponent<ShootingController>().Fire(Vector2.Distance(transform.position, searchingObj.transform.position) <= guns[0].GetComponent<ShootingController>().shot.GetComponent<BulletController>().speed * guns[0].GetComponent<ShootingController>().shot.GetComponent<DestroyByTime>().lifeTime);
                }
            }
        }

    }
}

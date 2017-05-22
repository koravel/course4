using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{

    float time;
    public float lifeTime;

    void Start ()
    {
        time = Time.time;
    }
	
	void Update ()
    {
        if(time + lifeTime < Time.time)
        {
            Destroy(this.gameObject);
        }
    }
}

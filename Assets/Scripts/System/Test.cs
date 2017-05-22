using UnityEngine;
using System.Collections;
using Utilites.Level;
using System;

public class Test : MonoBehaviour
{
    ArcadeLevelBuilder builder = new ArcadeLevelBuilder();

	void Start ()
    {
        GameObject q = builder.MeleeBuild("Prefabs\\Units\\EnemyMelee", new Vector3(10, 10, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1000, 1500);
        GameObject qq = builder.RangeBuild("Prefabs\\Units\\EnemyRange", new Vector3(10, 10, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1500, 1500, new float[] { -0.3f, 0.3f }, 2, 0.5f);
            Instantiate(q, gameObject.transform);
            Instantiate(qq, gameObject.transform);
    }
}

using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

    public Transform target;
    private Vector3 lastPosition;

 void Update()
    {
        if (target != null)
        {
            lastPosition = new Vector3(target.position.x, target.position.y, -100);
        }
        GetComponent<Transform>().position = lastPosition;
    }
}
    
using UnityEngine;
using System.Collections;
using System;

public class MovementController : MonoBehaviour
{
    public float speed;
    public GameObject obj;
    public GameObject objMoveTo;
    private bool paused;

    public void Start()
    {
        paused = false;
    }

    public void Turn(Vector2 _turnPos)
    {
        if (obj != null && !paused)
        {
            Vector2 player_pos = Camera.main.WorldToScreenPoint(obj.transform.position);
            _turnPos.x = _turnPos.x - player_pos.x;
            _turnPos.y = _turnPos.y - player_pos.y;
            float angle = Mathf.Atan2(_turnPos.y, _turnPos.x) * Mathf.Rad2Deg - 90;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void Move(Vector2 _movement)
    {
        if(obj != null && !paused)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(_movement * speed / Time.deltaTime);
        }
    }

    public bool MoveToObject(float distance,float visionDistance)
    {
        if(objMoveTo != null && !paused)
        {
            var dir = objMoveTo.transform.position - obj.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if(Vector2.Distance(obj.transform.position,objMoveTo.transform.position) > distance && Vector2.Distance(obj.transform.position, objMoveTo.transform.position) < visionDistance)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(obj.transform.up * speed / Time.deltaTime);
                return false;
            }
            return true;
        }
        else
        {
            /////////////
            return false;
        }
    }

    public void MoveFromObject(float distance)
    {
        if(!paused)
        {
            var dir = objMoveTo.transform.position - obj.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if (Vector2.Distance(obj.transform.position, objMoveTo.transform.position) < distance)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(obj.transform.up * speed / Time.deltaTime * (-1));
            }
        }
    }

    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}

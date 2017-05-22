using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float damage;
    public Vector3 target;
    public GameObject sourseObject;
    public GameObject contactAnimation;

    void Start()
    {
        target.z = 0;
        target -= sourseObject.transform.parent.position;
        gameObject.GetComponent<Rigidbody2D>().
        GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(target) * speed;
        transform.rotation = sourseObject.transform.parent.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != null && sourseObject != null)
        {
            if (other.gameObject != sourseObject && other.gameObject != sourseObject.transform.parent.gameObject && other.gameObject.transform.parent != sourseObject.transform.parent)
            {
                Instantiate(contactAnimation, transform.position, transform.rotation);
                GameObject obj;
                if (other.gameObject.GetComponent<ShootingController>() != null)
                {
                    obj = other.gameObject.transform.parent.gameObject;
                }
                else
                {
                    obj = other.gameObject;
                }
                if (obj.GetComponent<PersonController>() != null)
                {
                    if (!obj.GetComponent<PersonController>().invulnerable)
                    {
                        obj.GetComponent<PersonController>().health -= damage;
                    }
                    obj.GetComponent<PersonController>().healthBar.transform.localScale = new Vector3(obj.GetComponent<PersonController>().health / obj.GetComponent<PersonController>().maxHealth, obj.GetComponent<PersonController>().healthBar.transform.localScale.y, 0);
                    obj.GetComponent<PersonController>().textHealthBar.GetComponent<Text>().text = obj.GetComponent<PersonController>().health.ToString();
                    Destroy(gameObject);
                    if (obj.GetComponent<PersonController>().health <= 0)
                    {
                        Instantiate(obj.GetComponent<PersonController>().explosionAnimation, obj.transform.position, obj.transform.rotation);
                        Destroy(obj);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}

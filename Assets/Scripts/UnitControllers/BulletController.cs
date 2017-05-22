using UnityEngine;
using Controllers;
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
        if(other.gameObject.GetComponent<ShootingController>() == null && other.gameObject != sourseObject && other.gameObject != sourseObject.transform.parent.gameObject && other.gameObject.transform != null && sourseObject.transform != null)
        {
            Instantiate(contactAnimation, transform.position, transform.rotation);
            if(other.gameObject.GetComponent<PersonController>() != null)
            {
                if (!other.gameObject.GetComponent<PersonController>().invulnerable)
                {
                    other.gameObject.GetComponent<PersonController>().health -= damage;
                }
                other.gameObject.GetComponent<PersonController>().healthBar.transform.localScale = new Vector3(other.gameObject.GetComponent<PersonController>().health / other.gameObject.GetComponent<PersonController>().maxHealth, other.gameObject.GetComponent<PersonController>().healthBar.transform.localScale.y, 0);
                other.gameObject.GetComponent<PersonController>().textHealthBar.GetComponent<Text>().text = other.gameObject.GetComponent<PersonController>().health.ToString();
                Destroy(gameObject);
                if (other.gameObject.GetComponent<PersonController>().health <= 0)
                {
                    Instantiate(other.gameObject.GetComponent<PersonController>().explosionAnimation, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    Destroy(other.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}

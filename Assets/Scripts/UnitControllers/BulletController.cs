using UnityEngine;
using Controllers;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float damage;
    public Vector3 target;
    public Transform sourseTransform;
    public GameObject contactAnimation;

    void Start()
    {
        target -= sourseTransform.parent.position;
        target.z = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(target) * speed;
        transform.rotation = sourseTransform.parent.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform != sourseTransform.parent.transform && other.gameObject.transform != sourseTransform && (other.gameObject.transform != null && sourseTransform.parent.transform != null))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 250f;
    public float smoothTime = 0.25f;
    public GameObject muzzlePrefab;
    public int damage = 10;
    public float forceImpact = 3f;


    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject muzzle = Instantiate(muzzlePrefab, transform.position, transform.rotation);
        Destroy(muzzle, .4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = transform.right * speed * Time.fixedDeltaTime;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Vector3 impactPoint = transform.position + transform.right * 0.2f;
            GameObject impact = Instantiate(muzzlePrefab, impactPoint, transform.rotation);
            Destroy(impact, .4f);

            Health enemy = collision.GetComponent<Health>();
            enemy.takeDamage(damage);

            if (!enemy.isDead())
            {
                Vector2 force = new Vector2(forceImpact, 2f);
                if(collision.transform.position.x < transform.position.x)
                {
                    force.x *= -1;
                }
                collision.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }
    }
}

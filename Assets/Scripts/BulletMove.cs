using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 250f;
    public float smoothTime = 0.25f;
    public int damage = 10;
    public float forceImpact = 3f;
    public string muzzleTag = "Muzzle";


    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = transform.right * speed * Time.fixedDeltaTime;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            enemy.takeDamage(damage);

            Vector2 force = new Vector2(forceImpact, 5f);
            if(collision.transform.position.x < transform.position.x)
            {
                force.x *= -1;
            }

            collision.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }

        if (collision.tag != "Player" && collision.tag != "RechargeStation")
        {
            GameObject impact = ObjectPooler.instance.instantiateFromPool(muzzleTag, transform.position, transform.rotation);
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
}

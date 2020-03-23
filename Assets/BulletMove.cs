using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 250f;
    public float smoothTime = 0.25f;
    public GameObject muzzlePrefab;

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
}

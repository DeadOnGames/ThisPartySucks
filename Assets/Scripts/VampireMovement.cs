using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireMovement : MonoBehaviour
{
    public float movementSpeed;
    public float speedX, speedY;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Collider"))
        //{
        //rb.AddForce(rb.velocity.normalized * -10);
        //}
    }
}

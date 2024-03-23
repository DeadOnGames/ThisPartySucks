using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform currentPoint;
    public float speed;

    public GameObject vampire;

    private bool hasLineOfSight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
        //animator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasLineOfSight)
        {
            float DirectionX = (Vector2.MoveTowards(transform.position, vampire.transform.position, speed * Time.deltaTime).x);
            transform.position = new Vector2(DirectionX, transform.position.y);
        }
        else
        {
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                Flip();
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                Flip();
                currentPoint = pointB.transform;
            }
        }
    }

    public void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, vampire.transform.position - transform.position);
        if(ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Vampire");
            if(hasLineOfSight)
            {
                Debug.DrawRay(transform.position, vampire.transform.position - transform.position, Color.red);
            } else
            {
                Debug.DrawRay(transform.position, vampire.transform.position - transform.position, Color.green);
            }
        }    
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

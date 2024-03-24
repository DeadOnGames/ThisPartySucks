using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isDead;
    private bool vampireSeen;
    private float timeVampireSeen;

    public AudioClip screamAudio;
    public AudioClip bloodAudio;
    public AudioSource audio;

    public Image uiEyeIndicator;
    private VampireController VampireController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;

        audio = GetComponent<AudioSource>();
        animator.SetBool("isScreaming", false);
        uiEyeIndicator.gameObject.SetActive(false);

        VampireController = vampire.GetComponent<VampireController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if(hasLineOfSight)
        {
            float DirectionX = (Vector2.MoveTowards(transform.position, vampire.transform.position, speed * Time.deltaTime).x);
            transform.position = new Vector2(DirectionX, transform.position.y);
            uiEyeIndicator.gameObject.SetActive(true);
            Scream();

            VampireController.hasBeenSeen = true;
        }
        else
        {
            //uiEyeIndicator.gameObject.SetActive(false);
            animator.SetBool("isScreaming", false);

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
        if (ray.collider != null)
        {
            bool previousLineOfSight = hasLineOfSight;
            hasLineOfSight = ray.collider.CompareTag("Vampire");

            // Reset the timer if the vampire just entered the line of sight
            if (hasLineOfSight && !previousLineOfSight)
            {
                StopCoroutine("CheckSustainedLineOfSight"); // Stop to ensure not multiple instances
                StartCoroutine("CheckSustainedLineOfSight");
            }
            else if (!hasLineOfSight)
            {
                StopCoroutine("CheckSustainedLineOfSight");
            }

            if (hasLineOfSight)
            {
                Debug.DrawRay(transform.position, vampire.transform.position - transform.position, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, vampire.transform.position - transform.position, Color.green);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected");
        if (collision.gameObject.tag == "Vampire")
        {
            OnBite();
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

    IEnumerator CheckSustainedLineOfSight()
    {
        yield return new WaitForSeconds(5f);
        if (hasLineOfSight)
        {
            // Trigger game over here
            GameOver();
        }
    }

    public void OnBite()
    {
        animator.SetTrigger("isBitten");
        isDead = true;
        audio.clip = bloodAudio;
        audio.Play();
    }

    public void Scream()
    {
        if (isDead) return;
        animator.SetBool("isScreaming", true);
        audio.clip = screamAudio;
        audio.Play();
    }

    public void GameOver()
    {
        Debug.Log("Game over");
    }
}

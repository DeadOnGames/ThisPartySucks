using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VampireController : MonoBehaviour
{
    // public BoxCollider2D collider;
    [SerializeField] private UnityEvent onBite;
    public bool hasBeenSeen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenSeen)
        {
            //Call coroutine
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.gameObject;
        NPCMovement NPCcontroller = collider.gameObject.GetComponent<NPCMovement>();

        if (collider.tag == "NPC" && !NPCcontroller.isDead)
        {
            Debug.Log("Bite");
            OnBite();
        }
    }

    public void OnBite()
    {
        onBite?.Invoke();
    }

    IEnumerator WaitForSeenSustained()
    {
        hasBeenSeen = false;
        yield return new WaitForSeconds(.1f);
        
    }
}

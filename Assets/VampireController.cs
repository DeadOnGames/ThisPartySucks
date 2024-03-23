using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireController : MonoBehaviour
{
   // public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Bite");
            //Bite();
        }
    }

    public void Bite()
    {
        //Increase blood bar
        //Bloode animation
        //NPC destroy
        
    }
}

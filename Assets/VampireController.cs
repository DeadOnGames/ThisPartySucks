using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VampireController : MonoBehaviour
{
    [SerializeField] private UnityEvent onBite;
    public bool hasBeenSeen;
    public AudioClip bumpAudio;
    public AudioSource audioSource;
    public Image uiEyeIndicator;

    public int seenCount;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bumpAudio;

        seenCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (seenCount > 0)
        {
            uiEyeIndicator.gameObject.SetActive(true);
            StartCoroutine(CheckSustainedLineOfSight());
        } else
        {
            uiEyeIndicator.gameObject.SetActive(false);
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
        else
        {
            audioSource.Play();
        }
    }

    public void OnBite()
    {
        onBite?.Invoke();
    }

    IEnumerator CheckSustainedLineOfSight()
    {
        yield return new WaitForSeconds(5f);
        if (seenCount > 0)
        {
            GameManager.Instance.gameLost = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onLose;

    [SerializeField] private GameObject HealthBar;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = HealthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check the health bar to determine win state
        if (slider.value == slider.maxValue)
        {
            OnWin();
        }
    }

    public void OnWin()
    {
        onWin?.Invoke();
    }

    public void OnLose()
    {
        onLose?.Invoke();
    }
}

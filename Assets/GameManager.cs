using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onLose;

    [SerializeField] private GameObject HealthBar;
    [SerializeField] private Slider slider;

    public bool gameLost;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        if (gameLost)
        {
            OnLose();
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

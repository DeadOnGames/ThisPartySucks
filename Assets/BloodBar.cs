using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    private Slider slider;
    public GameObject UIWinScreen;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        UIWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value == slider.maxValue)
        {
            UIWinScreen.SetActive(true);
        }
    }

    public void AddBlood()
    {
        slider.value += 5;
    }
}

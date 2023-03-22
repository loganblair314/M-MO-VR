using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI[] Timers;
    [SerializeField] Slider slider;

    private double size = 5.0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSlider(){
        size = (double)slider.value;
        foreach (var timer in Timers)
        {
            timer.fontSize = (float)size;
        }
    }
}

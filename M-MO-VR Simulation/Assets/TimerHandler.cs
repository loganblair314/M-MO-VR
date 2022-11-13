using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    private float startTime;
    // [SerializeField]
    private string textTime;
    private float guiTime;
    private int minutes;
    private int seconds;
    private int fraction;
    public TextMeshProUGUI textField;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        guiTime = Time.time - startTime;
        minutes = (int)guiTime / 60;
        seconds = (int)guiTime % 60;
        fraction = (int)(guiTime * 100) % 100;
        textTime = string.Format("{0:00}:{1:00}", minutes, seconds, fraction);

        textField.text = "Time Spent - " + textTime;
    }
}

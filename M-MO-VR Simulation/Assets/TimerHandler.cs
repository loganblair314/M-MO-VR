using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    public string objectTag = "Player";
    private bool timer1;
    private bool timer2;
    private float minutes;
    private float seconds;
    private float startTime;
    private float currentTime;
    public TextMeshProUGUI textField1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            startTime = Time.time;
            timer1 = true;
            timer2 = false;
            Debug.Log("Successful Collision with Rings. Timer starting.");
        }
    }

    void Start()
    {
        timer1 = false;
        timer2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer1 == true)
        {
            currentTime = Time.time - startTime;
            minutes = (int)(currentTime / 60f);
            seconds = (int)(currentTime % 60f);
            Debug.Log("Level 1 Time - " + minutes + ":" + seconds);
        }
    }
}

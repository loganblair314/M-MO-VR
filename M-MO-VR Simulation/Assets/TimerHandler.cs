using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    private bool timer1On, timer2On, timer3On;
    private float timer1, timer2, timer3, timer4, timer5, timer6;
    private float minutes;
    private float seconds;
    private bool resetTime;
    public TextMeshProUGUI textField1;

    void Start()
    {
        //timer1On = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((TeleportManager.index == 0))
        {
            timer1 += Time.deltaTime;
            minutes = (int)(timer1 / 60f);
            seconds = (int)(timer1 % 60f);
            Debug.Log("Level 1 Time - " + minutes + ":" + seconds);
        }
        if ((TeleportManager.index == 1))
        {
            timer2 += Time.deltaTime;
            minutes = (int)(timer2 / 60f);
            seconds = (int)(timer2 % 60f);
            Debug.Log("Level 2 Time - " + minutes + ":" + seconds);
        }
        if ((TeleportManager.index == 2))
        {
            timer3 += Time.deltaTime;
            minutes = (int)(timer3 / 60f);
            seconds = (int)(timer3 % 60f);
            Debug.Log("Level 3 Time - " + minutes + ":" + seconds);
        }
        if ((TeleportManager.index == 3))
        {
            timer4 += Time.deltaTime;
            minutes = (int)(timer4 / 60f);
            seconds = (int)(timer4 % 60f);
            Debug.Log("Level 3 Time - " + minutes + ":" + seconds);
        }
        if ((TeleportManager.index == 4))
        {
            timer5 += Time.deltaTime;
            minutes = (int)(timer5 / 60f);
            seconds = (int)(timer5 % 60f);
            Debug.Log("Level 3 Time - " + minutes + ":" + seconds);
        }
        if ((TeleportManager.index == 5))
        {
            timer6 += Time.deltaTime;
            minutes = (int)(timer6 / 60f);
            seconds = (int)(timer6 % 60f);
            Debug.Log("Level 3 Time - " + minutes + ":" + seconds);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSim : MonoBehaviour
{
    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSim : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.Quit();
        }
    }
}

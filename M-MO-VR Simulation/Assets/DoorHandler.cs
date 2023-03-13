using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandler : MonoBehaviour
{
    public GameObject door4, handle41, handle42, door5, handle51, handle52;
    public int InterLayer, NonLayer;

    // Start is called before the first frame update
    void Start()
    {
        door4.GetComponent<BoxCollider>().enabled = true;
        door5.GetComponent<BoxCollider>().enabled = true;
        InterLayer = LayerMask.NameToLayer("Interactable");
        NonLayer = LayerMask.NameToLayer("Ignore Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disableDoorColliders()
    {
        if (TeleportManager.index == 3)
        {
            Debug.Log("Door in Level 4 grabbed.");
            handle41.layer = NonLayer;
            handle42.layer = NonLayer;
            door4.GetComponent<BoxCollider>().enabled = false;
        }
        else if (TeleportManager.index == 4)
        {
            Debug.Log("Door in Level 5 grabbed.");
            handle51.layer = NonLayer;
            handle52.layer = NonLayer;
            door5.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void enableDoorColliders()
    {
        if (TeleportManager.index == 3)
        {
            Debug.Log("Door in Level 4 let go.");
            handle41.layer = InterLayer;
            handle42.layer = InterLayer;
            door4.GetComponent<BoxCollider>().enabled = true;
        }
        else if (TeleportManager.index == 4)
        {
            Debug.Log("Door in Level 5 let go.");
            handle51.layer = InterLayer;
            handle52.layer = InterLayer;
            door5.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

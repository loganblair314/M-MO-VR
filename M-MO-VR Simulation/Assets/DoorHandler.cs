using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] XRController controller;
    private InputDevice targetDevice1, targetDevice2;
    public GameObject door4, handle41, handle42, door5, handle51, handle52;
    public int InterLayer, NonLayer;
    private float timer;
    private bool doorGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        doorGrabbed = false;
        door4.GetComponent<BoxCollider>().enabled = true;
        door5.GetComponent<BoxCollider>().enabled = true;
        InterLayer = LayerMask.NameToLayer("Interactable");
        NonLayer = LayerMask.NameToLayer("Ignore Player");
    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devicesRight = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devicesRight);

        if (devicesRight.Count > 0)
        {
            targetDevice1 = devicesRight[0];
        }

        List<InputDevice> devicesLeft = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devicesLeft);


        if (devicesLeft.Count > 0)
        {
            targetDevice2 = devicesLeft[0];
        }

        if (doorGrabbed)
        {
            timer += Time.deltaTime;
            if (timer > 0.5) // 0.1)
            {
                Debug.Log("Door being grabbed.");
                targetDevice1.SendHapticImpulse(0, 1.0f, 0.1f);
                targetDevice2.SendHapticImpulse(0, 1.0f, 0.1f);
                timer = 0;
            }
        }
    }

    public void disableDoorColliders()
    {
        doorGrabbed = true;
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
        doorGrabbed = false;
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

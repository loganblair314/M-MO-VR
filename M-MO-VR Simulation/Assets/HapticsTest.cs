using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class HapticsTest : MonoBehaviour
{
    [SerializeField] XRController controller;
    private int count;
    // public TextMeshProUGUI textField;
    private Animator animatorController;
    private InputDevice targetDevice;
    public Transform raycastOrigin;
    //public GameObject Floor;

    /*
    private void Start()
    {
        
    }
    */

    private void Update()
    {
        // Quest 2 takes a few seconds to show up in Unity, so have to intialize all of this in Update.
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        /*
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        */

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        /*
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            count++;
        }
        */

        /*
        HapticCapabilities capabilities;
        if (targetDevice.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                targetDevice.SendHapticImpulse(0, 0.5f, 1.0f);
            }
        }
        */

        // If we find a device we are looking for and if the trigger is pulled.
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            RaycastHit hit;
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

            // If a Raycast of length 2 detectes a hit.
            if (Physics.Raycast(ray, out hit, 2))
            {
                var hitPoint = hit.point;
                hitPoint.y = 0;

                var playerPosition = raycastOrigin.position;
                playerPosition.y = 0;

                float distance = Vector3.Distance(hitPoint, playerPosition);
                // Increment.
                count++;

                // Intervals below:
                // 0 < x <= 0.5
                // 0.5 < x <= 1
                // 1 < x <= 1.5
                // 1.5 < x <= 2

                // Now, as long as the floor is not being pointed at, Haptics function.
                // The closer the object, the greater the vibration's strength.
                if (hit.transform.name != "Floor" || hit.transform.name != "LeftHand Controller")
                {
                    if (distance > 0 && distance <= 0.5)
                    {
                        Debug.Log("Item is between 0 and 0.5 units away.");
                        targetDevice.SendHapticImpulse(0, 1.0f, 1.0f);

                    }
                    else if (distance > 0.5 && distance <= 1.0)
                    {
                        Debug.Log("Item is between 0.5 and 1 units away.");
                        targetDevice.SendHapticImpulse(0, 0.75f, 1.0f);

                    }
                    else if (distance > 1.0 && distance <= 1.5)
                    {
                        Debug.Log("Item is between 1 and 1.5 units away.");
                        targetDevice.SendHapticImpulse(0, 0.5f, 1.0f);

                    }
                    else if (distance > 1.5 && distance < 2.0)
                    {
                        Debug.Log("Item is between 1.5 and 2.0 units away.");
                        targetDevice.SendHapticImpulse(0, 0.25f, 1.0f);
                    }
                }
            }
        }
        //textField.text = "Trigger Pulled - " + count;
    }
}
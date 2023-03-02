using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class RayCone : MonoBehaviour
{
    public int theAngle;
    public int segments;
    public Transform cam;
    [SerializeField] XRController controller;
    private UnityEngine.XR.InputDevice targetDevice;
    AudioSource AudSrc;
    public AudioClip note;

    // Start is called before the first frame update
    void Start()
    {
        AudSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
 
        // Quest 2 takes a few seconds to show up in Unity, so have to intialize all of this in Update.
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        // If we find a device we are looking for and if the trigger is pulled.
        if ((targetDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) || Input.GetKey("space"))
        {
            RaycastSweep();
        }
    }

    void RaycastSweep()
    {
        int ignoreLayer = 1 << 2;
        ignoreLayer = ~ignoreLayer;
        Vector3 startPos = cam.position; // start position
        Vector3 targetPos = Vector3.zero; // variable for calculated end position

        int startAngle = -theAngle / 2; // half the angle to the Left of the forward
        int finishAngle = theAngle / 2; // half the angle to the Right of the forward

        int inc = theAngle / segments; // the gap between each ray (increment)

        float ClDis = 999; // the closest distance between the object and a player

        for (int i = startAngle; i <= finishAngle; i += inc) // Angle from forward
        {
            // step through and find each target point
            for (int j = startAngle; j <= finishAngle; j += inc) // Angle from forward
            {
                // step through and find each target point
                for (int k = startAngle; k <= finishAngle; k += inc) // Angle from forward
                {
                    targetPos = new Vector3(i, j, k) + (cam.forward * 90);
                    Vector3 newTarg = transform.TransformDirection(targetPos);
                    Ray ray = new Ray(startPos, targetPos);

                    // If a Raycast of length 15 units detectes a hit.
                    if (Physics.Raycast(startPos, targetPos, out RaycastHit hit, 15, ignoreLayer))
                    {
                        var hitPoint = hit.point;
                        hitPoint.y = 0;

                        var playerPosition = startPos;
                        playerPosition.y = 0;

                        // Distance between player and object
                        float distance = Vector3.Distance(hitPoint, playerPosition);

                        // Update the closest distance
                        // As long as it is not detecting the floor or cei or the player, and the distance is shorter
                        if (distance <= ClDis && hit.transform.tag != "Floor" && hit.transform.tag != "Ceiling")
                        {
                            Debug.Log(distance);
                            Debug.Log(hit.transform.name);
                            ClDis = distance;
                        }
                    }
                    // to show ray just for testing
                    Debug.DrawRay(startPos, targetPos, Color.red);
                }
            }
        }

        // PUTTING NOTE AS THE CLIP TEMPORARILY UNTIL AUDITOY TOOL IMPLEMENTED
        AudSrc.clip = note;

        // Intervals (Delay):
        // 0 < x <= 3 (none)
        // 3 < x <= 6 (0.4 second)
        // 6 < x <= 9 (0.75 second)
        // 9 < x <= 12 (1.125 second)
        // 12 < x <= 15 (1.5 second)
        // 15 < x (NO SOUND)
        // The closer the object, the more frequent the beeps.

        if (AudSrc.isPlaying == false)
        {
            if (ClDis > 0 && ClDis <= 3)
            {
                Debug.Log("Object is between 0 and 3 units away.");
                AudSrc.PlayOneShot(note);
            }
            else if (ClDis > 3 && ClDis <= 6)
            {
                Debug.Log("Object is between 3 and 6 units away.");
                AudSrc.PlayDelayed(0.4F);
            }
            else if (ClDis > 6 && ClDis <= 9)
            {
                Debug.Log("Object is between 6 and 9 units away.");
                AudSrc.PlayDelayed(0.75F);
            }
            else if (ClDis > 9 && ClDis <= 12)
            {
                Debug.Log("Object is between 9.6 and 12.8 units away.");
                AudSrc.PlayDelayed(1.125F);
            }
            else if (ClDis > 12 && ClDis <= 15)
            {
                Debug.Log("Object is between 12 and 15 units away.");
                AudSrc.PlayDelayed(1.5F);
            }/*
            else if (ClDis > 16 && ClDis <= 20)
            {
                Debug.Log("Object is between 16 and 20 units away.");
                AudSrc.PlayDelayed(1F);
            }*/
            else
            {
                Debug.Log("No object detected");
            }
        }
    }
}

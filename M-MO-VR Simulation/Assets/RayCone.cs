using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.VisualScripting;

public class RayCone : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public int theAngle;
    public int segments;
    public Transform cam;
    [SerializeField] XRController controller;
    private UnityEngine.XR.InputDevice targetDevice;
    public AudioClip[] audioClips;
    AudioSource AudSrc;
    AudioClip note;
    public AudioClip a4;
    public AudioClip a5;
    public AudioClip b4;
    public AudioClip b5;
    public AudioClip c4;
    public AudioClip c5;
    public AudioClip d4;
    public AudioClip d5;
    public AudioClip e4;
    public AudioClip e5;


    // Start is called before the first frame update
    void Start()
    {
        ignoreLayer = ~ignoreLayer;
        AudSrc = gameObject.GetComponent<AudioSource>();
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
            if (((TeleportManager.index == 0) || (TeleportManager.index == 1) || (TeleportManager.index == 4) || (TeleportManager.index == 5)) && (!MenuManager.MenuOpen))
            {
                RaycastSweep();
            }  
        }
    }

    void RaycastSweep()
    {
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
                    if (Physics.Raycast(ray, out RaycastHit hit, 10, ignoreLayer))
                    {
                        var hitPoint = hit.point;
                        float ObjY = hit.transform.position.y;
                        hitPoint.y = 0;

                        var playerPosition = startPos;
                        playerPosition.y = 0;

                        // Distance between player and object
                        float distance = Vector3.Distance(hitPoint, playerPosition);

                        // Update the closest distance
                        // As long as it is not detecting the floor or cei or the player, and the distance is shorter
                        if (distance <= ClDis && hit.transform.tag != "Floor" && hit.transform.tag != "Ceiling" && hit.transform.name != "XR Origin V2")
                        {
                            Debug.Log(distance);
                            Debug.Log(hit.transform.name);
                            Debug.Log(hit.transform.gameObject.layer);
                            ClDis = distance;

                            //Selects audio Clip by Height
                            if (ObjY > 0 && ObjY < 0.3)
                            {
                                note = a4;
                            }
                            else if (ObjY >= 0.3 && ObjY < 0.6)
                            {
                                note = b4;
                            }
                            else if (ObjY >= 0.6 && ObjY < 0.9)
                            {
                                note = c4;
                            }
                            else if (ObjY >= 0.9 && ObjY < 1.2)
                            {
                                note = d4;
                            }
                            else if (ObjY >= 1.2 && ObjY < 1.5)
                            {
                                note = e4;
                            }
                            else if (ObjY >= 1.5 && ObjY < 1.8)
                            {
                                note = a5;
                            }
                            else if (ObjY >= 1.8 && ObjY < 2.1)
                            {
                                note = b5;
                            }
                            else if (ObjY >= 2.1 && ObjY < 2.4)
                            {
                                note = c5;
                            }
                            else if (ObjY >= 2.4 && ObjY < 2.7)
                            {
                                note = d5;
                            }
                            else
                            {
                                note = e5;
                            }
                        }
                    }
                    // to show ray just for testing
                    Debug.DrawRay(startPos, targetPos, Color.red);
                }
            }
        }

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
            if (ClDis > 0 && ClDis <= 2)
            {
                Debug.Log("Object is between 0 and 3 units away.");
                AudSrc.PlayOneShot(note);
            }
            else if (ClDis > 2 && ClDis <= 4)
            {
                Debug.Log("Object is between 3 and 6 units away.");
                AudSrc.PlayDelayed(0.25F);
            }
            else if (ClDis > 4 && ClDis <= 6)
            {
                Debug.Log("Object is between 6 and 9 units away.");
                AudSrc.PlayDelayed(0.5F);
            }
            else if (ClDis > 6 && ClDis <= 8)
            {
                Debug.Log("Object is between 9.6 and 12.8 units away.");
                AudSrc.PlayDelayed(0.75F);
            }
            else if (ClDis > 8 && ClDis <= 10)
            {
                Debug.Log("Object is between 12 and 15 units away.");
                AudSrc.PlayDelayed(1F);
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

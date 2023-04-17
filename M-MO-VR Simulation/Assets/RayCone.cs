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
            if (!MenuManager.MenuOpen)
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

        for (int x = startAngle; x <= finishAngle; x += inc) // Angle from forward, X position
        {
            // step through and find each target point
            for (int y = startAngle; y <= finishAngle; y += inc) // Angle from forward, Y position
            {
                // step through and find each target point
                for (int z = startAngle; z <= finishAngle; z += inc) // Angle from forward, Z position
                {
                    targetPos = new Vector3(x, y, z) + (cam.forward * 90);
                    Vector3 newTarg = transform.TransformDirection(targetPos);
                    Ray ray = new Ray(startPos, targetPos);

                    // If a Raycast of length 16 units detectes a hit.
                    if (Physics.Raycast(ray, out RaycastHit hit, 16, ignoreLayer))
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
        // 0 < x <= 2 (none)
        // 2 < x <= 4 (0.25 second)
        // 4 < x <= 7 (0.5 second)
        // 7 < x <= 11 (0.75 second)
        // 11 < x <= 16 (1 second)
        // 16 < x (NO SOUND)
        // The closer the object, the more frequent the beeps.

        if (AudSrc.isPlaying == false)
        {
            if (ClDis > 0 && ClDis <= 2)
            {
                Debug.Log("Object is between 0 and 2 units away.");
                AudSrc.PlayOneShot(note);
            }
            else if (ClDis > 2 && ClDis <= 4)
            {
                Debug.Log("Object is between 2 and 4 units away.");
                AudSrc.PlayDelayed(0.25F);
            }
            else if (ClDis > 4 && ClDis <= 7)
            {
                Debug.Log("Object is between 4 and 7 units away.");
                AudSrc.PlayDelayed(0.5F);
            }
            else if (ClDis > 7 && ClDis <= 11)
            {
                Debug.Log("Object is between 7 and 11 units away.");
                AudSrc.PlayDelayed(0.75F);
            }
            else if (ClDis > 11 && ClDis <= 16)
            {
                Debug.Log("Object is between 11 and 16 units away.");
                AudSrc.PlayDelayed(1F);
            }
            else
            {
                Debug.Log("No object detected");
            }
        }
    }
}

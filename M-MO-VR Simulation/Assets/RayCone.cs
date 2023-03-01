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
        Vector3 startPos = cam.position; // start position
        Vector3 targetPos = Vector3.zero; // variable for calculated end position

        int startAngle = -theAngle / 2; // half the angle to the Left of the forward
        int finishAngle = theAngle / 2; // half the angle to the Right of the forward

        int inc = theAngle / segments; // the gap between each ray (increment)

        float ClDis = 999; // the closest distance between the object and a player

        for (int j = startAngle; j <= finishAngle; j += inc) // Angle from forward
        {
            // step through and find each target point
            for (int i = startAngle; i <= finishAngle; i += inc) // Angle from forward
            {
                targetPos = new Vector3(j, i, j) + (cam.forward * 90);
                Vector3 newTarg = transform.TransformDirection(targetPos);
                RaycastHit hit;
                Ray ray = new Ray(startPos, targetPos);

                // If a Raycast of length 1 detectes a hit.
                if (Physics.Raycast(startPos, targetPos, out hit, 1))
                {
                    var hitPoint = hit.point;
                    hitPoint.y = 0;

                    var playerPosition = startPos;
                    playerPosition.y = 0;

                    // Distance between player and object
                    float distance = Vector3.Distance(hitPoint, playerPosition);

                    // Update the closest distance
                    // As long as it is not detecting the floor or the player, and the distance is shorter
                    if (distance < ClDis && (hit.transform.tag != "Floor" && hit.transform.name != "LeftHand Controller" && hit.transform.name != "RightHand Controller" && hit.transform.name != "Main Camera" && hit.transform.name != "Feet (For Collision)" && hit.transform.name != "Capsule" && hit.transform.name != "XR Origin V2"))
                    {
                        Debug.Log(distance);
                        Debug.Log(hit.transform.name);
                        ClDis = distance;
                    }
                }

                Debug.Log(targetPos);
                // to show ray just for testing
                Debug.DrawRay(startPos, targetPos, Color.red);
            }
        }

        // Intervals below:
        // 0 < x <= 0.25 (Consistent)
        // 0.25 < x <= 0.5 (0.33 second)
        // 0.5 < x <= 0.75 (0.66 second)
        // 0.75 < x <= 1 (1 second)
        // 1 < x (no sound)
        // The closer the object, the more frequent the beeps.

        if (AudSrc.isPlaying == false)
        {
            if (ClDis > 0 && ClDis <= 0.25)
            {
                Debug.Log("Object is between 0 and 0.25 units away.");
                AudSrc.PlayOneShot(note);
            }
            else if (ClDis > 0.25 && ClDis <= 0.5)
            {
                Debug.Log("Object is between 0.25 and 0.5 units away.");
                AudSrc.PlayDelayed(0.33F);
            }
            else if (ClDis > 0.5 && ClDis <= 0.75)
            {
                Debug.Log("Object is between 0.5 and 0.75 units away.");
                AudSrc.PlayDelayed(0.66F);
            }
            else if (ClDis > 0.75 && ClDis <= 1)
            {
                Debug.Log("Object is between 0.75 and 1 units away.");
                AudSrc.PlayDelayed(1F);
            }
            else
            {
                Debug.Log("No object detected");
            }
        }
    }
}

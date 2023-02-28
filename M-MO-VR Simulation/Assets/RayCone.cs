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
    public InputActionProperty coneBut;
    private UnityEngine.XR.InputDevice targetDevice;
    AudioSource note;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
<<<<<<< Updated upstream
        if (coneBut.action.WasPressedThisFrame())
        {
            RaycastSweep();
        }
        else if (Input.GetMouseButtonDown(0))
=======
        note = GetComponent<AudioSource>();
        if (Input.GetKey("space"))
        {
            RaycastSweep();
        }

        // Quest 2 takes a few seconds to show up in Unity, so have to intialize all of this in Update.
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        // If we find a device we are looking for and if the trigger is pulled.
        if (targetDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
>>>>>>> Stashed changes
        {
            RaycastSweep();
        }
    }

    void RaycastSweep()
    {
        Vector3 startPos = cam.position; // umm, start position !
        Vector3 targetPos = Vector3.zero; // variable for calculated end position

        int startAngle = -theAngle / 2; // half the angle to the Left of the forward
        int finishAngle = theAngle / 2; // half the angle to the Right of the forward

        // the gap between each ray (increment)
        int inc = theAngle / segments;

        for (int j = startAngle; j <= finishAngle; j += inc) // Angle from forward
        {
            // step through and find each target point
            for (int i = startAngle; i <= finishAngle; i += inc) // Angle from forward
            {
                targetPos = new Vector3(j, i, 0) + (cam.forward * 90);
                Vector3 newTarg = transform.TransformDirection(targetPos) / 10;

<<<<<<< Updated upstream
                Debug.Log(cam.forward);
                Debug.Log(targetPos);
                Debug.Log(newTarg);

                // linecast between points
                if (Physics.Raycast(startPos, newTarg))
=======
                // If a Raycast of length 1 detectes a hit.
                if (Physics.Raycast(startPos, newTarg, out hit, 1))
>>>>>>> Stashed changes
                {
                    Debug.Log("Hit");
                }

                //USE PERIMETER EQUATION TO SOLVE THIS

                // to show ray just for testing
                Debug.DrawRay(startPos, newTarg, Color.red);
            }
        }
<<<<<<< Updated upstream
=======

        // Intervals below:
        // 0 < x <= 0.25 (Consistent)
        // 0.25 < x <= 0.5 (0.33 second)
        // 0.5 < x <= 0.75 (0.66 second)
        // 0.75 < x <= 1 (1 second)
        // 1 < x (no sound)
        // The closer the object, the more frequent the beeps.
        if (ClDis > 0 && ClDis <= 0.25)
        {
            Debug.Log("Object is between 0 and 0.5 units away.");
            note.Play();
        }
        else if (ClDis > 0.25 && ClDis <= 0.5)
        {
            Debug.Log("Object is between 0.5 and 1 units away.");
            note.PlayDelayed((float)0.33);
        }
        else if (ClDis > 0.5 && ClDis <= 0.75)
        {
            Debug.Log("Object is between 1 and 1.5 units away.");
            note.PlayDelayed((float)0.66);
        }
        else if (ClDis > 0.75 && ClDis <= 1)
        {
            Debug.Log("Object is between 1.5 and 2.0 units away.");
            note.PlayDelayed((float)1);
        }
        else
        {
            Debug.Log("No object detected");
        }
>>>>>>> Stashed changes
    }
}

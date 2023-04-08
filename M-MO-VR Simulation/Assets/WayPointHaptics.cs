using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Linq;

public class WayPointHaptics : MonoBehaviour
{
    [SerializeField] XRController controller;
    //private int count;
    private Animator animatorController;
    private InputDevice targetDevice;
    public Transform raycastOrigin;
    public string objectTag = "Player";
    public GameObject waypoint;
    private bool activeWaypoint;
    private float timer;
    GameObject[] waypoints;

    private void Start()
    {
        // Store all waypoints in array and set all to active.
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].SetActive(true);
        }
        //waypoint.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            // Deactivate the specific waypoint the user walks into.
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoint.name == waypoints[i].name)
                {
                    waypoints[i].SetActive(false);
                    if (waypoint.name == "Waypoint 2.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 2.2")
                            {
                                Debug.Log("Disabled Waypoint 2.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 2.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 2.1")
                            {
                                Debug.Log("Disabled Waypoint 2.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.2")
                            {
                                Debug.Log("Disabled Waypoint 4.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.1")
                            {
                                Debug.Log("Disabled Waypoint 4.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 5.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 5.2")
                            {
                                Debug.Log("Disabled Waypoint 5.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 5.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 5.1")
                            {
                                Debug.Log("Disabled Waypoint 5.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 4.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 4.1")
                            {
                                Debug.Log("Disabled Waypoint 4.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 6.1")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 6.2")
                            {
                                Debug.Log("Disabled Waypoint 6.2");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                    else if (waypoint.name == "Waypoint 6.2")
                    {
                        for (int j = 0; j < waypoints.Length; j++)
                        {
                            if (waypoints[j].name == "Waypoint 6.1")
                            {
                                Debug.Log("Disabled Waypoint 6.1");
                                waypoints[j].SetActive(false);
                            }
                        }
                    }
                }
            }
           //waypoint.SetActive(false);
           Debug.Log("Disabling waypoint.");
        }
    }

    private void Update()
    {
        // Quest 2 takes a few seconds to show up in Unity, so have to intialize all of this in Update.
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            RaycastHit hit;
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

            // If a Raycast of length 2 detectes a hit.
            if (Physics.Raycast(ray, out hit, 10))
            {
                var hitPoint = hit.point;
                hitPoint.y = 0;

                var playerPosition = raycastOrigin.position;
                playerPosition.y = 0;

                float distance = Vector3.Distance(hitPoint, playerPosition);

                if ((hit.transform.tag == "Waypoint") && (waypoint.activeSelf) && ((hit.transform.name != "Handle (In)") || (hit.transform.name != "Handle (Out)")))
                {
                    timer += Time.deltaTime;
                    if (timer > 0.6) // 0.1)
                    {
                        Debug.Log("Detected waypoint.");
                        targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        timer = 0;
                    }
                }
                
                else if ((distance <= 2) && ((hit.transform.name == "Door") || (hit.transform.name == "Handle (In)") || (hit.transform.name == "Handle (Out)")))
                {
                    timer += Time.deltaTime;
                    if (timer > 0.3)
                    {
                        Debug.Log("Door detected.");
                        targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        timer = 0;
                    }
                }
            
                else
                {
                    if (((TeleportManager.index >= 2) && (TeleportManager.index < 6)) && (!MenuManager.MenuOpen))
                    {
                        if (distance > 0 && distance <= 0.67)
                        {
                            Debug.Log("Item is between 0 and 0.67 units away.");
                            targetDevice.SendHapticImpulse(0, 1.0f, 0.1f);
                        }
                        else if (distance > 0.67 && distance <= 1.33)
                        {
                            Debug.Log("Item is between 0.67 and 1.33 units away.");
                            targetDevice.SendHapticImpulse(0, 0.67f, 0.1f);
                        }
                        else if (distance > 1.33 && distance < 2.0)
                        {
                            Debug.Log("Item is between 1.33 and 2.0 units away.");
                            targetDevice.SendHapticImpulse(0, 0.33f, 0.1f);
                        }
                    }
                }
            }
        }
    }

    public void ResetWaypoints()
    {
        // Reset all waypoints' active status on level reset / second tries.
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (!waypoints[i].activeSelf)
            {
                waypoints[i].SetActive(true);
            }
            //waypoints[i].SetActive(true);
        }
    }
}

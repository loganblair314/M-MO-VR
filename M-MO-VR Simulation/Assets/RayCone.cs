using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCone : MonoBehaviour
{
    public float distance;
    public int theAngle;
    public int segments;
    public Transform cam;
    public InputActionProperty coneBut;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Have the cone detect objects aslong as the buttons are pressed
        while (coneBut.action.WasPressedThisFrame())
        {
            RaycastSweep();
        }
        while (Input.GetMouseButtonDown(0))
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

        int inc = theAngle / segments; // the gap between each ray (increment)

        float ClDis = 999; // the closest distance between the object and a player

        for (int j = startAngle; j <= finishAngle; j += inc) // Angle from forward
        {
            // step through and find each target point
            for (int i = startAngle; i <= finishAngle; i += inc) // Angle from forward
            {
                targetPos = new Vector3(j, i, 0) + (cam.forward * 90);
                Vector3 newTarg = transform.TransformDirection(targetPos);
                RaycastHit hit;
                Ray ray = new Ray(startPos, targetPos);

                // If a Raycast of length 2 detectes a hit.
                if (Physics.Raycast(startPos, newTarg, out hit, 2))
                {
                    var hitPoint = hit.point;
                    hitPoint.y = 0;

                    var playerPosition = startPos;
                    playerPosition.y = 0;

                    // Distance between player and object
                    float distance = Vector3.Distance(hitPoint, playerPosition);

                    // Update the closest distance
                    // As long as it is not detecting the floor or the controllers, and the distance is shorter
                    if (distance < ClDis && (hit.transform.name != "Floor" && hit.transform.name != "LeftHand Controller" && hit.transform.name != "RightHand Controller"))
                    {
                        ClDis = distance;
                    }
                }
                // to show ray just for testing
                Debug.DrawRay(startPos, newTarg, Color.red, 8);
            }
        }

        // Intervals below:
        // 0 < x <= 0.5 (Consistent)
        // 0.5 < x <= 1 (_ BPM)
        // 1 < x <= 1.5 (_ BPM)
        // 1.5 < x <= 2 (_ BPM)
        // 2 < x (no sound)
        // The closer the object, the more frequent the beeps.
        if (ClDis > 0 && ClDis <= 0.5)
        {
            Debug.Log("Object is between 0 and 0.5 units away.");
        }
        else if (ClDis > 0.5 && ClDis <= 1)
        {
            Debug.Log("Object is between 0.5 and 1 units away.");
        }
        else if (ClDis > 1 && ClDis <= 1.5)
        {
            Debug.Log("Object is between 1 and 1.5 units away.");
        }
        else if (ClDis > 1.5 && ClDis <= 2)
        {
            Debug.Log("Object is between 1.5 and 2.0 units away.");
        }
        else
        {
            Debug.Log("No object detected");
        }
    }
}

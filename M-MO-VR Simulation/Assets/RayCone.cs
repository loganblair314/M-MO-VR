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
        if (coneBut.action.WasPressedThisFrame())
        {
            RaycastSweep();
        }
        else if (Input.GetMouseButtonDown(0))
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

                Debug.Log(cam.forward);
                Debug.Log(targetPos);
                Debug.Log(newTarg);

                // linecast between points
                if (Physics.Raycast(startPos, newTarg))
                {
                    Debug.Log("Hit");
                }

                //USE PERIMETER EQUATION TO SOLVE THIS

                // to show ray just for testing
                Debug.DrawRay(startPos, newTarg, Color.red, 8);
            }
        }
    }
}

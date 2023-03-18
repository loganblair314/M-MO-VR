using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickNav : MonoBehaviour
{
    private bool active;
    public InputActionProperty Joystick;
    private Vector2 input;
    private bool reset;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {   
        input = ReadInput();
        if(active){
            if(input.y > 0.25){
                Debug.Log("UP");
            }
            else if(input.y < -0.25){
                Debug.Log("DOWN");
            }
            else{
                Debug.Log("ZEROED");
            }
        }
    }

    public Vector2 ReadInput()
        {
            return Joystick.action?.ReadValue<Vector2>() ?? Vector2.zero;
        }

    public void setState(bool value){
        active = value;
    }

    private void setNav(int input){
        //If the input has not been reset, skip
        if(!reset){
            return;
        }
        //Input is up
        else if(input == 1){
            reset = true;
            doUp(); 
        }
        //Input is down
        else if(input == -1){
            reset = true;
            doDown();

        }
        //Input is centered
        else{
            reset = false;
        }



    }

    private void doUp(){
        Debug.Log("we go up");
    }

    private void doDown(){
        Debug.Log("we go down");

    }
}

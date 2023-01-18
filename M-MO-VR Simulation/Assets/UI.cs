using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class UI : MonoBehaviour
{   
    public TextMeshProUGUI uiDisplay;
    public Slider mainSlider;
    public string text;
    public int size = 32;
    private bool hidden;

    [SerializeField] XRController controller;
    private InputDevice targetDevice;


    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    

    public void toggle(){

        if(hidden){
            uiDisplay.text = text;
            hidden = false;
        }
        else{
            uiDisplay.text = "";
            hidden = true;
        }
    }

    public void changeFontSize(){
        size = (int)mainSlider.value;
        uiDisplay.fontSize = size;
    }
    
}
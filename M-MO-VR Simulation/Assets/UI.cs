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
    public string[] text;
    public int size = 32;
    //private bool hidden = false;
    private bool menuButtonPress;

    public int index = 0;

    //[SerializeField] XRController controller;
    //private InputDevice targetDevice;

    //public GameObject UILayer;

    
    // Start is called before the first frame update
    void Start()
    {
        uiDisplay.text = text[index];
    }
    

    // Update is called once per frame
    void Update()
    { 
        /*
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

         if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPress ) && menuButtonPress){
            Debug.Log("Button Pressed");
         }
        */ 
    }
    
    public void updateText(){

    }

    public void testNext(){
        if(index == 6){
            index = 0;
        }
        else{
            index ++;
        }

        uiDisplay.text = text[index];

    }

    public void testPrev(){
        if(index == 0){
            index = 6;
        }
        else{
            index --;
        }

        uiDisplay.text = text[index];
    }
    /*
    public void toggle(){

        if(hidden){
            UILayer.SetActive(false);
            hidden = false;
        }
        else{
            UILayer.SetActive(true);
            hidden = true;
        }
    }
    */


    public void changeFontSize(){
        size = (int)mainSlider.value;
        uiDisplay.fontSize = size;
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TriggerCounter : MonoBehaviour
{
    [SerializeField] XRController controller;
    private int count;
    public TextMeshProUGUI textField;
    private Animator animatorController;
    private InputDevice targetDevice;

    /*
    private void Start()
    {
        
    }
    */

    private void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            count++;
        }

        textField.text = "Trigger Pulled - " + count;
    }
}
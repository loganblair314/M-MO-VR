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

    private void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        //get & print the trigger value
        float triggerValue;
        controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
        if (triggerValue >= 0.1f) Debug.Log($"Trigger Pressed, value {triggerValue}");
        if (triggerValue >= 0.1f) animatorController.SetFloat("Trigger", triggerValue);

        //get & print the grip value
        float gripValue;
        controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue);
        if (gripValue >= 0.1f) Debug.Log($"Grip Pressed, value {gripValue}");
        if (gripValue >= 0.1f) animatorController.SetFloat("Grip", gripValue);
    }
}
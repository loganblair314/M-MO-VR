using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Facebook.WitAi.TTS.Utilities;
using TMPro;
using System.Collections;
using System.Linq;

public class TTSButtonPress : MonoBehaviour
{
    [SerializeField] XRController controller;
    private Animator animatorController;
    private InputDevice targetDevice;
    [SerializeField] private TTSSpeaker _speaker;
    private int currLevel;
    private Vector3 lvl4DoorPos, lvl5DoorPos;
    private Quaternion lvl4DoorRot, lvl5DoorRot;
    private float time;
    public GameObject partialVis;
    public GameObject lvl4Door, lvl5Door;
    [SerializeField] private TextMeshProUGUI lvl1Des, lvl2Des, lvl3Des, lvl4Des, lvl5Des, lvl6Des, pVLabel1, pVLabel2, pVLabel3, pVName, pVInteractable, pVDetails, lvl1Time, lvl2Time, lvl3Time, lvl4Time, lvl5Time, lvl6Time, nextButton;

    // Start is called before the first frame update
    void Start()
    {
        currLevel = TeleportManager.index;
        lvl4DoorPos = lvl4Door.transform.position;
        lvl4DoorRot = lvl4Door.transform.rotation;
        lvl5DoorPos = lvl5Door.transform.position;
        lvl5DoorRot = lvl5Door.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Looking for left controller, specifically the X button.
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        // If we transitioned levels, stop all speakers and switch variable to current level to select the phrase.
        if (currLevel != TeleportManager.index)
        {
            StopAllCoroutines();
            _speaker.Stop();
            lvl4Door.transform.position = lvl4DoorPos;
            lvl4Door.transform.rotation = lvl4DoorRot;
            lvl5Door.transform.position = lvl5DoorPos;
            lvl5Door.transform.rotation = lvl5DoorRot;
            currLevel = TeleportManager.index;
            time = 0;
        }
        // For some reason there has to be a delay when utilizing this. No idea why.
        time += Time.deltaTime;
        if (time >= 2)
        {
            if ((!MenuManager.MenuOpen))
            {
                if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true)
                {
                    SayPhrase();
                    time = 0;
                }
            }
        }
    }

    public void SayPhrase()
    {
        StopAllCoroutines();
        // If the speaker is currently in use, stop on button press.
        if (_speaker.IsLoading || _speaker.IsSpeaking)
        {
            _speaker.Stop();
        }
        // Otherwise, speak the phrases for each respective level.
        else
        {
            if (currLevel == 0)
            {
                _speaker.Speak(lvl1Des.text);
            }
            if (currLevel == 1)
            {
                _speaker.Speak(lvl2Des.text);
            }
            if (currLevel == 2)
            {
                _speaker.Speak(lvl3Des.text);
            }
            if (currLevel == 3)
            {
                // Edge case: If details field is blank / clicking on walls.
                if (partialVis.activeSelf && (pVDetails.text != ""))
                {
                    StartCoroutine(SpeakDetails());
                }
                else if (partialVis.activeSelf && (pVDetails.text == ""))
                {
                    StartCoroutine(SpeakNoDetails());
                }
                else
                {
                    _speaker.Speak(lvl4Des.text);
                }
            }
            if (currLevel == 4)
            {
                if (partialVis.activeSelf && (pVDetails.text != ""))
                {
                    StartCoroutine(SpeakDetails());
                }
                else if (partialVis.activeSelf && (pVDetails.text == ""))
                {
                    StartCoroutine(SpeakNoDetails());
                }
                else
                {
                    _speaker.Speak(lvl5Des.text);
                }
            }
            if (currLevel == 5)
            {
                if (partialVis.activeSelf && (pVDetails.text != ""))
                {
                    StartCoroutine(SpeakDetails());
                }
                else if (partialVis.activeSelf && (pVDetails.text == ""))
                {
                    StartCoroutine(SpeakNoDetails());
                }
                else
                {
                    _speaker.Speak(lvl6Des.text);
                }
            }
            if (currLevel == 6)
            {
                // Have to do a coroutine to speak multiple text boxes.
                StartCoroutine(SpeakTimes());
            }
        }
    }

    IEnumerator SpeakDetails()
    {
        string[] details = new string[] { pVLabel1.text, pVName.text, pVLabel2.text, pVInteractable.text, pVLabel3.text, pVDetails.text };
        for (int i = 0; i < details.Length; i++)
        {
            _speaker.Speak(details[i]);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator SpeakNoDetails()
    {
        string[] noDetails = new string[] { pVLabel1.text, pVName.text, pVLabel2.text, pVInteractable.text, pVLabel3.text };
        for (int i = 0; i < noDetails.Length; i++)
        {
            _speaker.Speak(noDetails[i]);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator SpeakTimes()
    {
        string[] texts = new string[] { lvl1Time.text, lvl2Time.text, lvl3Time.text, lvl4Time.text, lvl5Time.text, lvl6Time.text };
        for (int i = 0; i < texts.Length; i++)
        {
            _speaker.Speak(texts[i]);
            yield return new WaitForSeconds(4);
        }
    }

    public void ResetDoors()
    {
        lvl4Door.transform.position = lvl4DoorPos;
        lvl4Door.transform.rotation = lvl4DoorRot;
        lvl5Door.transform.position = lvl5DoorPos;
        lvl5Door.transform.rotation = lvl5DoorRot;
    }
}

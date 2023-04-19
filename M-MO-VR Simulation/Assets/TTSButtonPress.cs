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
    private float time;
    // Import menu; if it is active, then play the controls, otherwise play the level description.
    public GameObject partialVis;
    public GameObject uiMenu;
    [SerializeField] private TextMeshProUGUI pVName, pVInteractable, pVDetails, lvl1Time, lvl2Time, lvl3Time, lvl4Time, lvl5Time, lvl6Time;
    public AudioSource source;
    public AudioClip abstractAbstract, abstractBlack, abstractBlue, abstractCircles, book, chiselCurved, chiselFlat, chiselNarrow, coffee, cookie, door, dogBook, doughnut, fileFlat, laptopMac, laptopWindows;
    public AudioClip lvl1Des, lvl1DesWaypoint, lvl2Des, lvl3Des, lvl4Des, lvl5Des, lvl6Des, lvlDesControls, lvlDesDataDisplay, lvlDesDoor, lvlDesDoorDetection, lvlDesMenu, lvlDesOffice, melon, notebook, peach;
    public AudioClip penBlack, penBlue, penGreen, penRed, pizza, potPlant, raspRound, snakePlant, sodaBottle, sodaCan, spikeSmall, stapler, succulentsBig, succulentsSmall, tomsNotebook, noDetails, wall, obstacle;
    // Start is called before the first frame update
    void Start()
    {
        currLevel = TeleportManager.index;
        GameObject obj = GameObject.Find("XR Origin V2");
        source = obj.GetComponent<AudioSource>();
        //StartCoroutine(speakLvl1());
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
            source.Stop();
            currLevel = TeleportManager.index;
            time = 0;
        }
        // For some reason there has to be a delay when utilizing this. No idea why.
        time += Time.deltaTime;
        if (time >= 2)
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true)
            {
                SayPhrase();
                time = 0;
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
            if ((source.isPlaying == false))
            {
                PlayAudioClip();
            }
            else
            {
                source.Stop();
                //source.PlayOneShot(teleportDing);
                //PlayAudioClip();
            }
        }
    }

    public void PlayAudioClip()
    {
        // Level 1
        if (currLevel == 0)
        {
            //_speaker.Speak(lvl1Des.text);
            if (uiMenu.activeSelf)
            {
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                StartCoroutine(speakLvl1());
                //source.PlayOneShot(lvl1Des);
                //source.PlayOneShot(lvl1DesWaypoint);
            }
        }

        // Level 2
        if (currLevel == 1)
        {
            // _speaker.Speak(lvl2Des.text);
            if (uiMenu.activeSelf)
            {
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                source.PlayOneShot(lvl2Des);
            }
        }

        // Level 3
        if (currLevel == 2)
        {
            //_speaker.Speak(lvl3Des.text);
            if (uiMenu.activeSelf)
            {
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                source.PlayOneShot(lvl3Des);
            }
        }

        // Level 4
        if (currLevel == 3)
        {
            // Edge case: If details field is blank / clicking on walls.
            if (partialVis.activeSelf && !uiMenu.activeSelf && (pVDetails.text != ""))
            {
                speakPV();
                //StartCoroutine(SpeakDetails());
            }
            else if (partialVis.activeSelf && (pVDetails.text == ""))
            {
                // Otherwise, mention there is nothing of note.
                source.PlayOneShot(noDetails);
            }
            else if (uiMenu.activeSelf && !partialVis.activeSelf)
            {
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                StartCoroutine(speakLvl4());
            }
        }

        // Level 5
        if (currLevel == 4)
        {
            if (partialVis.activeSelf && (pVDetails.text != ""))
            {
                speakPV();
                //StartCoroutine(SpeakDetails());
            }
            else if (partialVis.activeSelf)
            {
                // Otherwise, mention there is nothing of note.
                source.PlayOneShot(noDetails);
            }
            else if (uiMenu.activeSelf && !partialVis.activeSelf)
            {
                //_speaker.Speak(lvl4Des.text);
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                StartCoroutine(speakLvl5());
            }
        }

        // Level 6
        if (currLevel == 5)
        {
            if (partialVis.activeSelf && (pVDetails.text != ""))
            {
                speakPV();
                //StartCoroutine(SpeakDetails());
            }
            else if (partialVis.activeSelf && (pVDetails.text == ""))
            {
                // Otherwise, mention there is nothing of note.
                source.PlayOneShot(noDetails);
            }
            else if (uiMenu.activeSelf && !partialVis.activeSelf)
            {
                //_speaker.Speak(lvl4Des.text);
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                source.PlayOneShot(lvl6Des);
            }
        }

        // Level 7 - Data Display
        if (currLevel == 6)
        {
            if (uiMenu.activeSelf && !partialVis.activeSelf)
            {
                //_speaker.Speak(lvl4Des.text);
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                // Have to do a coroutine to speak multiple text boxes.
                StartCoroutine(SpeakTimes());
            }
        }

        // Level 8 - Office
        if (currLevel == 7)
        {
            if (partialVis.activeSelf && (pVDetails.text != ""))
            {
                //StartCoroutine(SpeakDetails());
                speakPV();
            }
            else if (partialVis.activeSelf && (pVDetails.text == ""))
            {
                // Otherwise, mention there is nothing of note.
                source.PlayOneShot(noDetails);
            }
            else if (uiMenu.activeSelf && !partialVis.activeSelf)
            {
                source.PlayOneShot(lvlDesControls);
            }
            else
            {
                StartCoroutine(speakLvl8());
            }
        }
    }

    public void speakPV()
    {
        if (pVName.text == "Door" || pVName.text == "Door Handle")
        {
            // New audio clip: "This is an interactable door"
            source.PlayOneShot(door);
        }
        else if (pVName.text == "Pizza")
        {
            source.PlayOneShot(pizza);
        }
        else if (pVName.text == "Soda Bottle")
        {
            source.PlayOneShot(sodaBottle);
        }
        else if (pVName.text == "Cookie")
        {
            source.PlayOneShot(cookie);
        }
        else if (pVName.text == "Doughnut")
        {
            source.PlayOneShot(doughnut);
        }
        else if (pVName.text == "Laptop Base" || pVName.text == "Laptop Screen")
        {
            source.PlayOneShot(laptopMac);
        }
        else if (pVName.text == "laptop screen " || pVName.text == "Laptop")
        {
            source.PlayOneShot(laptopWindows);
        }
        else if (pVName.text == "Black Pen")
        {
            source.PlayOneShot(penBlack);
        }
        else if (pVName.text == "Blue Pen")
        {
            source.PlayOneShot(penBlue);
        }
        else if (pVName.text == "Green Pen")
        {
            source.PlayOneShot(penGreen);
        }
        else if (pVName.text == "Red Pen")
        {
            source.PlayOneShot(penRed);
        }
        else if (pVName.text == "Flat Chisel")
        {
            source.PlayOneShot(chiselFlat);
        }
        else if (pVName.text == "Curved Chisel")
        {
            source.PlayOneShot(chiselCurved);
        }
        else if (pVName.text == "Narrow Chisel")
        {
            source.PlayOneShot(chiselNarrow);
        }
        else if (pVName.text == "File")
        {
            source.PlayOneShot(fileFlat);
        }
        else if (pVName.text == "Rasp")
        {
            source.PlayOneShot(raspRound);
        }
        else if (pVName.text == "Stapler")
        {
            source.PlayOneShot(stapler);
        }
        else if (pVName.text == "Book")
        {
            source.PlayOneShot(book);
        }
        else if (pVName.text == "Small Spiked Plant")
        {
            source.PlayOneShot(spikeSmall);
        }
        else if (pVName.text == "Sucklent Plant" || pVName.text == "small pot terracotta")
        {
            source.PlayOneShot(succulentsSmall);
        }
        else if (pVName.text == "Dog Book")
        {
            source.PlayOneShot(dogBook);
        }
        else if (pVName.text == "Pot Plant" || pVName.text == "Cement pot medum")
        {
            source.PlayOneShot(potPlant);
        }
        else if (pVName.text == "Snake Plant" || pVName.text == "Leaves" || pVName.text == "Cement Pot")
        {
            source.PlayOneShot(snakePlant);
        }
        else if (pVName.text == "Abstract Art (Grid)")
        {
            source.PlayOneShot(abstractBlack);
        }
        else if (pVName.text == "Notebook")
        {
            source.PlayOneShot(notebook);
        }
        else if (pVName.text == "Tom's Notebook")
        {
            source.PlayOneShot(tomsNotebook);
        }
        else if (pVName.text == "Coffee Cup")
        {
            source.PlayOneShot(coffee);
        }
        else if (pVName.text == "Melon")
        {
            source.PlayOneShot(melon);
        }
        else if (pVName.text == "Peach")
        {
            source.PlayOneShot(peach);
        }
        else if (pVName.text == "Soda Can")
        {
            source.PlayOneShot(sodaCan);
        }
        else if (pVName.text == "Abstract Art (Lines)")
        {
            source.PlayOneShot(abstractAbstract);
        }
        else if (pVName.text == "Small Spiked Plant")
        {
            source.PlayOneShot(spikeSmall);
        }
        else if (pVName.text == "Abstract Art (Blue)")
        {
            source.PlayOneShot(abstractBlue);
        }
        else if (pVName.text == "Abstract Art (Circles)")
        {
            source.PlayOneShot(abstractCircles);
        }
        else if (pVName.text == "Wall")
        {
            source.PlayOneShot(wall);
        }
        else if (pVName.text == "Obstacle")
        {
            source.PlayOneShot(obstacle);
        }
        else
        {
            source.PlayOneShot(noDetails);
        }
    }


    IEnumerator speakLvl1()
    {
        AudioClip[] lvl1 = new AudioClip[] { lvl1Des, lvl1DesWaypoint, lvlDesMenu };
        for (int i = 0; i < lvl1.Length; i++)
        {
            source.PlayOneShot(lvl1[i]);
            yield return new WaitForSeconds(7.21f);
        }
    }

    IEnumerator speakLvl4()
    {
        AudioClip[] lvl4 = new AudioClip[] { lvl4Des, lvlDesDoor, lvlDesDoorDetection };
        for (int i = 0; i < lvl4.Length; i++)
        {
            source.PlayOneShot(lvl4[i]);
            yield return new WaitForSeconds(7);
        }
    }

    IEnumerator speakLvl5()
    {
        AudioClip[] lvl5 = new AudioClip[] { lvl5Des, lvlDesDoor, lvlDesDoorDetection };
        for (int i = 0; i < lvl5.Length; i++)
        {
            source.PlayOneShot(lvl5[i]);
            yield return new WaitForSeconds(7);
        }
    }

    IEnumerator speakLvl8()
    {
        AudioClip[] lvl8 = new AudioClip[] { lvlDesOffice, lvlDesDoor, lvlDesDoorDetection };
        for (int i = 0; i < lvl8.Length; i++)
        {
            source.PlayOneShot(lvl8[i]);
            yield return new WaitForSeconds(7);
        }
    }

    IEnumerator SpeakTimes()
    {
        source.PlayOneShot(lvlDesDataDisplay);
        yield return new WaitForSeconds(6);
        string[] texts = new string[] { lvl1Time.text, lvl2Time.text, lvl3Time.text, lvl4Time.text, lvl5Time.text, lvl6Time.text };
        for (int i = 0; i < texts.Length; i++)
        {
            _speaker.Speak(texts[i]);
            yield return new WaitForSeconds(4);
        }
    }
}

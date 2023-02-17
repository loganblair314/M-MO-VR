/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

// Code adapted from Unity's original TTS Speaker Input.

using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using TMPro;
using System.Collections;
using System.Linq;


public class TTSSpeakerInputData : MonoBehaviour
{
    //[SerializeField] private Text _title;
    //[SerializeField] private InputField _input;
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private TextMeshProUGUI textField1, textField2, textField3, textField4, textField5, textField6;
    
    public void Update()
    {
        if (TeleportManager.index != 6)
        {
            StopAllCoroutines();
        }
    }

    // Either say the current phrase or stop talking/loading
    public void SayPhrase()
    {
        StopAllCoroutines();
        if (_speaker.IsLoading || _speaker.IsSpeaking)
        {
            _speaker.Stop();
        }
        else
        {
            StartCoroutine(SpeakText());
        }
    }

    IEnumerator SpeakText()
    {
        string[] texts = new string[] { textField1.text, textField2.text, textField3.text, textField4.text, textField5.text, textField6.text };
        for (int i = 0; i < texts.Length; i++)
        {
            _speaker.Speak(texts[i]);
            yield return new WaitForSeconds(4);
        }
    }
}
/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using TMPro;


public class TTSSpeakerInput : MonoBehaviour
{
    [SerializeField] private Text _title;
    [SerializeField] private InputField _input;
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private TextMeshProUGUI textField;


    // Either say the current phrase or stop talking/loading
    public void SayPhrase()
    {
        _speaker.Speak(textField.text);

        /*
        if (_speaker.IsLoading || _speaker.IsSpeaking)
        {
            _speaker.Stop();
        }
        else
        {
            _speaker.Speak(textField.text);
        }
        */
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    //Loads An Array of Audio Clips From the Notes Folder
    public AudioSource[] audioClips;
    AudioSource note;
    float hitY;

    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll<AudioSource>("Notes");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Selects The Audio Clip To Play In Order By Height
        //Finds the Y value Hit By a Ray
        if (Physics.Raycast(startPos, newTarg, out hit, 1)) {
                hitY = hit.point.y;
        }

        //Selects audio Clip by Height
        if(hitY > 0 && hitY < 0.3){
            note = audioClips[0];
        }
        else if(hitY >= 0.3 && hitY < 0.6){
            note = audioClips[2];
        }
        else if(hitY >= 0.6 && hitY < 0.9){
            note = audioClips[4];
        }
        else if(hitY >= 0.9 && hitY < 1.2){
            note = audioClips[6];
        }
        else if(hitY >= 1.2 && hitY < 1.5){
            note = audioClips[8];
        }
        else if(hitY >= 1.5 && hitY < 1.8){
            note = audioClips[1];
        }
        else if(hitY >= 1.8 && hitY < 2.1){
            note = audioClips[3];
        }
        else if(hitY >= 2.1 && hitY < 2.4){
            note = audioClips[5];
        }
        else if(hitY >= 2.4 && hitY < 2.7){
            note = audioClips[7];
        }
        else{
            note = audioClips[9];
        }
    }
}
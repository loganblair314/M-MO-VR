using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayBoard : MonoBehaviour
{
    public TextMeshProUGUI board;
    public string text;

    public Slider mainSlider;

    public int size = 5;

    
    // Start is called before the first frame update
    void Start()
    {
        board.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFontSize(){
        size = (int)mainSlider.value;
        board.fontSize = size;
    }

}


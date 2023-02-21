using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayBoard : MonoBehaviour
{
    public TextMeshProUGUI board;
    public string text;

    public GameObject UI_Manager; 
    UI ui;

    public Slider mainSlider;

    public int size = 5;
    [SerializeField] private int index;

    
    // Start is called before the first frame update
    void Start()
    {
        if(UI_Manager != null && UI_Manager.GetComponent<UI>() != null){
            ui = UI_Manager.GetComponent<UI>();

            text = ui.text[index];
        }
        
        
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


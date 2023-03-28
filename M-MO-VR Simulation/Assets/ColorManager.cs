using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ColorManager : MonoBehaviour
{

    GameObject[] foreground;
    GameObject[] background;

    [SerializeField] Color32 backgroundColor = Color.black;
    [SerializeField] Color32 foregroundColor = Color.white;
    [SerializeField] Color32 tColor = Color.green;
    [SerializeField] Color32 fColor = Color.red;

    //public GameObject test;


    // Start is called before the first frame update
    void Start()
    {
        foreground = FindGameObjectsWithTags(new string[]{"Foreground","Foreground Interactable"});
        background = FindGameObjectsWithTags(new string[]{"Background3D","BackgroundUI"});
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<Renderer>().material.color);
        //changeBackgroundColors(Color.cyan);
        //test.GetComponent<Image>().color = Color.cyan;
    }

    public void defaultColors(){
        
        //Reset Background Colors
        foreach (var item in background)
        {   
            //If the background object is the 3D plane
            if(item.tag == "Background3D"){
                item.GetComponent<Renderer>().material.SetColor("_Color",Color.black);
            }
            //Else this is the UI background
            else{
                item.GetComponent<Image>().color = Color.black;
            }
        }

        //Reset Foreground Colors
        foreach (var item in foreground)
        {   
            //If the foreground is the standard UI text
            if(item.tag == "Foreground"){
                item.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            //Else, it is the true/false text
            else{
                item.GetComponent<PartialVis>().setColors(Color.green, Color.red);
            }
        }
    }

    void changeForegroundColors(Color32 color,Color32 tColor,Color32 fColor){
        foreach (var item in foreground)
        {   
            //If the foreground is the standard UI text
            if(item.tag == "Foreground"){
                item.GetComponent<TextMeshProUGUI>().color = color;
            }
            //Else, it is the true/false text
            else{
                item.GetComponent<PartialVis>().setColors(tColor, fColor);
            }
        }
    }

    void changeBackgroundColors(Color32 color){
        foreach (var item in background)
        {   
            //If the background object is the 3D plane
            if(item.tag == "Background3D"){
                item.GetComponent<Renderer>().material.SetColor("_Color",color);
                //Debug.Log("Changing the color of "+gameObject+" to "+color);
            }
            //Else this is the UI background
            else{
                item.GetComponent<Image>().color = color;
            }
        }
    }

    GameObject[] FindGameObjectsWithTags(params string[] tags)
     {
         var all = new List<GameObject>() ;
         
         foreach(string tag in tags)
         {
             var temp = GameObject.FindGameObjectsWithTag(tag).ToList() ;
             all = all.Concat(temp).ToList() ;
         }
         
         return all.ToArray() ;
     }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    GameObject[] foreground;
    GameObject[] background;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<Renderer>().material.color);
        GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
    }

    public void defaultColors(){
        foreach (var item in background)
        {   
            //If the background object is the 3D plane
            if(item.tag == "Background3D"){
                item.GetComponent<Renderer>().material.SetColor("_Color",Color.black);
            }
            //Else this is the UI background
            else{

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string type;
    public string color;
    public string size;

    private Material material;

    void Start()
    {
        type = gameObject.name;

        color = gameObject.GetComponent<Renderer>().material.name;
        int indexOfSpace = color.IndexOf(" ");
        if (indexOfSpace >= 0) color = color.Substring(0, indexOfSpace);

        float totalSize = gameObject.transform.localScale.x + gameObject.transform.localScale.y + gameObject.transform.localScale.z;
        if (totalSize < 0.8) size = "Small";
        else if(totalSize >= 0.8 && totalSize < 2) size = "Medium";
        else if (totalSize > 2) size = "Large";
    }

    void Update()
    {
        
    }
}
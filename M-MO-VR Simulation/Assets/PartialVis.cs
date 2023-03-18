using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;




public class PartialVis : MonoBehaviour
{   
    public Transform raycastOrigin;
    public InputActionProperty button;

    public TextMeshProUGUI interactable;
    public TextMeshProUGUI details;
    public TextMeshProUGUI obj_name;

    Object objectInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button.action.WasPressedThisFrame()){
            Scan();
        }
    }

    

    void Scan(){
        //Raycast send
        RaycastHit hit;
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
        
        
        //If the raycast hits
        if(Physics.Raycast(ray, out hit, 100)) // Ray hit something
        {
            //Debug.Log(hit.collider.tag);
            //Debug.Log(hit.collider.gameObject);
            //Debug.Log(hit.collider.name);

            //Set the interactable field appropriately
            if(hit.collider.tag == "Interactable"){
                interactable.text = "True";
                interactable.color = new Color32(0,255,0,255);
            } else{
                interactable.text = "False";
                interactable.color = new Color32(255,0,0,255);
            }




            //If the object hit has an object script
            if(hit.collider.gameObject.GetComponent<Object>() != null){
                objectInfo = hit.collider.gameObject.GetComponent<Object>();
                Debug.Log("This is a "+objectInfo.objectName);

                details.text = objectInfo.description + "\n";
                obj_name.text = objectInfo.objectName;
            } 
            else {
                details.text = "";
                obj_name.text = "";
            }
        }
    }
    public void resetText(){
        interactable.text = "";
        details.text = "";
    }
    
}


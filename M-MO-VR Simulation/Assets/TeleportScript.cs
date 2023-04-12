using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{   
	public string objectTag = "Player";
	//height offset
	public float teleportationHeightOffset = 1;
	//private bool checking if you entered the trigger
	private bool inside;
	//gameobjects inside the pad
	private Transform subject;
	//add a sound component if you want the teleport playing a sound when teleporting
	public AudioSource teleportSound;
	public AudioSource source;
	//simple enable/disable function in case you want the teleport not working at some point
	//without disabling the entire script, so receiving objects still works
	public bool teleportPadOn = true;

	public AudioClip teleportDing;

	TeleportManager manager;
    UI uiManager;

	void Start ()
	{
        manager = GameObject.FindGameObjectWithTag("TeleportManager").GetComponent<TeleportManager>();
        uiManager = GameObject.FindGameObjectWithTag("UI-Manager").GetComponent<UI>();
		GameObject obj = GameObject.Find("XR Origin V2");
		source = obj.GetComponent<AudioSource>();
	}


	void Update ()
	{
		//check if theres something/someone inside
		if(inside)
		{
			Teleport();
            inside = false;
        }
	}

	void Teleport()
	{
		//and teleport the subject
		manager.nextRoom(); 
        manager.resetPosition();
        uiManager.testNext();
		//play teleport sound
		//teleportSound.Play();
		if ((source.isPlaying == false))
		{
			source.PlayOneShot(teleportDing);
		}
		else
        {
			source.Stop();
			source.PlayOneShot(teleportDing);
		}
		Debug.Log("Player Entered the Teleporter");
			
	}

	void OnTriggerEnter(Collider trig)
	{
		//when an object enters the trigger
		//if you set a tag in the inspector, check if an object has that tag
		//otherwise the pad will take in and teleport any object
		if(objectTag != "")
		{
			//if the objects tag is the same as the one allowed in the inspector
			if(trig.gameObject.tag == objectTag)
			{
				//set the subject to be the entered object
				subject = trig.transform;
				//and check inside, ready for teleport
				inside = true;
				//if its a button teleport, the pad should be set to be ready to teleport again
				//so the player/object doesn't have to leave the pad, and re enter again, we do that here
			}
		}
		else
		{
			//set the subject to be the entered object
			subject = trig.transform;
			//and check inside, ready for teleport
			inside = true;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject Player;
    private GameObject Current_Spawn;
    public GameObject[] Spawns;

    public int index = 0;
    
    //public GameObject DDR_Spawn;

   


    // Start is called before the first frame update
    void Start()
    {
        //For initial testing, we will set the spawn to the display room
        Current_Spawn = Spawns[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPosition(){
        Player.transform.position = Current_Spawn.transform.position;
        Player.transform.rotation = Current_Spawn.transform.rotation;
        //Player.transform.LookAt();
    }

    public void previousRoom(){
        if(index != 0){
            index --;
        }
    }

    public void nextRoom(){
        if(index != 6){
            index ++;
        }
    }
}

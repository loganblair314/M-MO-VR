using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTeleportManager : MonoBehaviour
{

    GameManger gm;
    public int NextLevel;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            gm.LoadNextLevel(NextLevel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     gm = gameObject.GetComponent<GameManger>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

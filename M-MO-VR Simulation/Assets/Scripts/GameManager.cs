using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public void LoadNextLevel(int x) { 
    
        SceneManager.LoadScene(x);
    }

    void Start()
    {

    }

    private void Update()
    {
        
    }
}

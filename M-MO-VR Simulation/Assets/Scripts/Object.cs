using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string objectName;
    [SerializeField] public string description;

    void Start()
    {
        objectName = gameObject.name;
    }

    void Update()
    {
        
    }
}
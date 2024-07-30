using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public float Deepness {get; set;}
    public PlayerController player;
    public float currentSpeed;

    void Start(){
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else {
            instance = this;
        }
        
        
    }

    void Update(){

    }
}

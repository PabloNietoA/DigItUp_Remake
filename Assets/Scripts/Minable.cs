using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour
{
    [SerializeField] int value;
    private bool notCollided = true;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(notCollided) Manager.instance.addMoney(value);
            notCollided = false;
            Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    [Header("Speed")]
    [SerializeField] float baseSpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxSpeed;
    [Header("Rotation")]
    [SerializeField] float maxAngle;
    [SerializeField] float rotationSmoothness;
    [Header("Fuel")]
    [SerializeField] float baseFuel;
    [SerializeField] float currentFuel;
    [SerializeField] float maxFuel;
    [Header("Capacity")]
    [SerializeField] int baseCapacity;
    [SerializeField] int currentCapacity;
    [SerializeField] int maxCapacity;
    [SerializeField] int currentLoad;
 

    // Update is called once per frame
    void Update()
    {
        rotatePlayer(); 
    }
    void rotatePlayer()
    {
        float h = Input.GetAxis("Horizontal") * maxAngle;
        Quaternion angle = Quaternion.Euler(0, 0, h);
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * rotationSmoothness);
    }

    
}

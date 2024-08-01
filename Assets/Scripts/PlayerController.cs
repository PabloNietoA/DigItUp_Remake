using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    [Header("Speed")]
    [SerializeField] float baseSpeed { get; set;}
    [SerializeField] float currentSpeed { get; set;}
    [SerializeField] float maxSpeed;


    [Header("Rotation")]
    [SerializeField] float maxAngle;
    [SerializeField] float turnSpeed;


    [Header("Fuel")]
    [SerializeField] float baseFuel;
    [SerializeField] float currentFuel;
    [SerializeField] float maxFuel;


    [Header("Capacity")]
    [SerializeField] int baseCapacity;
    [SerializeField] float currentCapacity;
    [SerializeField] float maxCapacity;
    [SerializeField] float currentLoad;

    // Update is called once per frame
    void Update()
    {
        rotatePlayer();

    }
    void rotatePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Quaternion quaternion = Quaternion.Euler(0, 0, h * maxAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * turnSpeed);
    }
    
    //Getters y setters
    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = value; } }
}

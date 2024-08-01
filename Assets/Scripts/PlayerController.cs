using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    [Header("Speed")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;


    [Header("Rotation")]
    [SerializeField] private float maxAngle;
    [SerializeField] private float turnSpeed;


    [Header("Fuel")]
    [SerializeField] private float baseFuel;
    [SerializeField] private float currentFuel;
    [SerializeField] private float maxFuel;


    [Header("Capacity")]
    [SerializeField] private int baseCapacity;
    [SerializeField] private float currentCapacity;
    [SerializeField] private float maxCapacity;
    [SerializeField] private float currentLoad;

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

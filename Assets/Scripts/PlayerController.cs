using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //VARIABLES
    [Header("Speed")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    private float currentXSpeed;
    private float currentYSpeed;
    private float currentAngleInRadians;


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


    private void Awake()
    {
        // Almacena el primer script creado, que se puede acceder estáticamente
        // Así tenemos una sola variable estática de la que consultamos variables
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calcular angulo actual en radianes
        currentAngleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
        //Calcular la descomposicion de la velocidad en x e y
        currentXSpeed = currentSpeed * Mathf.Sin(currentAngleInRadians);
        currentYSpeed = currentSpeed * Mathf.Cos(currentAngleInRadians);

        rotatePlayer();
    }
    void rotatePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Quaternion quaternion = Quaternion.Euler(0, 0, h * maxAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * turnSpeed);
    }
    
    //Getters y setters (Properties)
    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = value; } }
    public float CurrentXSpeed { get { return currentXSpeed; } set { currentXSpeed = value; } }
    public float CurrentYSpeed { get { return currentYSpeed; } set { currentYSpeed = value; } }

}

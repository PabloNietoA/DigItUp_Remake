using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private float fuelPerSecConsumption; //Consumo de fuel por segundo
    [SerializeField] private Image fuelBarImage; //Barra del color fill del fuel en la UI
    [SerializeField] private Gradient fuelBarGradient; //Gradiente de colores en el que transiciona la barra de fuel


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
        maxFuel = baseFuel; //En el futuro maxFuel se verá influenciado por el nivel del fuel, de momento igual a baseFuel
        currentFuel = maxFuel; //El fuel al inicio es igual al maxFuel
    }

    // Update is called once per frame
    void Update()
    {
        //Calcular angulo actual en radianes
        currentAngleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
        //Calcular la descomposicion de la velocidad en x e y
        currentXSpeed = currentSpeed * Mathf.Sin(currentAngleInRadians);
        currentYSpeed = currentSpeed * Mathf.Cos(currentAngleInRadians);

        //Rotar el player
        rotatePlayer();
        //Updatear el fuel y la barra de fuel
        UpdateFuel();
    }
    //Funcion para rotar el player
    void rotatePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Quaternion quaternion = Quaternion.Euler(0, 0, h * maxAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * turnSpeed);
    }

    //Funcion para updatear el estado del fuel y la barra de fuel en la UI
    void UpdateFuel()
    {
        currentFuel -= fuelPerSecConsumption * Time.deltaTime;
        fuelBarImage.fillAmount = currentFuel / maxFuel;
        fuelBarImage.color = fuelBarGradient.Evaluate(fuelBarImage.fillAmount);
    }
    
    //Getters y setters (Properties)
    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = value; } }
    public float CurrentXSpeed { get { return currentXSpeed; } set { currentXSpeed = value; } }
    public float CurrentYSpeed { get { return currentYSpeed; } set { currentYSpeed = value; } }

}

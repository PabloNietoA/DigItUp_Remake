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


    [Header("Rotation")]
    [SerializeField] private float baseAngle;
    [SerializeField] private float maxAngle;
    private float currentAngleInRadians;

    [SerializeField] private float baseTurnSpeed; //Velocidad de giro base
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float currentTurnSpeed;

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

    
    private float multiplier = 0.1f; //TEMPORAL, se cambiará en el futuro


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
    void Start()
    {
        //Las id de ItemLevels[id] no son las finales, se cambiarán en el futuro además del factor de multiplicacion
        //Speed
        maxSpeed = baseSpeed * (1 + Manager.instance.ItemLevels[0] * multiplier); //ItemLevels[0] es el nivel del motor, a cambiar en el futuro por la id del motor
        currentSpeed = maxSpeed; //La velocidad al inicio es igual a la maxSpeed
        //Fuel
        maxFuel = baseFuel * (1 + Manager.instance.ItemLevels[1] * multiplier); //ItemLevels[1] es el nivel del combustible, a cambiar en el futuro por la id del combustible
        currentFuel = maxFuel; //El fuel al inicio es igual al maxFuel
        //Angle
        maxAngle = baseAngle * (1 + Manager.instance.ItemLevels[2] * multiplier); //ItemLevels[2] es el nivel del ángulo, a cambiar en el futuro por la id del ángulo
        currentAngleInRadians = maxAngle;
        //Turn Speed
        maxTurnSpeed = baseTurnSpeed * (1 + Manager.instance.ItemLevels[3] * multiplier); //ItemLevels[3] es el nivel de la velocidad de giro, a cambiar en el futuro por la id de la velocidad de giro
        currentTurnSpeed = maxTurnSpeed;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * currentTurnSpeed);
    }

    //Funcion para updatear el estado del fuel y la barra de fuel en la UI
    void UpdateFuel()
    {
        currentFuel -= fuelPerSecConsumption * Time.deltaTime;
        fuelBarImage.fillAmount = currentFuel / maxFuel;
        fuelBarImage.color = fuelBarGradient.Evaluate(fuelBarImage.fillAmount);
    }

    //Funcion para detectar colisiones con objetos minables
    void OnTriggerEnter2D(Collider2D collider)
    {
       Debug.Log("Colision con " + collider.gameObject.name);
        IMineable minable = collider.GetComponent<IMineable>();
        if (minable != null)
        {
            minable.Mined();
        }
    }

    //Getters y setters (Properties)
    public float CurrentSpeed { get { return currentSpeed; } set { currentSpeed = value; } }
    public float CurrentXSpeed { get { return currentXSpeed; } set { currentXSpeed = value; } }
    public float CurrentYSpeed { get { return currentYSpeed; } set { currentYSpeed = value; } }

}

using System.Collections;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private GameObject brushPrefab;
    [SerializeField] private float timeBetweenInstances;
    [SerializeField] private bool automaticTimeBetweenInstances;
    [SerializeField] private float automaticBrushDensity;

    private float timeSinceLastInstance;
    private PlayerController playerController;

    

    void Start()
    {
        timeSinceLastInstance = timeBetweenInstances; // Instancia al inicio
        playerController = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Si el modo automatico esta puesto el tiempo de creacion de la brocha dependera de la velocidad a la que se vaya
        if (automaticTimeBetweenInstances) { timeBetweenInstances = 1 / (playerController.CurrentSpeed*automaticBrushDensity); }
        timeSinceLastInstance += Time.deltaTime;

        if (timeSinceLastInstance >= timeBetweenInstances)
        {
            Instantiate(brushPrefab, transform.position, transform.rotation);
            timeSinceLastInstance = 0;
        }
    }

    //Getters and setters
    public float TimeBetweenInstances { get { return timeBetweenInstances; } set { timeBetweenInstances = value; } }

}

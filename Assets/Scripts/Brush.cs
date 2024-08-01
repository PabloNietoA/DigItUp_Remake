using System.Collections;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private float destructionHeight;
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
            GameObject brushInstance = Instantiate(brush, transform.position, transform.rotation);
            StartCoroutine(Move(brushInstance));
            timeSinceLastInstance = 0;
        }
    }

    // Coroutine para hacer que las instancias se muevan
    private IEnumerator Move(GameObject instance)
    {
        while (instance != null)
        {
            if (instance.transform.position.y > destructionHeight)
            {
                Destroy(instance);
                yield break; // Stop the coroutine after destroying the GameObject
            }

            instance.transform.position += Vector3.up * playerController.CurrentYSpeed * Time.deltaTime;
            instance.transform.position += Vector3.left * playerController.CurrentXSpeed * Time.deltaTime;

            yield return null;
        }
    }

    //Getters and setters
    public float TimeBetweenInstances { get { return timeBetweenInstances; } set { timeBetweenInstances = value; } }

}

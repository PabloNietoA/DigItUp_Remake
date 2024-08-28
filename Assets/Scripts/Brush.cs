using System.Collections;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private GameObject brushPrefab;
    [SerializeField] private float timeBetweenInstances;
    [SerializeField] private bool automaticTimeBetweenInstances;
    [SerializeField] private float automaticBrushDensity;
    [SerializeField] private float destructionHeight;

    private float timeSinceLastInstance;
    [SerializeField] private Transform player;
    void Start()
    {
        player = transform.parent;
        timeSinceLastInstance = timeBetweenInstances; // Instancia al inicio
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si el modo automatico esta puesto el tiempo de creacion de la brocha dependera de la velocidad a la que se vaya
        if (automaticTimeBetweenInstances) { timeBetweenInstances = 1 / (PlayerController.instance.CurrentSpeed * automaticBrushDensity); }
        timeSinceLastInstance += Time.deltaTime;

        if (timeSinceLastInstance >= timeBetweenInstances)
        {
            GameObject brushInstance = Instantiate(brushPrefab, transform.position, transform.rotation);
            StartCoroutine(DestroyBrushInstances(brushInstance));
            timeSinceLastInstance = 0;
        }
    }

    // Coroutine to destroy brush instances
    IEnumerator DestroyBrushInstances(GameObject brushInstance)
    {
        while (true)
        {
            // Calcula la diferencia entre la posición del eje y del player y la del eje y de la brushInstance
            float yDifference = -(player.position.y - brushInstance.transform.position.y);
            // Si la diferencia es mayor que la destructionHeight, destruye el brushInstance
            if (yDifference > destructionHeight)
            {
                Destroy(brushInstance);
            }

            yield return null;
        }
    }

    /*-------------------METODOS PUBLICOS-------------------*/

    // Getters and setters
    public float TimeBetweenInstances { get { return timeBetweenInstances; } set { timeBetweenInstances = value; } }

    
}

using System.Collections;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private float destructionHeight;
    [SerializeField] private float timeBetweenInstances;
    [SerializeField] private bool automaticTimeBetweenInstances;
    [SerializeField] private float automaticBrushDensity;
    [SerializeField] private float speed;

    [SerializeField] private GameObject playerGameObject;

    private float timeSinceLastInstance;

    

    void Start()
    {
        timeSinceLastInstance = timeBetweenInstances; // Instancia al inicio
    }

    // Update is called once per frame
    void Update()
    {
        //La speed de movimiento de los sprites la coge del playerController
        speed = playerGameObject.GetComponent<PlayerController>().CurrentSpeed;

        //Si el modo automatico esta puesto el tiempo de creacion de la brocha dependera de la velocidad a la que se vaya
        if (automaticTimeBetweenInstances) { timeBetweenInstances = 1 / (speed*automaticBrushDensity); }
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

            float angleInRadians = playerGameObject.transform.eulerAngles.z * Mathf.Deg2Rad;
            float xSpeed = speed * Mathf.Sin(angleInRadians);
            float ySpeed = speed * Mathf.Cos(angleInRadians);

            instance.transform.position += Vector3.up * ySpeed * Time.deltaTime;
            instance.transform.position += Vector3.left * xSpeed * Time.deltaTime;

            yield return null;
        }
    }

    //Getters and setters
    public float Speed { get { return speed; } set { speed = value; } }
    public float TimeBetweenInstances { get { return timeBetweenInstances; } set { timeBetweenInstances = value; } }

}

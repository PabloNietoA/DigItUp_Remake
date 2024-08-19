using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [Header("Distance to center")]
    [SerializeField] private float verticalDistanceToCenter;
    [SerializeField] private float horizontalDistanceToCenter;

    [Header("Position Transforms")]
    [SerializeField] private Transform[] transforms = new Transform[6];
    
    [Header("Backgrounds")]
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject[] backgrounds = new GameObject[6];



    private void Start()
    {
        //Genera los backgrounds en las posiciones de los transforms y los almacena en el array backgrounds
        for (int i = 0; i < transforms.Length; i++)
        {
            //Si no hay background en la posición i, crea uno nuevo
            if (backgrounds[i] == null) //
            {
                GameObject newBackground = Instantiate(backgroundPrefab, transforms[i].position, Quaternion.identity);
                backgrounds[i] = newBackground;
            }
        }
    }
    private void Update()
    {
        UpdateBackGroundPositions();
    }
    private void UpdateBackGroundPositions()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Obtener la posición actual del background
            Vector3 backgroundPosition = backgrounds[i].transform.position;

            // Verificar si el background se ha salido de la grid hacia arriba
            if (backgroundPosition.y > verticalDistanceToCenter)
            {
                // Teletransportar el background a la posición de abajo
                backgroundPosition.y = transforms[3].position.y;
                backgrounds[i].transform.position = backgroundPosition;
            }
            // Verificar si el background se ha salido de la grid hacia la derecha
            else if (backgroundPosition.x > horizontalDistanceToCenter)
            {
                // Teletransportar el background a la posición de la izquierda
                backgroundPosition.x = transforms[1].position.x;
                backgrounds[i].transform.position = backgroundPosition;
            }
            // Verificar si el background se ha salido de la grid hacia la izquierda
            else if (backgroundPosition.x < (-horizontalDistanceToCenter))
            {
                // Teletransportar el background a la posición de la derecha
                backgroundPosition.x = transforms[2].position.x;
                backgrounds[i].transform.position = backgroundPosition;
            }
        }
    }
}

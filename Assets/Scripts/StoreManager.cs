using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [Header("Displayed Objects Reference")]
    [SerializeField] private DisplayItem[] displayItems; // Array de objetos que se pueden comprar

    [Header("Gold")]
    [SerializeField] private int gold; // Oro del jugador

    [Header("Selected Item")]
    [SerializeField] private int selectedIndex; // Indice del objeto seleccionado

    [Header("Level")]
    [SerializeField] private int[] itemLevels; // Niveles del objeto

    [Header("Visual Components")]
    [SerializeField] private TextMeshProUGUI goldText; // Texto que muestra el oro del jugador

    // Start is called before the first frame update
    void Awake() //Awake para que se ejecute la inicializacion antes del start de DisplayItem
    {
        // Inicializar los niveles de los objetos
        //displayItems = FindObjectsOfType<DisplayItem>(); /Funciona pero no aparecen en el orden correcto
        itemLevels = new int[displayItems.Length];
        for (int i = 0; i < displayItems.Length; i++)
        {
            itemLevels[i] = displayItems[i].Item.startLevel;
        }
    }

    private void Start()
    {
        //Seleccionar el primer objeto al inicio
        SelectItemByIndex(0);
        //Asignar el oro al texto
        goldText.text = gold.ToString() + "$";

    }
    public void SelectItemByIndex(int index)
    {
        //Marcar id como seleccionado
        selectedIndex = index;

        // Desactivar el sprite seleccionado de todos los objetos
        for (int i = 0; i < displayItems.Length; i++)
        {
            Debug.Log("Desactivando sprite seleccionado de " + displayItems[i].Item.name);
            displayItems[i].SelectedSprite.SetActive(false);
        }

        // Activar el sprite seleccionado del objeto seleccionado
        Debug.Log("Activando sprite seleccionado de " + displayItems[index].Item.name);
        displayItems[index].SelectedSprite.SetActive(true);
    }

    //Metodos para ser llamados por los botones de la UI

    public void BuyItem()
    {
        //Comprar el objeto seleccionado
        displayItems[selectedIndex].BuyItem(); //Llama al metodo BuyItem de DisplayItem desde donde se hace toda la logica de compra
        //Actualizar el texto del oro
        goldText.text = gold.ToString() + "$";

    }


    // Getters and Setters (Properties)
    public int[] ItemLevels { get { return itemLevels; } set { itemLevels = value; } }
    public int Gold { get { return gold; } set { gold = value; } }

    public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; } }
}

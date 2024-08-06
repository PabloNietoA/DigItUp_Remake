using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [Header("Buyable Objects Reference")]
    [SerializeField] private DisplayItem[] items; // Array de objetos que se pueden comprar

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
        //items = FindObjectsOfType<DisplayItem>(); /Funciona pero no aparecen en el orden correcto
        itemLevels = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            itemLevels[i] = items[i].Item.startLevel;
        }
    }

    private void Start()
    {
        //Seleccionar el primer objeto al inicio
        SelectItemByIndex(0);
        //Asignar el oro al texto
        goldText.text = gold.ToString() + "$";
        
    }
    public void SelectItemByIndex(int index) {      
        //Marcar id como seleccionado
        selectedIndex = index;

        // Desactivar el sprite seleccionado de todos los objetos
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("Desactivando sprite seleccionado de "+items[i].Item.name);
            items[i].SelectedSprite.SetActive(false);
        }

        // Activar el sprite seleccionado del objeto seleccionado
        Debug.Log("Activando sprite seleccionado de " + items[index].Item.name);
        items[index].SelectedSprite.SetActive(true);
    }

    // Getters and Setters (Properties)
    public  int[] ItemLevels { get { return itemLevels; } set { itemLevels = value; } }
    public int Gold { get { return gold; } set { gold = value; } }

    public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; } }
}

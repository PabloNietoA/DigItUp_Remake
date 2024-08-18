using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    [Header("Displayed Objects Reference")]
    [SerializeField] private DisplayItem[] displayItems; // Array de objetos que se pueden comprar

    [Header("Money")]
    [SerializeField] private int money; // Dinero del jugador

    [Header("Selected Item")]
    [SerializeField] private int selectedIndex; // Indice del objeto seleccionado

    [Header("Level")]
    [SerializeField] private int[] itemLevels; // Niveles del objeto

    [Header("Visual Components")]
    [SerializeField] private TextMeshProUGUI moneyText; // Texto que muestra el dinero del jugador

    void Awake() //Awake para que se ejecute la inicializacion antes del start de DisplayItem
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


        // Inicializar los niveles de los objetos
        //displayItems = FindObjectsOfType<DisplayItem>(); /Funciona pero no aparecen en el orden correcto
        itemLevels = new int[displayItems.Length];
        for (int i = 0; i < displayItems.Length; i++)
        {
            itemLevels[i] = displayItems[i].Item.startLevel;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Seleccionar el primer objeto al inicio
        SelectItemByIndex(0);
        //Asignar el dinero al texto
        moneyText.text = money.ToString() + "$";

    }

    /*-------------------METODOS PUBLICOS-------------------*/

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
        //Actualizar el texto del dinero
        moneyText.text = money.ToString() + "$";

    }


    // Getters and Setters (Properties)
    public int[] ItemLevels { get { return itemLevels; } set { itemLevels = value; } }
    public int Money { get { return money; } set { money = value; } }

    public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; } }
}

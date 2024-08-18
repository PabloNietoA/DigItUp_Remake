using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItem : MonoBehaviour
{
    [Header("Buyable Object Reference")]
    [SerializeField] private BuyableObject item; // Scriptable object que contiene los atributos del objeto que se puede comprar


    [Header("Level")]
    [SerializeField] private static int[] itemLevels; // Niveles del objeto

    [Header("Visual Components")]
    [SerializeField] private TextMeshProUGUI nameText; // Texto que muestra el nombre del objeto
    [SerializeField] private Image itemImage; // Imagen del objeto
    [SerializeField] private TextMeshProUGUI priceText; // Texto que muestra el precio del objeto
    [SerializeField] private TextMeshProUGUI levelText; // Texto que muestra el nivel actual del objeto
    [SerializeField] private GameObject selectedSprite; // Sprite que se activa cuando el objeto está seleccionado


    private int currentCost; // Coste del objeto

    // Start is called before the first frame update
    void Start()
    {
        //Autoasignar el store manager
        if (StoreManager.instance == null)
        {
            StoreManager.instance = FindObjectOfType<StoreManager>();
        }

        // Obtener los niveles de los objetos

        itemLevels = StoreManager.instance.ItemLevels;

        // Calcular el precio del objeto

        currentCost = Mathf.FloorToInt(item.initialCost * itemLevels[item.id] * item.priceIncreaseIndex);

        // Asignar los valores del objeto a los componentes visuales

        nameText.text = item.name;
        itemImage.sprite = item.sprite;
        priceText.text = currentCost.ToString() + "$";
        levelText.text = "Lv." + itemLevels[item.id].ToString();
    }

    /*-------------------METODOS PUBLICOS-------------------*/


    //Metodos para los botones

    //Metodo para comprar este objeto
    public void BuyItem()
    {
        // Comprobar si el jugador tiene suficiente oro
        if (StoreManager.instance.Money >= currentCost)
        {
            // Restar el oro
            StoreManager.instance.Money -= currentCost;
            // Incrementar el nivel del objeto
            itemLevels[item.id]++;
            // Actualizar el precio
            currentCost = Mathf.FloorToInt(item.initialCost * itemLevels[item.id] * item.priceIncreaseIndex);
            // Actualizar el texto del precio
            priceText.text = currentCost.ToString() + "$";
            // Actualizar el texto del nivel
            levelText.text = "Lv." + itemLevels[item.id].ToString();
            // Actualizar el nivel del objeto en el store manager
            StoreManager.instance.ItemLevels[item.id] = itemLevels[item.id];
        }
    }

    //Metodo para seleccionar este objeto
    public void SelectItem()
    {
        StoreManager.instance.SelectItemByIndex(item.id);
    }


    // Getters and Setters (Properties)
    public BuyableObject Item { get { return item; } set { item = value; } }
    public GameObject SelectedSprite { get { return selectedSprite; } set { selectedSprite = value; } }

}

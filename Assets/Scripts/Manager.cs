using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI txtDeepness;
    [SerializeField] private TextMeshProUGUI txtMoney;

    [Header("Levels")]
    [SerializeField] private int[] itemLevels; // Niveles de cada item. Por ejemplo itemLevels[0] es el nivel del motor idk

    private int money;

    // Awake se ejecuta en compilación, Start en ejecución
    private void Awake()
    {
        // Almacena el primer script creado, que se puede acceder estáticamente
        // Así tenemos una sola variable estática de la que consultamos variables
        if (instance != null && instance != this) { 
            Destroy(this); 
        } else { 
            instance = this; 
        }
    }
    void Start(){
        money = 0;
    }

    void Update(){
        txtDeepness.text = ((int) -PlayerController.instance.Deepness).ToString() + "m";
        txtMoney.text = "$" + money.ToString();
    }

    /*-------------------METODOS PUBLICOS-------------------*/

    public void AddMoney(int money){
        this.money += money;
    }

    public int[] ItemLevels {get { return itemLevels; } set { itemLevels = value; } }
}

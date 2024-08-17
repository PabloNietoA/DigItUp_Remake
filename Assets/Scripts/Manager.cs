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

    private float deepness;
    private int money;

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
        this.money = 0;
        this.deepness = 0f;
    }

    void Update(){

        //La speed de movimiento de los sprites la coge del playerController
        deepness += Time.deltaTime * PlayerController.instance.CurrentYSpeed;
        txtDeepness.text = ((int) deepness).ToString() + "m";
        txtMoney.text = "$" + money.ToString();
    }

    public void AddMoney(int money){
        this.money += money;
    }

    public float Deepness {get { return deepness; } set { deepness = value; } }
    public int[] ItemLevels {get { return itemLevels; } set { itemLevels = value; } }
}

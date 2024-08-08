using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public TextMeshProUGUI txtDeepness;
    public TextMeshProUGUI txtMoney;

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

    void addMoney(int money){
        this.money += money;
    }

    public float Deepness {get { return deepness; } set { deepness = value; } }
}

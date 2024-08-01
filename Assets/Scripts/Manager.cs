using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public TextMeshProUGUI txtDeepness;
    private float deepness {get; set;}
    private PlayerController player;
    private float currentSpeed;

    

    void Start(){
        // Almacena el primer script creado, que se puede acceder estáticamente
        // Así tenemos una sola variable estática de la que consultamos variables
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else {
            instance = this;
        }        
    }

    void Update(){
        deepness += Time.deltaTime * PlayerController.instance.CurrentSpeed;
        txtDeepness.text = deepness.ToString();
    }

    public float Deepness {get { return deepness; } set { deepness = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMineral : MonoBehaviour, IMineable
{
    [Header("Mineral Object Reference")]
    [SerializeField] private BasicMineralObject mineralObject;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = mineralObject.sprite;
    }
    public void Mined()
    {
        Manager.instance.AddMoney(mineralObject.moneyReward);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayItem : MonoBehaviour
{
    public BuyableObject Item;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;
    public Image ItemImage;
    public int Maxlevel = 8;
    public int ActualLevel = 1;
    public float PriceIncreaseIndex;
    public float FuelPriceIncreaseIndex;
    public float EnginePriceIncreaseIndex;
    public float CapacityPriceIncreaseIndex;
    public int Cost;
    public TextMeshProUGUI LevelText;



    public int SelectedItemID;
   
    public int ItemID;
    
    
    public int MaxFuelLevel= 16;
    public static int FuelLevel=1;
    public int MaxEngineLevel=5;
    public static int EngineLevel= 1;
    public int MaxCapacityLevel=32;
    public static int CapacityLevel = 1;

   

    private void Awake()
    {

        switch (ItemID)
        {
            case 1:
                ActualLevel = FuelLevel;
                PriceIncreaseIndex = FuelPriceIncreaseIndex;
                Maxlevel = MaxFuelLevel;
                break;
            case 2:
                ActualLevel = EngineLevel;
                PriceIncreaseIndex = EnginePriceIncreaseIndex;
                Maxlevel = MaxEngineLevel;
                break;
            case 3:
                ActualLevel = CapacityLevel;
                PriceIncreaseIndex = CapacityPriceIncreaseIndex;
                Maxlevel = MaxCapacityLevel;
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {

       
        NameText.text = Item.itemName;
        if(ActualLevel != 1)
        {
            Cost = Mathf.RoundToInt((float)Item.initialCost * ActualLevel * PriceIncreaseIndex);
        }
        else
        {
            Cost = Mathf.RoundToInt((float)Item.initialCost * ActualLevel);
        }
        
        PriceText.text =  Cost.ToString()+"$";
        
        ItemImage.sprite = Item.itemSprite;
        LevelText.text = "Lv." + ActualLevel.ToString();
        Debug.Log(CapacityLevel);



    }
    private void Update()
    {

        Debug.Log(Cost);
        SelectedItemID = StoreManager.SelectedItemID;

        
        
        ItemID = Item.itemID;
        switch (ItemID)
        {
            case 1:
                FuelLevel = ActualLevel;
                PriceIncreaseIndex = FuelPriceIncreaseIndex;
                Maxlevel = MaxFuelLevel;    
                break;
            case 2:
                EngineLevel = ActualLevel;
                PriceIncreaseIndex = EnginePriceIncreaseIndex;
                Maxlevel = MaxEngineLevel;  
                break;
            case 3:
                CapacityLevel = ActualLevel;
                PriceIncreaseIndex = CapacityPriceIncreaseIndex;
                Maxlevel = MaxCapacityLevel;
                break;
           
        }
        if(ActualLevel>= Maxlevel)
        {
            LevelText.text = "MAX";
        }
    }
    public void Upgrade()
    {
        if (SelectedItemID == ItemID)
        {
            if (ActualLevel < Maxlevel)
            {
                ActualLevel++;
                LevelText.text = "Lv." + ActualLevel.ToString();
                Cost = Mathf.RoundToInt((float)Cost * PriceIncreaseIndex);
                PriceText.text = Cost.ToString() + "$";
            }
        }
       

        
    }
  
  
    
    
    
}
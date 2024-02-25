using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonShopController : MonoBehaviour
{
    public GameObject shopPanel;
    public Button buttonShop;
    public Button buttonReturn;
    public GameObject wall;

    private FoodShop foodShop;
    private FoodManager foodManager;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;

        buttonShop.onClick.AddListener(() =>
        {
            shopPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
            for(int i=0; i<foodShop.foodShopList.Count ; i++)
            {
                if(foodShop.foodShopList[i] != null)
                {
                    foodShop.foodShopList[i].GetComponent<FoodShopPrefab>().NumFoodTextUpdate();
                }
            }
            foodManager.CaluculateFoodStockSum();

        });

        buttonReturn.onClick.AddListener(() =>
        {
            shopPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });
    }

    private void Awake()
    {
        foodShop = GameObject.Find("FoodShop").GetComponent<FoodShop>();
        foodManager = GameObject.Find("FoodManager").GetComponent <FoodManager>();
    }
}

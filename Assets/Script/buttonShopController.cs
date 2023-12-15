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

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;

        buttonShop.onClick.AddListener(() =>
        {
            shopPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });

        buttonReturn.onClick.AddListener(() =>
        {
            shopPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });
    }
}

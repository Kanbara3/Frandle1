using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonFoodController : MonoBehaviour
{
    public GameObject foodPanel;
    public Button buttonFood;
    public Button buttonReturn;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;

        buttonFood.onClick.AddListener(() =>
        {
            foodPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });

        buttonReturn.onClick.AddListener(() =>
        {
            foodPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });
    }
}

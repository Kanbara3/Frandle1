using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonToyController : MonoBehaviour
{
    public GameObject toyPanel;
    public Button buttonToy;
    public Button buttonReturn;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;

        buttonToy.onClick.AddListener(() =>
        {
            toyPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });

        buttonReturn.onClick.AddListener(() =>
        {
            toyPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

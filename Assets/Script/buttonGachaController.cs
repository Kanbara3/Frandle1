using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonGachaController : MonoBehaviour
{
    public GameObject GachaPanel;
    public Button buttonGacha;
    public Button buttonReturn;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;

        buttonGacha.onClick.AddListener(() =>
        {
            GachaPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });

        buttonReturn.onClick.AddListener(() =>
        {
            GachaPanel.SetActive(isActive);
            wall.SetActive(isActive);
            isActive = !isActive;
        });
    }
}

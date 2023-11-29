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

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;
        bool invisivle = false;

        rectTransform = toyPanel.GetComponent<RectTransform>();
        

        buttonToy.onClick.AddListener(() =>
        {
            toyPanel.SetActive(isActive);
            wall.SetActive(isActive);
            if (invisivle == false)
            {
                Vector2 pos = rectTransform.position;
                pos.x = 0;
                rectTransform.position = pos;
                invisivle = true;
            }
            
            //isActive = !isActive;
        });

        buttonReturn.onClick.AddListener(() =>
        {
            //toyPanel.SetActive(isActive);
            //wall.SetActive(isActive);
            Vector2 pos = rectTransform.position;
            pos.x = -100;
            rectTransform.position = pos;
            //Debug.Log(pos.x);
            invisivle = false;
            //isActive = !isActive;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

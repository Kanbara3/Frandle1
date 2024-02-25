using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonVisitorController : MonoBehaviour
{
    public GameObject visitorPanel;
    public Button buttonVisitor;
    public Button buttonReturn;
    public GameObject wall;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = true;
        bool invisivle = false;

        rectTransform = visitorPanel.GetComponent<RectTransform>();


        buttonVisitor.onClick.AddListener(() =>
        {
            visitorPanel.SetActive(isActive);
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
            Vector2 pos = rectTransform.position;
            pos.x = -100;
            rectTransform.position = pos;
            invisivle = false;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

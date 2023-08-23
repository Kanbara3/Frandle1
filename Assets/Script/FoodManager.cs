using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public Canvas foodCanvas;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<3; i++)
        {
            int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(-500+p, -300, 0), Quaternion.identity);
            newFood.transform.SetParent(foodCanvas.transform, false);
            newFood.GetComponent<Food>().InitFood("1-"+(i+1), 2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

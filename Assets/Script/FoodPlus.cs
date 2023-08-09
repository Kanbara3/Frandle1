using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodPlus : MonoBehaviour
{
    public TextMeshProUGUI foodtext;
    [SerializeField] int num = 0;
    public Button foodGiveButton;

    // Start is called before the first frame update
    void Start()
    {
        //‚²‚Í‚ñ‚Ì”‚ªŒ¸‚é
        foodGiveButton.onClick.AddListener(() =>
        {
            if (num > 0)
            {
                num -= 1;
                foodtext.text = "x"+ " " + num.ToString();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

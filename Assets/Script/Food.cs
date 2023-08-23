using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    public FrandleManager frandleManager;

    // ごはんの数制御
    private string foodName;
    // ごはんの個数のtext
    public TextMeshProUGUI textFood;
    // ごはんの個数のint
    int numFood = 10;
    // ごはんを与えるButton
    public Button buttonFood;
    // ごはんを与えた時のtapした時の好感度増幅量
    public int tapFood = 1;

    // ボタンを押したときにマイナス1する
    public void GiveButton()
    {
        buttonFood.onClick.AddListener(() =>
        {
            if (numFood > 0)
            {
                numFood -= 1;
                frandleManager.changeOneTapIncreaseRate(tapFood);
                textFood.text = "×" + " " + numFood.ToString();
            }
        });
    }
    /*
    public void InitFood(string imagePath, Food tapFood)
    {
        this.tapFood = tapFood;
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        GiveButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

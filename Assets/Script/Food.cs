using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    public FrandleManager frandleManager;

    // Ç≤ÇÕÇÒÇÃêîêßå‰
    private string foodName;
    // Ç≤ÇÕÇÒÇÃå¬êîÇÃtext
    public TextMeshProUGUI textFood;
    // Ç≤ÇÕÇÒÇÃå¬êîÇÃint
    int numFood = 10;
    // Ç≤ÇÕÇÒÇó^Ç¶ÇÈButton
    public Button buttonFood;
    // Ç≤ÇÕÇÒÇó^Ç¶ÇΩéûÇÃtapÇµÇΩéûÇÃçDä¥ìxëùïùó 
    public int tapFood = 1;

    public void GiveButton()
    {
        buttonFood.onClick.AddListener(() =>
        {
            if (numFood > 0)
            {
                numFood -= 1;
                frandleManager.changeOneTapIncreaseRate(tapFood);
                textFood.text = "Å~" + " " + numFood.ToString();
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

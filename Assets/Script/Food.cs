using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    private FrandleManager frandleManager;

    // ���͂�̐�����
    private string foodName;
    // ���͂�̌���text
    private TextMeshProUGUI foodText;
    // ���͂�̌���int
    int numFood = 0;
    // ���͂��^����Button
    private Button foodButton;
    // ���͂��^��������tap�������̍D���x������
    public long tapFood = 1;

    private Image foodImage;

    // �{�^�����������Ƃ��Ƀ}�C�i�X1����
    public void GiveButton()
    {
        foodButton.onClick.AddListener(() =>
        {
            if (numFood == 0) return;
            numFood -= 1;
            Debug.Log(tapFood);
            frandleManager.changeOneTapIncreaseRate(tapFood);
            foodText.text = "�~" + " " + numFood.ToString();
        });
    }

    
    public void InitFood(string imagePath, long tapFood)
    {
        this.tapFood = tapFood;
        foodImage.sprite = Resources.Load<Sprite>("FoodImage/"+imagePath);
            //GameObject.Find("test").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            //Resources.Load<Sprite>("ImageFoodImage/1-2");
        
    }
    
    public void AddStock(int ct)
    {
        numFood += ct;
        foodText.text = "�~" + " " + numFood.ToString();
    }

    // Start is called before the first frame update
    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        foodButton = this.transform.GetChild(0).GetComponent<Button>();
        foodText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        GiveButton();
        foodImage = this.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

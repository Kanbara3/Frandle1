using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    public FrandleManager frandleManager;

    // ���͂�̐�����
    private string foodName;
    // ���͂�̌���text
    public TextMeshProUGUI textFood;
    // ���͂�̌���int
    int numFood = 10;
    // ���͂��^����Button
    public Button buttonFood;
    // ���͂��^��������tap�������̍D���x������
    public int tapFood = 1;

    // �{�^�����������Ƃ��Ƀ}�C�i�X1����
    public void GiveButton()
    {
        buttonFood.onClick.AddListener(() =>
        {
            if (numFood > 0)
            {
                numFood -= 1;
                frandleManager.changeOneTapIncreaseRate(tapFood);
                textFood.text = "�~" + " " + numFood.ToString();
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

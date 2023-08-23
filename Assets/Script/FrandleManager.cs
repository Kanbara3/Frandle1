using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrandleManager : MonoBehaviour
{
    public int tap;
    public GameObject harttext;
    public GameObject kankeitext;
    int oneTapIncrease = 1;

    // Start is called before the first frame update
    void Start()
    {
        tap = 0;
        //GiveButton();
    }

    // Update is called once per frame
    void Update()
    {
        KankeiDirector();
        
    }

    // �D���x(tap)�̍X�V
    public void changeOneTapIncreaseRate(int upRate)
    {
        oneTapIncrease += upRate;
    }
    
    public void HartDirector()
    {
        tap += oneTapIncrease;
        this.harttext.GetComponent<TextMeshProUGUI>().text = tap.ToString("F0");
    }

    
    // �D���x�ɂ���Ċ֌W�e�L�X�g���ω�����
    public void KankeiDirector()
    {
        if (tap < 1000) // 1
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�E�E�E�H";
        }
        else if (tap < 2000) // 2
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��邪�u���Ă���������";
        }
        else if (tap < 5000) // 3
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��������H";
        }
        else if (tap < 10000) // 4
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��������Ȃ̂ɂ悭����������";
        }
        else if (tap < 20000) // 5
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���킢���Ȃ���������󂵂��Ⴄ����";
        }
        else if (tap < 50000) // 6
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁH";
        }
        else if (tap < 100000) // 7
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���������Ƃ���l��";
        }
        else if (tap < 300000) // 8
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�H�ׂĂ����l�Ԃ���";
        }
        else if (tap < 600000) // 9
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���ɐH�ׂ���_���Ȃ������Ⴞ���Č���ꂽ";
        }
        else if (tap < 1000000) // 10
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�����ɓ������l�Ԃ͂��Ȃ��ŎO�l�ڂ�";
        }
        else if (tap < 3000000) // 11 
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�V��ł�Ԃɉ�ꂿ����Ă��d���Ȃ����";
        }
        else if (tap < 6000000) // 12
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�Ȃɂ��ėV�ԁH";
        }
        else if (tap < 10000000) // 13
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁI����ǂ�ŁI";
        }
        else if (tap < 30000000) // 14
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁ`���l�`��ꂿ�����";
        }
        else if (tap < 60000000) // 15
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�������[����";
        }
        else if (tap < 100000000) // 16
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���g�̐l�Ԃ���z�������Ăǂ�Ȗ�����";
        }
        else if (tap < 300000000) // 17
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁc����Ȃ��Ă��Ɂ[����H";
        }
        else if (tap < 500000000) // 18
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���Ɂ[����V��ŁI";
        }
        else if (tap < 800000000) // 19
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���o�l���Ă��Ɂ[����ƈ���Ă��������";
        }
        else if (tap < 1000000000) // 20
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�˂��A�z���Ă����ł���H";
        }
        else if (tap < 2000000000) // 20
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���[���Ǝ��̂�������ˁI";
        }
        else // 21
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���[���Ǝ��̂�������ˁI";
        }
    }

    /*
    // ���͂�̐�����
    // ���͂�̌���text
    public TextMeshProUGUI text_food1_1;
    public TextMeshProUGUI text_food1_2;
    public TextMeshProUGUI text_food1_3;
    public TextMeshProUGUI text_food1_4;
    // ���͂�̌���int
    int num_food1_1 = 10;
    int num_food1_2 = 10;
    int num_food1_3 = 10;
    int num_food1_4 = 10;
    // ���͂��^����Button
    public Button button_food1_1;
    public Button button_food1_2;
    public Button button_food1_3;
    public Button button_food1_4;
    // ���͂��^��������tap�������̍D���x������
    private int tap_food1_1 = 1;
    private int tap_food1_2 = 2;
    private int tap_food1_3 = 3;
    private int tap_food1_4 = 4;
    public void GiveButton()
    {
        button_food1_1.onClick.AddListener(() =>
        {
            if (num_food1_1 > 0)
            {
                num_food1_1 -= 1;
                one_tap += tap_food1_1;
                text_food1_1.text = "�~" + " " + num_food1_1.ToString();
            }
        });
        button_food1_2.onClick.AddListener(() =>
        {
            if (num_food1_2 > 0)
            {
                num_food1_2 -= 1;
                one_tap += tap_food1_2;
                text_food1_2.text = "�~" + " " + num_food1_2.ToString();
            }
        });
        button_food1_3.onClick.AddListener(() =>
        {
            if (num_food1_3 > 0)
            {
                num_food1_3 -= 1;
                one_tap += tap_food1_3;
                text_food1_3.text = "�~" + " " + num_food1_3.ToString();
            }
        });
        button_food1_4.onClick.AddListener(() =>
        {
            if (num_food1_4 > 0)
            {
                num_food1_4 -= 1;
                one_tap += tap_food1_4;
                text_food1_4.text = "�~" + " " + num_food1_4.ToString();
            }
        });
    }
    */
}
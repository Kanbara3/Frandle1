using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Visitor : MonoBehaviour
{
    private FrandleManager frandleManager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    private Image visitorImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Asset>Resources>CharacterImage�t�H���_����摜��ǂݍ���
    public void InitVisitor(string imagePath, string visitorName)
    {
        visitorImage.sprite = Resources.Load<Sprite>("VisitorImage/" + imagePath);
        nameText.text = visitorName;

    }

    void Awake()
    {
        visitorImage = this.transform.GetChild(0).GetComponent<Image>();
        nameText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        levelText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}

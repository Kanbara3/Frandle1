using System.Security.Claims;
using UnityEngine;

public class GachaAnimationManager : MonoBehaviour
{
    //private Animator doorAnime;
    //private Animator whitePanelAnime;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject whitePanel;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject GachaAnimation;
    [SerializeField] private GameObject gachaAudioManager;

    private DoorAnimation doorAnimation;
    private WhitePanelAnimation whitePanelAnimation;
    private BackgroundAnimation backgroundAnimation;
    private GachaAudio gachaAudio;

    private bool gachaFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //doorAnime = door.GetComponent<Animator>();
        //whitePanelAnime = whitePanel.GetComponent<Animator>();
        doorAnimation = door.GetComponent<DoorAnimation>();
        whitePanelAnimation = whitePanel.GetComponent<WhitePanelAnimation>();
        backgroundAnimation = background.GetComponent<BackgroundAnimation>();
        gachaAudio = gachaAudioManager.GetComponent<GachaAudio>();
        GachaAnimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GachaAnimation.SetActive(true);
            doorAnimation.PlayDoorAnimation();
            whitePanelAnimation.PlayWhitePanelAnimation();
            backgroundAnimation.PlayBackgroundlAnimation();
        }
    }

    public void PlayGachaAnimation()
    {
        GachaAnimation.SetActive(true);
        doorAnimation.PlayDoorAnimation();
        whitePanelAnimation.PlayWhitePanelAnimation();
        backgroundAnimation.PlayBackgroundlAnimation();
        gachaAudio.PlayAudio();
        gachaFlag = true;
    }

    public void StopGachaAnimation()
    {
        doorAnimation.StopDoorAnimation();
        whitePanelAnimation.StopWhitePanelAnimation();
        backgroundAnimation.StopBackgroundAnimation();
        GachaAnimation.SetActive(false);
    }

    // ガチャ画面を閉じるコード
    public void CloseGachaAnimationObject()
    {
        if (gachaFlag)
        {
            StopGachaAnimation();
            gachaFlag=false;
        }
    }
}

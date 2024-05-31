using UnityEngine;

public class WhitePanelAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void PlayWhitePanelAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolFade", true);
    }

    public void StopWhitePanelAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolFade", false);
    }
}

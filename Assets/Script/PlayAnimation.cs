using UnityEngine;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;
    public string singleTurnAnimationName;
    public string tenTurnAnimationName;

    private Button turnButton;
    private Button turnButton10;

    private void Start()
    {
        turnButton.onClick.AddListener(PlaySingleTurnAnimation);
        turnButton10.onClick.AddListener(PlayTenTurnAnimation);
    }

    void PlaySingleTurnAnimation()
    {
        animator.Play(singleTurnAnimationName);
    }

    void PlayTenTurnAnimation()
    {
        animator.Play(tenTurnAnimationName);
    }
}

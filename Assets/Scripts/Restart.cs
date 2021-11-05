using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class Restart : MonoBehaviour
{
    [SerializeField] Image cover;
    [SerializeField] Button restartButton;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image loadScreen;
    [SerializeField] UnityEvent onLoading, onRestarted;

    Tweener
        restartScreenFadeIn,
        restartScreenFadeOut,
        loadScreenFadeIn,
        loadScreenFadeOut;
    Sequence
        restartSequence;

    public void Awake()
    {
        SetCanvasGroupActive(false);

        restartScreenFadeIn =
            canvasGroup.DOFade(1f, .3f)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(() =>
            {
                SetCanvasGroupActive(true);
            });
        restartScreenFadeOut =
            canvasGroup.DOFade(0f, .3f)
            .SetAutoKill(false)
            .Pause() ;


        loadScreenFadeIn =
            loadScreen.DOFade(1f, .3f)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(() =>
            {
                onLoading.Invoke();
            });
        loadScreenFadeOut =
            loadScreen.DOFade(0f, .2f)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(() =>
            {
                onRestarted.Invoke();
            });


        restartSequence =
            DOTween.Sequence()
            .Append(loadScreenFadeIn)
            .Append(restartScreenFadeOut)
            .Append(loadScreenFadeOut)
            .SetAutoKill(false)
            .Pause()
            ;

    }

    public void RestartGame()
    {
        SetCanvasGroupActive(false);

        if (restartSequence.playedOnce) restartSequence.Rewind();

        restartSequence.Play();
    }

    public void SetCanvasGroupActive(bool isActive)
    {
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    public void FadeInRestartScreen()
    {
        SetCanvasGroupActive(true);

        if (restartScreenFadeIn.playedOnce) restartScreenFadeIn.Rewind();
        restartScreenFadeIn.Play();
    }
}

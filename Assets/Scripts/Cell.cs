using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] AnswerChecker answerChecker;
    [SerializeField] SpriteRenderer background, content;
    [SerializeField] AnswerEvent onAnswer;

    bool isClickable = true;

    string answer;

    Tweener
        wrongAnswerTween,
        appearTween;

    public void Awake()
    {
        appearTween =
            transform.DOScale(1f, .5f)
            .SetEase(Ease.OutBounce)
            .SetAutoKill(false)
            .Pause();

        wrongAnswerTween =
            content.transform.DOMoveX(1, .25f)
            .SetEase(Ease.InBounce)
            .SetLoops(2, LoopType.Yoyo)
            .SetRelative(true)
            .SetAutoKill(false)
            .Pause();
    }

    public void Setup(string answer, Sprite sprite)
    {
        this.answer = answer;
        content.sprite = sprite;
        isClickable = true;
    }

    public string GetAnswer() => answer;

    public SpriteRenderer GetContent() => content;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isClickable) return;

        onAnswer.Invoke(this);
    }


    public void PlayWrongAnswerTween()
    {
        wrongAnswerTween.Rewind();
        wrongAnswerTween.Play();
    }

    public void PlayAppearTween()
    {
        transform.localScale = Vector3.zero;
        appearTween.Rewind();
        appearTween.Play();
    }

    public void SetUnclickable()
    {
        isClickable = false;
    }
}

[System.SerializableAttribute]
public class AnswerEvent : UnityEvent<Cell> {}

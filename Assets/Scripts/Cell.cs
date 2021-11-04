using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


[System.SerializableAttribute]
public class AnswerEvent : UnityEvent<Cell> {}

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] AnswerChecker answerChecker;
    [SerializeField] SpriteRenderer background, content;
    [SerializeField] AnswerEvent onAnswer;

    Question answer;

    Tweener
        wrongAnswerTween,
        correctAnswerTween;

    public void Awake()
    {
        wrongAnswerTween =
            content.transform.DOMoveX(1, .25f)
            .SetEase(Ease.InBounce)
            .SetLoops(2, LoopType.Yoyo)
            .SetRelative(true)
            .SetAutoKill(false)
            .Pause();

        wrongAnswerTween.onComplete = () => wrongAnswerTween.Rewind();


        correctAnswerTween =
            content.transform.DOMoveY(1, .5f)
            .SetEase(Ease.OutBounce)
            .SetLoops(2, LoopType.Yoyo)
            .SetRelative(true)
            .SetAutoKill(false)
            .Pause();

        correctAnswerTween.onComplete = () => correctAnswerTween.Rewind();
    }

    public void SetAnswer(Question answer)
    {
        this.answer = answer;
        content.sprite = answer.sprite;
    }
    public Question GetAnswer()
    {
        return answer;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onAnswer.Invoke(this);
    }

    public void OnCorrectAnswer()
    {
        correctAnswerTween.Play();
    }

    public void OnWrongAnswer()
    {
        wrongAnswerTween.Play();
    }
}

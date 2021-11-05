using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] AnswerCheckerView view;
    [SerializeField] ParticleSystem starSystem;
    [SerializeField] UnityEvent onReadyToSwitchLevels;

    string correctAnswer;

    Tweener correctAnswerTween;

    public void SetCorrectAnswer(string correctAnswer)
    {
        this.correctAnswer = correctAnswer;

        view.SetQuestionText(correctAnswer);
    }

    public void CheckAnswer(Cell cell)
    {
        if (cell.GetAnswer() == correctAnswer)
        {
            if (!starSystem.isPlaying)
                PlayStarSystem(cell.transform.position);

            if (correctAnswerTween == null)
            {
                correctAnswerTween = PlayCorrectAnswerTween(cell.GetContent());

                StartCoroutine(WaitForCorrectAnswerTweenCompletion());
            }
        }
        else
        {
            cell.PlayWrongAnswerTween();
        }
    }

    void PlayStarSystem(Vector3 position)
    {
        starSystem.transform.position = position;
        starSystem.Play();;
    }

    Tweener PlayCorrectAnswerTween(SpriteRenderer cellContent)
    {
        return
            cellContent.transform.DOMoveY(1, .5f)
            .SetEase(Ease.OutBounce)
            .SetLoops(2, LoopType.Yoyo)
            .SetRelative(true)
            .SetAutoKill(false);
    }

    IEnumerator WaitForCorrectAnswerTweenCompletion()
    {
        yield return correctAnswerTween.WaitForCompletion();

        correctAnswerTween.Kill();;
        correctAnswerTween = null;

        onReadyToSwitchLevels.Invoke();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] AnswerCheckerView view;
    [SerializeField] ParticleSystem starSystem;
    [SerializeField] UnityEvent onCorrectAnswer;

    Question correctAnswer;


    public void SetCorrectAnswer(Question correctAnswer)
    {
        this.correctAnswer = correctAnswer;

        view.SetQuestionText(correctAnswer.name);
    }

    public void CheckAnswer(Cell cell)
    {
        if (cell.GetAnswer() == correctAnswer)
        {
            cell.OnCorrectAnswer();
            PlayStarSystem(cell.transform.position);

            onCorrectAnswer.Invoke();
        }
        else
        {
            cell.OnWrongAnswer();
        }
    }

    void PlayStarSystem(Vector3 position)
    {
        starSystem.transform.position = position;
        starSystem.Play();;
    }
}

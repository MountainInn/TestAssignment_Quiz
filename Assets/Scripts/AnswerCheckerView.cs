using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

public class AnswerCheckerView : MonoBehaviour
{
    [SerializeField] Text question;

    Tweener fadeIn;

    void Awake()
    {
        HideText();
       
        fadeIn =
            question.DOFade(1f, 1f)
            .SetAutoKill(false)
            .Pause();
    }

    public void SetQuestionText(string answerString)
    {
        question.text = "Find " + answerString;
    }


    public void HideText()
    {
        question.color = question.color.SetA(0);
    }

    public void FadeInText()
    {
        if (fadeIn.playedOnce) fadeIn.Rewind();
        fadeIn.Play();
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AnswerCheckerView : MonoBehaviour
{
    [SerializeField] Text question;

    public void SetQuestionText(string answerString)
    {
        question.text = "Find " + answerString;
    }
}

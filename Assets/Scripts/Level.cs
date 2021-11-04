using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] LevelConfig[] levelConfigs;
    [SerializeField] LevelGrid grid;
    [SerializeField] AnswerChecker answerChecker;
    [SerializeField] UnityEvent onAllLevelsCompleted;

    List<Question> selectedQuestions;

    int currentLevel;


    public void Awake()
    {
        selectedQuestions = new List<Question>();

        if (levelConfigs.Length == 0) Debug.LogError("Level has no configuration");
        SetConfig(levelConfigs[currentLevel]);
    }

    public void SetConfig(LevelConfig config)
    {
        RandomizeQuestions(config);
        grid.SetConfig(config.gridConfig);
        grid.SetupCells(selectedQuestions);
    }

    private void RandomizeQuestions(LevelConfig config)
    {
        selectedQuestions.Clear();

        var possibleQuestions = new List<Question>(config.questionData.questions);


        for (int i = 0; i < config.gridConfig.GetCellCount(); i++)
        {
            selectedQuestions.Add(possibleQuestions.ExtractRandom());
        }

        var correctAnswer = selectedQuestions.GetRandom();
        answerChecker.SetCorrectAnswer(correctAnswer);
    }


    public void NextLevel()
    {
        if (++currentLevel < levelConfigs.Length)
        {
            SetConfig(levelConfigs[currentLevel]);
        }
        else
        {
            onAllLevelsCompleted.Invoke();
        }
    }

}

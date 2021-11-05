using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class Level : MonoBehaviour
{
    [SerializeField] LevelConfig[] levelConfigs;
    [SerializeField] LevelGrid grid;
    [SerializeField] AnswerChecker answerChecker;
    [SerializeField] UnityEvent onSceneLoaded, onAllLevelsCompleted;

    Dictionary<QuestionData, HashSet<int>> unusedAnswers;

    int currentLevel = 0;

    public void Awake()
    {
        unusedAnswers = new Dictionary<QuestionData, HashSet<int>>();

        if (levelConfigs.Length == 0) Debug.LogError("Level has no configuration");

        SetConfig(levelConfigs[currentLevel]);
    }

    public void Start()
    {
        onSceneLoaded.Invoke();;
    }

    public void RestartFromFirstLevel()
    {
        currentLevel = 0;
        SetConfig(levelConfigs[currentLevel]);

        onSceneLoaded.Invoke(); ;
    }

    public void SetConfig(LevelConfig config)
    {
        var answers = config.questionData.answers;
        var sprites = config.questionData.sprites;

        var selectedIDs = RandomizeQuestions(config.questionData, config.gridConfig.GetCellCount(), out int correctAnswer);

        answerChecker.SetCorrectAnswer(answers[correctAnswer]);

        grid.SetConfig(config.gridConfig);

        grid.SetupCells(answers.SelectIndexes(selectedIDs), sprites.SelectIndexes(selectedIDs));
    }

    private List<int> RandomizeQuestions(QuestionData questionData, int cellCount, out int correctAnswer)
    {
        if (!unusedAnswers.ContainsKey(questionData))
            unusedAnswers[questionData] = new HashSet<int>(Enumerable.Range(0, questionData.answers.Count));

        var answers = unusedAnswers[questionData];

        if (answers.Count == 0)
        {
            Debug.LogWarning("No unused answers left for " + questionData.name);

            correctAnswer = -1;

            return null;
        }

        List<int>
            indexes = new List<int>(Enumerable.Range(0, questionData.answers.Count)),
            selected = new List<int>();


        for (int i = 0; i < cellCount; i++)
        {
            selected.Add(indexes.ExtractRandom());
        }


        List<int> possibleAnswers = answers.Intersect(selected).ToList();


        if (possibleAnswers.Count == 0)
        {
            selected.ExtractRandom();

            correctAnswer = answers.ToList().ExtractRandom();

            selected.Add(correctAnswer);
        }
        else
        {
            correctAnswer = possibleAnswers.GetRandom();
        }


        answers.Remove(correctAnswer);

        return selected;
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

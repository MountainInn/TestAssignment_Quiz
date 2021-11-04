using UnityEngine;
using UnityEngine.UIElements;
using System;

[CreateAssetMenuAttribute(fileName = "New LevelConfig", menuName = "Level Config", order = 3)]
public class LevelConfig : ScriptableObject
{
    public GridConfig gridConfig;
    public QuestionData questionData;
}

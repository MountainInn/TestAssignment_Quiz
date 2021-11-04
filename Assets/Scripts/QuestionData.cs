using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenuAttribute(fileName = "New QuestionData", menuName = "Question Data", order = 1)]
public class QuestionData : ScriptableObject
{
    [SerializeReference] public List<Sprite> sprites;
    [SerializeReference] public List<string> answers;
    [SerializeReference] public List<Question> questions;
}

[SerializableAttribute]
public class Question
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string name;
}

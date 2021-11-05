using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenuAttribute(fileName = "New QuestionData", menuName = "Question Data", order = 1)]
public class QuestionData : ScriptableObject
{
    [SerializeReference] public List<Sprite> sprites;
    [SerializeReference] public List<string> answers;
}

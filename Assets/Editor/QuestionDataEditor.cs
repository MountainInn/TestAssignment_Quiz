using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

[CustomEditor(typeof(QuestionData))]
[CanEditMultipleObjects]
public class QuestionDataEditor : Editor
{
    SerializedProperty spriteList, answerList,
        questionList;


    void OnEnable()
    {
        spriteList = serializedObject.FindProperty("sprites");
        answerList = serializedObject.FindProperty("answers");
       
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();;

        EditorGUILayout.PropertyField(spriteList);

        answerList.arraySize = spriteList.arraySize;

        var answerEnumerator = answerList.GetEnumerator();
        var spriteEnumerator = spriteList.GetEnumerator();


        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.BeginVertical();
        for (int i = 0; i < spriteList.arraySize; i++)
        {
            spriteEnumerator.MoveNext();

            var sprite = spriteEnumerator.Current as SerializedProperty;

            sprite.objectReferenceValue = EditorGUILayout.ObjectField("", sprite.objectReferenceValue, typeof(Sprite), false);
        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical();
        for (int i = 0; i < answerList.arraySize; i++)
        {
            answerEnumerator.MoveNext();

            var answer = answerEnumerator.Current as SerializedProperty;

            answer.stringValue = EditorGUILayout.TextField(answer.stringValue);

            EditorGUILayout.Space(45);
        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.EndHorizontal();



        serializedObject.ApplyModifiedProperties();
    }
}

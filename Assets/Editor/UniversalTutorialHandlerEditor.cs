using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UniversalTutorialHandler))] 
public class UniversalTutorialHandlerEditor : Editor
{
    //public override void OnInspectorGUI() 
    //{
    //    // Получаем целевой объект
    //    UniversalTutorialHandler handler = (UniversalTutorialHandler)target;

    //    // Отрисовываем стандартные поля
    //    serializedObject.Update();

    //    // Trigger Settings
    //    EditorGUILayout.LabelField("Trigger Settings", EditorStyles.boldLabel);
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("tutorialId"));
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerType"));
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("priority"));

    //    if (handler.Trigger == UniversalTutorialHandler.TriggerType.OnEvent)
    //    {
    //        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventName"));
    //    }

    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("oneTimeOnly"));

    //    // Action Settings
    //    EditorGUILayout.LabelField("Action Settings", EditorStyles.boldLabel);
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("actionType"));
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("uiObject"));

    //    if (handler.Action == UniversalTutorialHandler.ActionType.MoveObject ||
    //        handler.Action == UniversalTutorialHandler.ActionType.ShowUIAndMoveObject)
    //    {
    //        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetObject"));
    //        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetPosition"));
    //    }

    //    // Timing Settings
    //    EditorGUILayout.LabelField("Timing Settings", EditorStyles.boldLabel);
    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("delayBeforeAction"));

    //    serializedObject.ApplyModifiedProperties();
    //}
}

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InputEvent), true)]
[CustomPropertyDrawer(typeof(InputEvent<>), true)]
public class InputEventDrawer : PropertyDrawer
{
    private Color LINE_COLOR = new Color(.14f, .14f, .14f);

    private SerializedProperty inputProp;
    private SerializedProperty eventStartProp;
    private SerializedProperty eventProp;
    private SerializedProperty eventCancelProp;

    private bool isFoldoutOpen;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (inputProp == null) FindProperties(property);

        float labelWidth = EditorGUIUtility.labelWidth + 2;
        Rect foldoutWithoutField = GUILayoutUtility.GetRect(label, EditorStyles.foldoutHeader);

        var foldoutRect = new Rect(
            foldoutWithoutField.x,
            foldoutWithoutField.y,
            labelWidth,
            foldoutWithoutField.height);

        var inputActionRect = new Rect(
            foldoutWithoutField.x + labelWidth,
            foldoutWithoutField.y,
            foldoutWithoutField.width - labelWidth,
            foldoutWithoutField.height);


        isFoldoutOpen = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, isFoldoutOpen, label);

        EditorGUI.PropertyField(inputActionRect, inputProp, GUIContent.none);

        EditorGUILayout.EndFoldoutHeaderGroup();

        if (isFoldoutOpen)
        {
            Rect lineRect = GUILayoutUtility.GetLastRect();
            lineRect = new Rect(
                lineRect.x - 10,
                lineRect.y + 10,
                1f,
                EditorGUI.GetPropertyHeight(eventProp) * 3f + 15);

            EditorGUI.DrawRect(lineRect, LINE_COLOR);

            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(eventStartProp);
            EditorGUILayout.PropertyField(eventProp);
            EditorGUILayout.PropertyField(eventCancelProp);
            EditorGUILayout.Space(5);
        }
    }

    private void FindProperties(SerializedProperty property)
    {
        inputProp = property.FindPropertyRelative("inputAction");
        eventStartProp = property.FindPropertyRelative("onInputStarted");
        eventProp = property.FindPropertyRelative("onInput");
        eventCancelProp = property.FindPropertyRelative("onInputCanceled");
    }
}

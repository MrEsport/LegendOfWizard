using UnityEditor;
using UnityEngine;
using InputEventValue = InputEventsManagerSettings.InputEventValue;

[CustomPropertyDrawer(typeof(InputEventValue), true)]
public class InputEventValueDrawer : PropertyDrawer
{
    private SerializedProperty nameProp;
    private SerializedProperty typeProp;
    private SerializedProperty controlProp;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(nameProp, GUIContent.none);
        EditorGUILayout.Space(15);
        EditorGUILayout.PropertyField(typeProp, GUIContent.none);
        EditorGUILayout.Space(15);
        EditorGUILayout.PropertyField(controlProp, GUIContent.none);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private void FindProperties(SerializedProperty property)
    {
        nameProp = property.FindPropertyRelative("Name");
        typeProp = property.FindPropertyRelative("Type");
        controlProp = property.FindPropertyRelative("Control");
    }
}

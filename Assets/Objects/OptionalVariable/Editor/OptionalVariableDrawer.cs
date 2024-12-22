using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Optional<>), true)]
public class OptionalVariableDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var valueProperty = property.FindPropertyRelative("m_value");
        return EditorGUI.GetPropertyHeight(valueProperty, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var valueProperty = property.FindPropertyRelative("m_value");
        var enabledProperty = property.FindPropertyRelative("m_enabled");
        float enabledSize = EditorGUI.GetPropertyHeight(enabledProperty);

        EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);

        position.width -= enabledSize;
        EditorGUI.PropertyField(position, valueProperty, label, true);

        EditorGUI.EndDisabledGroup();

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        
        position.x += position.width + 6;
        position.width = enabledSize;
        position.height = enabledSize;
        EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);

        EditorGUI.indentLevel = indent;
    }
}

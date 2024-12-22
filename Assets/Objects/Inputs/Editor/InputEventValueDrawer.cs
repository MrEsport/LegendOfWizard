using UnityEditor;
using UnityEngine;
using InputEventValue = InputEventsManagerSettings.InputEventValue;

[CustomPropertyDrawer(typeof(InputEventValue), true)]
public class InputEventValueDrawer : PropertyDrawer
{
    class LabelStyle
    {
        public static GUIContent Name = new GUIContent("Name");
        public static GUIContent Type = new GUIContent("Input Type");
        public static GUIContent Control = new GUIContent("Output");
    }

    private SerializedProperty nameProp;
    private SerializedProperty typeProp;
    private SerializedProperty controlProp;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        Rect baseRect = position;
        baseRect.width /= 3f;
        baseRect.width -= 4;

        EditorGUI.PropertyField(baseRect, nameProp, GUIContent.none);
        baseRect.x += baseRect.width + 4;

        EditorGUI.PropertyField(baseRect, typeProp, GUIContent.none);
        baseRect.x += baseRect.width + 4;

        EditorGUI.PropertyField(baseRect, controlProp, GUIContent.none);
    }

    private void FindProperties(SerializedProperty property)
    {
        nameProp = property.FindPropertyRelative("Name");
        typeProp = property.FindPropertyRelative("Type");
        controlProp = property.FindPropertyRelative("Control");
    }
}

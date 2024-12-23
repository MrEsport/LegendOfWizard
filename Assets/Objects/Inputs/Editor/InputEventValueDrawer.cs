using UnityEditor;
using UnityEngine;
using InputEventValue = InputEventsManagerSettings.InputEventValue;

[CustomPropertyDrawer(typeof(InputEventValue), true)]
public class InputEventValueDrawer : PropertyDrawer
{
    class DrawStyle
    {
        public static Color LineColor = new Color(.14f, .14f, .14f);
        public static GUIContent ActionRefLabel = new GUIContent("Input Ref.");
        public static GUIContent EventLabel = new GUIContent("Event");
    }

    private SerializedProperty nameProp;
    private SerializedProperty typeProp;
    private SerializedProperty controlProp;
    private SerializedProperty actionRefProp;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 7f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FindProperties(property);

        // Top Line : InputActionReference Asset
        Rect actionRect = new Rect(position.x + position.width * .1f, position.y + 3f, position.width * .9f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(actionRect, actionRefProp, DrawStyle.ActionRefLabel);

        // Bottom Line : InputEvent Values
        Rect baseRect = new Rect(position.x, actionRect.y + actionRect.height + 2f, position.width * .3f - 4f, actionRect.height);
        Rect labelRect = new Rect(baseRect.x, baseRect.y, position.width * .1f, baseRect.height);
        EditorGUI.LabelField(labelRect, DrawStyle.EventLabel);
        baseRect.x += labelRect.width + 4;

        EditorGUI.PropertyField(baseRect, nameProp, GUIContent.none);
        baseRect.x += baseRect.width + 4;

        EditorGUI.PropertyField(baseRect, typeProp, GUIContent.none);
        baseRect.x += baseRect.width + 4;

        EditorGUI.PropertyField(baseRect, controlProp, GUIContent.none);

        // SeperativeLines
        Rect lineRect = new Rect(position.x, position.y, position.width, 1f);
        EditorGUI.DrawRect(lineRect, DrawStyle.LineColor);
        lineRect.y += position.height;
        EditorGUI.DrawRect(lineRect, DrawStyle.LineColor);
    }

    private void FindProperties(SerializedProperty property)
    {
        nameProp = property.FindPropertyRelative("Name");
        typeProp = property.FindPropertyRelative("Type");
        controlProp = property.FindPropertyRelative("Control");
        actionRefProp = property.FindPropertyRelative("ActionRef");
    }
}

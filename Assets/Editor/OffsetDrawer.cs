/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CyberAslan.AdaptiveGrid
{
    [CustomPropertyDrawer(typeof(Offset))]
    public class OffsetDrawerUIE : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            // Create property container element.
            var container = new VisualElement();

            // Create property fields.
            var leftField = new PropertyField(property.FindPropertyRelative("Left"));
            var topField = new PropertyField(property.FindPropertyRelative("Top"));
            var rightField = new PropertyField(property.FindPropertyRelative("Right"));
            var bottomField = new PropertyField(property.FindPropertyRelative("Bottom"));

            // Add fields to the container.
            container.Add(leftField);
            container.Add(topField);
            container.Add(rightField);
            container.Add(bottomField);
            return container;
        }
    }

    [CustomPropertyDrawer(typeof(Offset))]
    public class OffsetDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.

            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var leftFieldPrefix = new Rect(position.x, position.y, 12, position.height);
            var leftField = new Rect(position.x + 12, position.y, 31, position.height);
            var topFieldPrefix = new Rect(position.x + 45, position.y, 12, position.height);
            var topField = new Rect(position.x + 57, position.y, 31, position.height);
            var rightFieldPrefix = new Rect(position.x + 90, position.y, 12, position.height);
            var rightField = new Rect(position.x + 102, position.y, 31, position.height);
            var bottomFieldPrefix = new Rect(position.x + 135, position.y, 12, position.height);
            var bottomField = new Rect(position.x + 147, position.y, 31, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.LabelField(leftFieldPrefix, "L");
            EditorGUI.PropertyField(leftField, property.FindPropertyRelative("Left"), GUIContent.none);
            EditorGUI.LabelField(topFieldPrefix, "T");
            EditorGUI.PropertyField(topField, property.FindPropertyRelative("Top"), GUIContent.none);
            EditorGUI.LabelField(rightFieldPrefix, "R");
            EditorGUI.PropertyField(rightField, property.FindPropertyRelative("Right"), GUIContent.none);
            EditorGUI.LabelField(bottomFieldPrefix, "B");
            EditorGUI.PropertyField(bottomField, property.FindPropertyRelative("Bottom"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}*/
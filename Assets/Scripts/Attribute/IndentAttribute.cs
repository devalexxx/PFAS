using UnityEngine;
using UnityEditor;
using PFAS.Attribute;

namespace PFAS.Attribute
{
    public class IndentAttribute : PropertyAttribute
    {
        public int IndentLevel;
        public IndentAttribute(int indentLevel)
        {
            IndentLevel = indentLevel;
        }

        public IndentAttribute()
        {
            IndentLevel = 1;
        }
    }
}

#if UNITY_EDITOR
namespace PFAS.Internal
{
    [CustomPropertyDrawer(typeof(IndentAttribute))]
    public class IndentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            IndentAttribute indentAttribute = (IndentAttribute)attribute;
            int previousIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel += indentAttribute.IndentLevel;

            EditorGUI.PropertyField(position, property, label);

            EditorGUI.indentLevel = previousIndent;
        }
    }
}
#endif
using UnityEditor;
using UnityEngine;

namespace SimpleVFXs
{
    public static class CustomEditorHelper
    {
        public static readonly string HighlightColor = "#AAEEFF";

        public static void drawInfoPanel(string message)
        {
            GUIStyle style = new(GUI.skin.textField);
            style.margin = new RectOffset(5, 5, 15, 15);
            style.richText = true;
            style.wordWrap = true;

            GUIStyle iconStyle = new(GUI.skin.box);
            iconStyle.margin = new RectOffset(5, 5, 15, 15);

            GUILayout.BeginHorizontal();
            GUILayout.Box(EditorGUIUtility.IconContent("_Help"), iconStyle);
            GUILayout.TextArea(message, style);
            GUILayout.EndHorizontal();
        }
    }
}
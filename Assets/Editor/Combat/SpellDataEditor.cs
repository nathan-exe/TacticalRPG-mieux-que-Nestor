using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(SpellData))]
public class SpellDataEditor : Editor
{
    Rect _canvas;
    float _tileSize;

    public override void OnInspectorGUI()
    {
        SpellData Target = (SpellData)target;

        base.OnInspectorGUI();

        GUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        GUILayout.Space(5);

        if (GUILayout.Button("Edit Area of Effect"))
        {
            SpellEditionWindow window = EditorWindow.GetWindow<SpellEditionWindow>();
            window.SetUp(Target);
            window.Show();
        }
        
    }


}

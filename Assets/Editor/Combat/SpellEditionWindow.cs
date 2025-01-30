using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellEditionWindow : EditorWindow
{
    public SpellData Spell;

    Rect _canvas;
    float _tileSize;

    int _selectedTile;
    void OnGUI()
    {

        EditorGUILayout.LabelField("Canvas");

        UpdateCanvas();
        HandleInputs();
        DrawCanvas();

        EditorGUILayout.Separator();
        if (GUILayout.Button("Clear")) Spell.AffectedTiles.Clear();
    }



    void HandleInputs()
    {

        if (Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
        {
            if (_canvas.Contains(Event.current.mousePosition))
            {
                // Debug.Log(canvas.min);
                Vector2Int c = ((Vector2)Unity.Mathematics.math.remap(_canvas.min, _canvas.max, (Vector2)Spell.Bounds.min, (Vector2)Spell.Bounds.max, Event.current.mousePosition)).Floor();
                

                if(!Spell.AffectedTiles.Contains(new Vector2Int(c.x, c.y)))
                {
                    Spell.AffectedTiles.Add(new Vector2Int(c.x, c.y));
                    EditorUtility.SetDirty(Spell);
                    Event.current.Use();
                }
               
            }
        }
    }

    void UpdateCanvas()
    {

        //compute white rect
        float minSize;
        float ratio = (float)Spell.Bounds.height / (float)Spell.Bounds.width;
        if (position.size.x * ratio < position.size.y)
        {

            minSize = _canvas.width = (float)position.size.x * .75f;
            _canvas.height = _canvas.width * ratio;

            _tileSize = (float)minSize / (float)Spell.Bounds.width;
        }
        else
        {

            minSize = _canvas.height = (float)position.size.y * .75f;
            _canvas.width = _canvas.height / ratio;

            _tileSize = (float)minSize / (float)Spell.Bounds.height;
        }


        _canvas.center = new Vector2(position.size.x * 0.5f, _canvas.height * 0.5f + EditorGUILayout.GetControlRect().y);
        GUILayout.Space(_canvas.height);
    }

    void DrawCanvas()
    {
        if (Event.current.type == EventType.Repaint)
        {
            EditorGUI.DrawRect(_canvas, Color.white);

            //draw all tiles inside white area
            for (int i = 0; i < Spell.Bounds.width; i++)
            {
                for (int j = 0; j < Spell.Bounds.height; j++)
                {

                    Vector2Int value = new Vector2Int(i + Mathf.FloorToInt(Spell.Bounds.xMin), j + Mathf.FloorToInt(Spell.Bounds.yMin));
                    if (Spell.AffectedTiles.Contains(value))
                    {
                        Color c = Color.red;
                        EditorGUI.DrawRect(new Rect(_canvas.position.x + i * _tileSize + 1, _canvas.position.y + j * _tileSize + 1, _tileSize - 2, _tileSize - 2), c);
                    }

                }
            }
        }

    }

    private void OnEnable()
    {
        name = Spell.name + " Editor";
        titleContent = new(name);

    }


    private void OnDisable()
    {
        AssetDatabase.SaveAssetIfDirty(Spell);
    }
}

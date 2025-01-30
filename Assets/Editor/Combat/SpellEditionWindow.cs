using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellEditionWindow : EditorWindow
{
    public SpellData Spell;

    Rect _canvas;
    float _tileSize;

    int _selectedTile;

    //visuals
    const int canvasOutlineSize = 2;
    static readonly Color c = new Color(.95f, .25f, .35f);
    void OnGUI()
    {

        //EditorGUILayout.LabelField("Canvas");
        EditorGUILayout.Separator();

        UpdateCanvas();
        HandleInputs();
        DrawCanvas();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("LMB : Paint ; RMB : Erase");
        EditorGUILayout.Separator();
        if (GUILayout.Button("Clear")) Spell.AffectedTiles.Clear();
    }



    void HandleInputs()
    {

        if (Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
        {
            if (_canvas.Contains(Event.current.mousePosition))
            {
                Vector2Int c = ((Vector2)Unity.Mathematics.math.remap(_canvas.min, _canvas.max, (Vector2)Spell.Bounds.min, (Vector2)Spell.Bounds.max, Event.current.mousePosition)).Floor();
                //c.y *= -1;
                if (c != Vector2Int.zero)
                {
                    if (Event.current.button == 0)
                    {
                        if (!Spell.AffectedTiles.Contains(new Vector2Int(c.x, c.y)))
                        {


                            Spell.AffectedTiles.Add(new Vector2Int(c.x, c.y));
                            EditorUtility.SetDirty(Spell);
                            Event.current.Use();
                        }
                    }
                    else if (Event.current.button == 1)
                    {
                        if (Spell.AffectedTiles.Contains(new Vector2Int(c.x, c.y)))
                        {
                            Spell.AffectedTiles.Remove(new Vector2Int(c.x, c.y));
                            EditorUtility.SetDirty(Spell);
                            Event.current.Use();
                        }
                    }
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
            Color backgroundColor = EditorGUIUtility.isProSkin ? new Color32(56, 56, 56, 255) : new Color32(194, 194, 194, 255);

            EditorGUI.DrawRect(_canvas.ExpandBy(new RectOffset(canvasOutlineSize, canvasOutlineSize, canvasOutlineSize, canvasOutlineSize)), Color.grey);
            EditorGUI.DrawRect(_canvas, backgroundColor);

            //draw all tiles inside white area
            for (int i = 0; i < Spell.Bounds.width; i++)
            {
                for (int j = 0; j < Spell.Bounds.height; j++)
                {

                    Vector2Int value = new Vector2Int(i + Mathf.FloorToInt(Spell.Bounds.xMin), j + Mathf.FloorToInt(Spell.Bounds.yMin));
                    //value.y *= -1;
                    if (Spell.AffectedTiles.Contains(value))
                    {
                        EditorGUI.DrawRect(new Rect(_canvas.position.x + i * _tileSize + 1, _canvas.position.y + j * _tileSize + 1, _tileSize - 2, _tileSize - 2), c);
                    }

                }
            }

            float  x = -Spell.Bounds.xMin;
            float  y = -Spell.Bounds.yMin;
            EditorGUI.DrawRect(new Rect(_canvas.position.x + x * _tileSize + 1, _canvas.position.y + y * _tileSize + 1, _tileSize - 2, _tileSize - 2),Color.white);
        }

    }

    public void SetUp(SpellData data)
    {
        Spell = data;
        name = Spell.Name;
        titleContent = new(name);

    }


    private void OnDisable()
    {
        AssetDatabase.SaveAssetIfDirty(Spell);
    }
}

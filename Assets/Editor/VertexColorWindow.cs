using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.GraphicsBuffer;




public class VertexColorWindow : EditorWindow
{
    Mesh mesh;
    Color color;
    void OnGUI()
    {
        EditorGUILayout.Space(3);
        EditorGUILayout.LabelField("Target Mesh");

        mesh = (Mesh)EditorGUILayout.ObjectField(mesh,typeof(Mesh), true);

        if (GUILayout.Button("pick mesh on selected object"))
        {
            mesh = Selection.activeObject as Mesh;
            if (!mesh)
            {
                GameObject o = Selection.activeObject as GameObject;
                if (o && o.TryGetComponent<MeshFilter>(out MeshFilter m)) mesh = m.sharedMesh;
            }
            if (!mesh) Debug.Log("No mesh found"); else Debug.Log("found mesh : "+mesh.name);
        }

        EditorGUILayout.Space(3);
        EditorGUILayout.LabelField("VertexColors");

        color = EditorGUILayout.ColorField(color);
        if (GUILayout.Button("Apply Color to mesh vertices (no undo)"))
        {
            Assert.IsTrue(mesh.isReadable,"Selected Mesh isn't readable.");
            Debug.Log("Set Mesh vertex colors to " + color);
            Color[] a = new Color[mesh.vertexCount];
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                a[i] = color;
            }
            mesh.SetColors(a);
            

            AssetDatabase.SaveAssetIfDirty(mesh);

        }

        if (GUILayout.Button("print vertex Color"))
        {
            Debug.Log("Vertex Color Count : " + mesh.vertexCount);
            
            for (int i = 0; i < mesh.colors.Length; i++)
            {
                Debug.Log(mesh.colors[i]);
            }
        }
    }

    private void OnEnable()
    {
        mesh = Selection.activeObject as Mesh;
        if (!mesh) 
        {
            GameObject o = Selection.activeObject as GameObject;
            if (o && o.TryGetComponent<MeshFilter>(out MeshFilter m)) mesh = m.sharedMesh;
        } 
    }

    


    [MenuItem("Window/Vertex Color Tool")]
    static void ShowWindow()
    {
        GetWindow<VertexColorWindow>().Show();
    }
}

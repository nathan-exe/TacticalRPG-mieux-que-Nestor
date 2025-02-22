using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.VFX;

namespace SimpleVFXs
{
    public class GroundHeightDetector : MonoBehaviour
    {
        public VisualEffect VFX;
        public string groundHeightPropertieName = "GroundWorldY";
        public LayerMask GroundLayerMask;
        public bool AutoUpdatePropertieOnStart = true;
        public void UpdateVFXgroundHeightPropertie()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, float.PositiveInfinity, GroundLayerMask.value))
            {
                if(VFX.HasFloat(groundHeightPropertieName))
                VFX.SetFloat(groundHeightPropertieName, hit.point.y);
            }
            else
            {
                if (VFX.HasFloat(groundHeightPropertieName))
                VFX.SetFloat(groundHeightPropertieName, float.NegativeInfinity);
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(GroundHeightDetector))]
    class GroundHeightDetectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GroundHeightDetector myScript = (GroundHeightDetector)target;
            if (GUILayout.Button("Update VFX ground Height Propertie"))
            {
                myScript.UpdateVFXgroundHeightPropertie();
            }


            GUILayout.TextArea("Be sure to always call UpdateVFXgroundHeightPropertie() before playing the Visual effect, or else the effect might look weird if the object was moved since the last time it was called.");

        }
    }

#endif
}
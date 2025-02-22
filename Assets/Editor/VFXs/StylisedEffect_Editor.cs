using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SimpleVFXs
{

#if UNITY_EDITOR

    [CustomEditor(typeof(StylisedEffect))]
    class StylisedEffect_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            StylisedEffect myScript = (StylisedEffect)target;
            if (GUILayout.Button("Trigger Main Event"))
            {
                myScript.TriggerMainEvent();
            }

            if (!(GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).supportsCameraOpaqueTexture)
            {
                CustomEditorHelper.drawInfoPanel($"The current URP Asset doesn't support  <b>Opaque Texture</b>, wich might break some visual effects such as shockwaves or heat distortion. You can enable <b><color={CustomEditorHelper.HighlightColor}>\"Opaque Texture\" </color></b>on the main Camera or on the current <b>URP Asset</b> (search for \"URP-HighFidelity\" by default)"/*, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects."*/);
                //GUILayout.TextArea("The current URP Asset doesn't support Camera Opaque Texture, wich might break some effects on the current VFX. You can either enable \"Opaque Texture\" On the current URP Asset or on the main Camera, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects.");
            }

            if (!(GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).supportsCameraDepthTexture)
            {
                CustomEditorHelper.drawInfoPanel($"The current URP Asset doesn't support  <b>Depth Texture</b>, wich might break some visual effects such as soft particles. You can enable <b><color={CustomEditorHelper.HighlightColor}>\"Depth Texture\" </color></b>on the main Camera or on the current <b>URP Asset</b> (search for \"URP-HighFidelity\" by default)"/*, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects."*/);
                //GUILayout.TextArea("The current URP Asset doesn't support Camera Opaque Texture, wich might break some effects on the current VFX. You can either enable \"Opaque Texture\" On the current URP Asset or on the main Camera, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects.");
            }
        }
    }

#endif


}
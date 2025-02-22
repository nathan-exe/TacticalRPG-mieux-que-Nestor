using UnityEditor;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SimpleVFXs
{

#if UNITY_EDITOR

    [CustomEditor(typeof(shieldVisual))]
    class shieldVisual_Editor : Editor
    {
        public override void OnInspectorGUI()
        {

            CustomEditorHelper.drawInfoPanel("This script contains some functions meant to easily control the shield VFX's visuals and animations,but does not contain any real gameplay logic.\n\nYou can call:\n-ActivateShield()\n-DeactivateShield\n-PlayBreakAnimation()\n-PlayHitAnimation\n-SetHealthValue()\n\nThe demo scene contains an example script with some fake gameplay logic. ");

            DrawDefaultInspector();

            shieldVisual myScript = (shieldVisual)target;

            GUILayout.Space(10);

            if (GUILayout.Button("ActivateShield()"))
            {
                myScript.ActivateShield();
            }

            GUILayout.Space(4);
            if (GUILayout.Button("DisableShield()"))
            {
                myScript.DeactivateShield();
            }

            GUILayout.Space(4);
            if (GUILayout.Button("PlayBreakAnimation()"))
            {
                myScript.PlayBreakAnimation();
            }

            GUILayout.Space(4);
            if (GUILayout.Button("PlayHitAnimation()"))
            {
                myScript.PlayHitAnimation();
            }

            if (!(GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).supportsCameraOpaqueTexture)
            {
                CustomEditorHelper.drawInfoPanel($"The current URP Asset doesn't support  <b>Opaque Texture</b>, wich might break some visual effects such as screen space reflections. You can enable <b><color={CustomEditorHelper.HighlightColor}>\"Opaque Texture\" </color></b>on the main Camera or on the current <b>URP Asset</b> (search for \"URP-HighFidelity\" by default)"/*, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects."*/);
                //GUILayout.TextArea("The current URP Asset doesn't support Camera Opaque Texture, wich might break some effects on the current VFX. You can either enable \"Opaque Texture\" On the current URP Asset or on the main Camera, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects.");
            }

            if (!(GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).supportsCameraDepthTexture)
            {
                CustomEditorHelper.drawInfoPanel($"The current URP Asset doesn't support  <b>Depth Texture</b>, wich might break some visual effects such as the thin line that appears where the shield touches other objects. You can enable <b><color={CustomEditorHelper.HighlightColor}>\"Depth Texture\" </color></b>on the main Camera or on the current <b>URP Asset</b> (search for \"URP-HighFidelity\" by default)"/*, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects."*/);
                //GUILayout.TextArea("The current URP Asset doesn't support Camera Opaque Texture, wich might break some effects on the current VFX. You can either enable \"Opaque Texture\" On the current URP Asset or on the main Camera, or disable \"_UseOpaqueTexture\" in the visual effect's properties to disable everything relying on the opaque texture,such as heat distortion or shockwave effects.");
            }
        }
    }

#endif


}
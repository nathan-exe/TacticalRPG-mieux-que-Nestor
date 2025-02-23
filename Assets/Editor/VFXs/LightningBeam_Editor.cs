using UnityEditor;


namespace SimpleVFXs
{

#if UNITY_EDITOR

    [CustomEditor(typeof(LightningBeam), true)]
class LightningBeam_Editor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        CustomEditorHelper.drawInfoPanel($"This effect simulates a lightning beam hitting a precise target thanks to this c# script,with no vfx graph.\nYou have to call <b><color={CustomEditorHelper.HighlightColor}>PlayAnimationTowardTargetTransform()</color></b>\n or <b><color={CustomEditorHelper.HighlightColor}>PlayAnimationTowardPosition()</color></b>\n in order to use it.\n PlayAnimationTowardTargetTransform() won't work if _targetTransform is null.");
        
        //AvatarInspector/HeadZoom
        //WelcomeScreen.AssetStoreLogo
        /*foreach (string s in "ScriptableObject Icon\r\n_Popup\r\n_Help\r\nClipboard\r\nSocialNetworks.UDNOpen\r\nSocialNetworks.Tweet\r\nSocialNetworks.FacebookShare\r\nSocialNetworks.LinkedInShare\r\nSocialNetworks.UDNLogo\r\nanimationvisibilitytoggleoff\r\nanimationvisibilitytoggleon\r\ntree_icon\r\ntree_icon_leaf\r\ntree_icon_frond\r\ntree_icon_branch\r\ntree_icon_branch_frond\r\nediticon.sml\r\nTreeEditor.Refresh\r\nTreeEditor.Duplicate\r\nTreeEditor.Trash\r\nTreeEditor.AddBranches\r\nTreeEditor.AddLeaves\r\nTreeEditor.Trash\r\npreAudioPlayOn\r\npreAudioPlayOff\r\nAvatarInspector/RightFingersIk\r\nAvatarInspector/LeftFingersIk\r\nAvatarInspector/RightFeetIk\r\nAvatarInspector/LeftFeetIk\r\nAvatarInspector/RightFingers\r\nAvatarInspector/LeftFingers\r\nAvatarInspector/RightArm\r\nAvatarInspector/LeftArm\r\nAvatarInspector/RightLeg\r\nAvatarInspector/LeftLeg\r\nAvatarInspector/Head\r\nAvatarInspector/Torso\r\nAvatarInspector/MaskEditor_Root\r\nAvatarInspector/BodyPartPicker\r\nAvatarInspector/BodySIlhouette\r\nMirror\r\nSpeedScale\r\nToolbar Minus\r\nToolbar Plus More\r\nToolbar Plus\r\nAnimatorController Icon\r\nTextAsset Icon\r\nShader Icon\r\nboo Script Icon\r\ncs Script Icon\r\njs Script Icon\r\nPrefab Icon\r\nProfiler.NextFrame\r\nProfiler.PrevFrame\r\nsv_icon_none\r\nColorPicker.CycleSlider\r\nColorPicker.CycleColor\r\nEyeDropper.Large\r\nClothInspector.PaintValue\r\nClothInspector.ViewValue\r\nClothInspector.SettingsTool\r\nClothInspector.PaintTool\r\nClothInspector.SelectTool\r\nWelcomeScreen.AssetStoreLogo\r\nWelcomeScreen.AssetStoreLogo\r\nAboutWindow.MainHeader\r\nUnityLogo\r\nAgeiaLogo\r\nMonoLogo\r\nToolbar Minus\r\nPlayButtonProfile Anim\r\nStepButton Anim\r\nPauseButton Anim\r\nPlayButton Anim\r\nPlayButtonProfile On\r\nStepButton On\r\nPauseButton On\r\nPlayButton On\r\nPlayButtonProfile\r\nStepButton\r\nPauseButton\r\nPlayButton\r\nViewToolOrbit On\r\nViewToolZoom On\r\nViewToolMove On\r\nViewToolOrbit On\r\nViewToolOrbit\r\nViewToolZoom\r\nViewToolMove\r\nViewToolOrbit\r\nScaleTool On\r\nRotateTool On\r\nMoveTool On\r\nScaleTool\r\nRotateTool\r\nMoveTool\r\nPauseButton\r\nPlayButton\r\nToolbar Minus\r\nToolbar Plus\r\nUnityLogo\r\n_Help\r\n_Popup\r\nIcon Dropdown\r\nAvatar Icon\r\nAvatarPivot\r\nSpeedScale\r\nAvatarInspector/DotSelection\r\nAvatarInspector/DotFrameDotted\r\nAvatarInspector/DotFrame\r\nAvatarInspector/DotFill\r\nAvatarInspector/RightHandZoom\r\nAvatarInspector/LeftHandZoom\r\nAvatarInspector/HeadZoom\r\nAvatarInspector/RightLeg\r\nAvatarInspector/LeftLeg\r\nAvatarInspector/RightFingers\r\nAvatarInspector/RightArm\r\nAvatarInspector/LeftFingers\r\nAvatarInspector/LeftArm\r\nAvatarInspector/Head\r\nAvatarInspector/Torso\r\nAvatarInspector/RightHandZoomSilhouette\r\nAvatarInspector/LeftHandZoomSilhouette\r\nAvatarInspector/HeadZoomSilhouette\r\nAvatarInspector/BodySilhouette\r\nAnimation.AddKeyframe\r\nAnimation.NextKey\r\nAnimation.PrevKey\r\nlightMeter/redLight\r\nlightMeter/orangeLight\r\nlightMeter/lightRim\r\nlightMeter/greenLight\r\nAnimation.AddEvent\r\nSceneviewAudio\r\nSceneviewLighting\r\nMeshRenderer Icon\r\nTerrain Icon\r\nsv_icon_none\r\nBuildSettings.SelectedIcon\r\nAnimation.AddEvent\r\nAnimation.AddKeyframe\r\nAnimation.NextKey\r\nAnimation.PrevKey\r\nAnimation.Record\r\nAnimation.Play\r\nPreTextureRGB\r\nPreTextureAlpha\r\nPreTextureMipMapHigh\r\nPreTextureMipMapLow\r\nTerrainInspector.TerrainToolSettings\r\nTerrainInspector.TerrainToolPlants\r\nTerrainInspector.TerrainToolTrees\r\nTerrainInspector.TerrainToolSplat\r\nTerrainInspector.TerrainToolSmoothHeight\r\nTerrainInspector.TerrainToolSetHeight\r\nTerrainInspector.TerrainToolRaise\r\nSettingsIcon\r\nPauseButton\r\nPlayButton\r\nPreMatLight1\r\nPreMatLight0\r\nPreMatTorus\r\nPreMatCylinder\r\nPreMatCube\r\nPreMatSphere\r\nPreMatLight1\r\nPreMatLight0\r\nCamera Icon\r\nToolbar Minus\r\nToolbar Plus\r\nAnimation.EventMarker\r\nAS Badge New\r\nAS Badge Move\r\nAS Badge Delete\r\nWaitSpin00\r\nWaitSpin01\r\nWaitSpin02\r\nWaitSpin03\r\nWaitSpin04\r\nWaitSpin05\r\nWaitSpin06\r\nWaitSpin07\r\nWaitSpin08\r\nWaitSpin09\r\nWaitSpin10\r\nWaitSpin11\r\nWelcomeScreen.AssetStoreLogo\r\nWelcomeScreen.UnityAnswersLogo\r\nWelcomeScreen.UnityForumLogo\r\nWelcomeScreen.UnityBasicsLogo\r\nWelcomeScreen.VideoTutLogo\r\nWelcomeScreen.MainHeader\r\nUnityLogo\r\npreAudioPlayOn\r\npreAudioPlayOff\r\nAnimation.EventMarker\r\nPreMatLight1\r\nPreMatLight0\r\nPreMatTorus\r\nPreMatCylinder\r\nPreMatCube\r\nPreMatSphere\r\nTreeEditor.Duplicate\r\nToolbar Minus\r\nToolbar Plus\r\nPreTextureMipMapHigh\r\nPreTextureMipMapLow\r\nPreTextureRGB\r\nPreTextureAlpha\r\nVerticalSplit\r\nHorizontalSplit\r\nIcon Dropdown\r\nPrefabNormal Icon\r\nPrefabModel Icon\r\nPrefabNormal Icon\r\nPrefab Icon\r\nGameObject Icon\r\npreAudioLoopOn\r\npreAudioLoopOff\r\npreAudioPlayOn\r\npreAudioPlayOff\r\npreAudioAutoPlayOn\r\npreAudioAutoPlayOff\r\nBuildSettings.Web.Small\r\nBuildSettings.Standalone.Small\r\nBuildSettings.iPhone.Small\r\nBuildSettings.Android.Small\r\nBuildSettings.BlackBerry.Small\r\nBuildSettings.Tizen.Small\r\nBuildSettings.XBox360.Small\r\nBuildSettings.XboxOne.Small\r\nBuildSettings.PS3.Small\r\nBuildSettings.PSP2.Small\r\nBuildSettings.PS4.Small\r\nBuildSettings.PSM.Small\r\nBuildSettings.FlashPlayer.Small\r\nBuildSettings.Metro.Small\r\nBuildSettings.WP8.Small\r\nBuildSettings.SamsungTV.Small\r\nBuildSettings.Web\r\nBuildSettings.Standalone\r\nBuildSettings.iPhone\r\nBuildSettings.Android\r\nBuildSettings.BlackBerry\r\nBuildSettings.Tizen\r\nBuildSettings.XBox360\r\nBuildSettings.XboxOne\r\nBuildSettings.PS3\r\nBuildSettings.PSP2\r\nBuildSettings.PS4\r\nBuildSettings.PSM\r\nBuildSettings.FlashPlayer\r\nBuildSettings.Metro\r\nBuildSettings.WP8\r\nBuildSettings.SamsungTV\r\nTreeEditor.BranchTranslate\r\nTreeEditor.BranchRotate\r\nTreeEditor.BranchFreeHand\r\nTreeEditor.BranchTranslate On\r\nTreeEditor.BranchRotate On\r\nTreeEditor.BranchFreeHand On\r\nTreeEditor.LeafTranslate\r\nTreeEditor.LeafRotate\r\nTreeEditor.LeafTranslate On\r\nTreeEditor.LeafRotate On\r\nsv_icon_dot15_pix16_gizmo\r\nsv_icon_dot1_sml\r\nsv_icon_dot4_sml\r\nsv_icon_dot7_sml\r\nsv_icon_dot5_pix16_gizmo\r\nsv_icon_dot11_pix16_gizmo\r\nsv_icon_dot12_sml\r\nsv_icon_dot15_sml\r\nsv_icon_dot9_pix16_gizmo\r\nsv_icon_name6\r\nsv_icon_name3\r\nsv_icon_name4\r\nsv_icon_name0\r\nsv_icon_name1\r\nsv_icon_name2\r\nsv_icon_name5\r\nsv_icon_name7\r\nsv_icon_dot1_pix16_gizmo\r\nsv_icon_dot8_pix16_gizmo\r\nsv_icon_dot2_pix16_gizmo\r\nsv_icon_dot6_pix16_gizmo\r\nsv_icon_dot0_sml\r\nsv_icon_dot3_sml\r\nsv_icon_dot6_sml\r\nsv_icon_dot9_sml\r\nsv_icon_dot11_sml\r\nsv_icon_dot14_sml\r\nsv_label_0\r\nsv_label_1\r\nsv_label_2\r\nsv_label_3\r\nsv_label_5\r\nsv_label_6\r\nsv_label_7\r\nsv_icon_none\r\nsv_icon_dot14_pix16_gizmo\r\nsv_icon_dot7_pix16_gizmo\r\nsv_icon_dot3_pix16_gizmo\r\nsv_icon_dot0_pix16_gizmo\r\nsv_icon_dot2_sml\r\nsv_icon_dot5_sml\r\nsv_icon_dot8_sml\r\nsv_icon_dot10_pix16_gizmo\r\nsv_icon_dot12_pix16_gizmo\r\nsv_icon_dot10_sml\r\nsv_icon_dot13_sml\r\nsv_icon_dot4_pix16_gizmo\r\nsv_label_4\r\nsv_icon_dot13_pix16_gizmo".Split("\r\n"))
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box(EditorGUIUtility.IconContent(s),GUIStyle.none);
            GUILayout.TextArea(s,GUIStyle.none);
            GUILayout.EndHorizontal();

        }*/
    }
}

#endif

}
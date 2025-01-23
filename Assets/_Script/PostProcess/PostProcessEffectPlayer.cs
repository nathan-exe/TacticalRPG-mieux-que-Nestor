using UnityEngine;

public class PostProcessEffectPlayer : MonoBehaviour
{
    //pas terrible mais ça marche pour le scope du projet
    public void PlayFlash()
    {
        PostProcessController.instance.E_ExposureFlash.play();
    }

    public void PlayDistortion()
    {
        PostProcessController.instance.E_ScreenDistortion.play();
    }
    public void PlayFadeIn()
    {
        PostProcessController.instance.FadeIn.play();
    }
    public void PlayFadeOut()
    {
        PostProcessController.instance.FadeOut.play();
    }
}

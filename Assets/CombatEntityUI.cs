using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntityUI : MonoBehaviour
{
    [Header("References")]
    public CoolSlider HealthSlider;
    public CoolSlider ManaSlider;
    public void PreviewManaLoss(float loss) => ManaSlider.PreviewValue(ManaSlider.Value - loss);
    public void CancelManaLossPreview() => ManaSlider.CancelPreview();


    private void Start()
    {
        ManaSlider.MaxValue = CombatEntity.MaxManaPerEntity;
        ManaSlider.Value = CombatEntity.MaxManaPerEntity;
    }
}

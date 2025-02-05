using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntityUI : MonoBehaviour
{
    [Header("References")]
    public CoolSlider HealthSlider;
    public CoolSlider ManaSlider;

    //previews
    public void PreviewManaLoss(float loss) => ManaSlider.PreviewValue(ManaSlider.Value - loss);
    public void CancelManaLossPreview() => ManaSlider.CancelPreview();

    public void PreviewHPLoss(float loss) => HealthSlider.PreviewValue(HealthSlider.Value - loss);
    public void CancelHPLossPreview() => HealthSlider.CancelPreview();

    //set up
    private void Start()
    {
        ManaSlider.MaxValue = CombatEntity.MaxManaPerEntity;
        ManaSlider.Value = CombatEntity.MaxManaPerEntity;

        HealthSlider.MaxValue = transform.parent.GetComponent<CombatEntity>().Data.MaxHP;
        transform.parent.GetComponent<HealthComponent>().OnHealthUpdated += (float hp) => HealthSlider.Value = hp;
    }
}

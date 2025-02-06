using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatEntityUI : MonoBehaviour
{
    [Header("References")]
    public CoolSlider HealthSlider;
    public CoolSlider ManaSlider;
    public void PreviewManaLoss(float loss) => ManaSlider.PreviewValue(ManaSlider.Value - loss);
    public void CancelManaLossPreview() => ManaSlider.CancelPreview();

    public void PreviewHPLoss(float loss) => HealthSlider.PreviewValue(HealthSlider.Value - loss);
    public void CancelHPLossPreview() => HealthSlider.CancelPreview();
    public TextMeshProUGUI Name;

    private void Awake()
    {
        transform.parent.GetComponent<HealthComponent>().OnHealthUpdated += (float hp) => HealthSlider.Value = hp;
    }

    //set up
    private void Start()
    {
        ManaSlider.MaxValue = CombatEntity.MaxManaPerEntity;
        ManaSlider.Value = CombatEntity.MaxManaPerEntity;

        HealthSlider.MaxValue = transform.parent.GetComponent<CombatEntity>().Data.MaxHP;
    }
}

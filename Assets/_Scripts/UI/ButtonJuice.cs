using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonJuice : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    Vector3 _baseScale;
    [SerializeField] float _tweeningTime = .2f;
    [SerializeField] float _AdditionalScaleOnPointerEnter = .2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_baseScale+Vector3.one* _AdditionalScaleOnPointerEnter, _tweeningTime).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_baseScale, _tweeningTime).SetUpdate(true);
    }

    void OnEnable()
    {
        transform.localScale = _baseScale;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _baseScale = transform.localScale;
    }




}

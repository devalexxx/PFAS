using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Références")]
    [SerializeField] private GameObject cursor;      // l'objet Curseur (Image/RectTransform)
    [SerializeField] private RectTransform anchor;   // l'AnchorX du bouton

    [Header("Options")]
    [SerializeField] private float ecart;
    [SerializeField] private bool hideOnExit = true;

    private RectTransform _cursorRT;
    private RectTransform _child1;
    private RectTransform _child2;

    private void Awake()
    {
        if (cursor != null)
        {
            _cursorRT = cursor.transform as RectTransform;

            if (cursor.transform.childCount >= 2)
            {
                _child1 = cursor.transform.GetChild(0) as RectTransform;
                _child2 = cursor.transform.GetChild(1) as RectTransform;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!cursor || !anchor) return;

        cursor.SetActive(true);

        _cursorRT.anchoredPosition = anchor.anchoredPosition;
        _cursorRT.rotation = anchor.rotation;      // si tu veux suivre l’orientation

        // Ajuste la position X des 2 enfants
        if (_child1 != null && _child2 != null)
        {
            var pos1 = _child1.anchoredPosition;
            var pos2 = _child2.anchoredPosition;

            pos1.x = -ecart;
            pos2.x = ecart;

            _child1.anchoredPosition = pos1;
            _child2.anchoredPosition = pos2;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hideOnExit && cursor) cursor.SetActive(false);
    }
}

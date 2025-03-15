using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Loading;
using UnityEngine.UI;

namespace PFAS.UI
{
    public class ScrollingText : MonoBehaviour
    {
        public float speed = 100f; // Vitesse du déplacement

        private RectTransform _rectTransform;
        private float _startPosition;
        private float _resetPosition;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

            float t_textWidth = _rectTransform.rect.width;

            // Position de départ (juste à droite du masque)
            _startPosition = 243;

            // Position de reset (quand le texte est totalement sorti par la gauche)
            _resetPosition = _startPosition - t_textWidth - 250;

            // Placer le texte au départ
            _rectTransform.anchoredPosition = new Vector2(_startPosition, 0);
        }

        void Update()
        {
            // Déplacement vers la gauche
            _rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // Si le texte est entièrement sorti de l'image par la gauche
            if (_rectTransform.anchoredPosition.x <= _resetPosition)
            {
                // Réapparition à droite
                _rectTransform.anchoredPosition = new Vector2(_startPosition, 0);
            }
        }
    }
}
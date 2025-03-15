using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PFAS.UI
{
    public class ScrollingText : MonoBehaviour
    {
        TextMeshProUGUI _text;
        public float speed = 100f;

        List<string> _newsQueue = new List<string>();

        public bool playNews = false;

        private RectTransform _rectTransform;
        private float _startPosition;
        private float _resetPosition;

        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();

            _rectTransform = GetComponent<RectTransform>();

            Init();
        }

        private void Init()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

            float t_textWidth = _rectTransform.rect.width;

            // Position de départ (juste à droite du masque)
            _startPosition = 243;

            // Position de reset (quand le texte est totalement sorti par la gauche)
            _resetPosition = _startPosition - t_textWidth - 250;

            // Placer le texte au départ
            _rectTransform.anchoredPosition = new Vector2(_startPosition, 0);
        }

        public void SetUpNews(string p_text)
        {
            _newsQueue.Add(p_text);
            if (_newsQueue.Count == 1)
            {
                playNews = true;
                _text.text = _newsQueue[0];
                Init();
            }
        }

        void Update()
        {
            if(playNews)
            {
                _rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

                if (_rectTransform.anchoredPosition.x <= _resetPosition)
                {
                    _rectTransform.anchoredPosition = new Vector2(_startPosition, 0);
                    _newsQueue.RemoveAt(0);
                    if (_newsQueue.Count <= 0)
                    {
                        playNews = false;
                    }
                    else
                    {
                        _text.text = _newsQueue[0];
                        Init();
                    }
                }
            }
        }
    }
}
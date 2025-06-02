using Unity.VisualScripting;
using UnityEngine;

namespace PFAS.Map
{
    public class CountryOutline : MonoBehaviour
    {
        // The outline of the country
        private SpriteRenderer _renderer;

        public bool hasOutline {  get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Outline()
        {
            string t_spritePath = $"Sprites/Map/{name}Highlighted";
            _renderer.sprite = Resources.Load<Sprite>(t_spritePath);

            hasOutline = true;

            transform.position += new Vector3(0, 0, -2);
        }

        public void RemoveOutline()
        {
            string t_spritePath = $"Sprites/Map/{name}";
            _renderer.sprite = Resources.Load<Sprite>(t_spritePath);

            hasOutline = false;

            transform.position += new Vector3(0, 0, 2);
        }
    }
}
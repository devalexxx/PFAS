using UnityEngine;

namespace PFAS.Map
{
    public class CountryOutline : MonoBehaviour
    {
        // The outline of the country
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Outline()
        {
            string t_spritePath = $"Sprites/Map/{name}_Highlighted";
            _renderer.sprite = Resources.Load<Sprite>(t_spritePath);

            transform.position += new Vector3(0, 0, -2);
        }

        public void RemoveOutline()
        {
            string t_spritePath = $"Sprites/Map/{name}";
            _renderer.sprite = Resources.Load<Sprite>(t_spritePath);

            transform.position += new Vector3(0, 0, 2);
        }
    }
}
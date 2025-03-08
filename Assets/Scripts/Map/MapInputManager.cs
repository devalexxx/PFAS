using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace PFAS.Map
{
    public class MapInputManager : MonoBehaviour
    {
        // The selected country
        private CountryHighlight _selectedCountry;

        void Update()
        {
            // Check if the player has clicked the mouse
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in the world
                Vector3 t_worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 t_worldPoint2D = new Vector2(t_worldPoint.x, t_worldPoint.y);

                // Get the collider that is hit by the ray
                RaycastHit2D t_hit = Physics2D.Raycast(t_worldPoint2D, Vector2.zero);

                if (t_hit.collider != null)
                {

                    // Get the country highlight component of the hit object
                    CountryHighlight t_country = t_hit.collider.GetComponent<CountryHighlight>();
                    
                    // If the hit object has a country highlight component
                    if(t_country != null)
                    {
                        // if another country was selected, remove highlight
                        if (_selectedCountry != null)
                        {
                            _selectedCountry.RemoveHighlight();
                        }

                        // If the hit country is not the selected country
                        if (_selectedCountry != t_country)
                        {
                            // Highlight the country and set it as the selected country
                            _selectedCountry = t_country;
                            _selectedCountry.Highlight();

                        }
                    }
                    
                }
            }
        }
    }
}
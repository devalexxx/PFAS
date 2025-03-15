using PFAS.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PFAS.Map
{
    public class MapInputManager : MonoBehaviour
    {
        // Reference to the UI manager
        [SerializeField] private UIManager UIManager;

        // The selected country
        private CountryOutline _selectedCountry;

        [SerializeField] private InputActionReference _selectAction;

        private void Awake()
        {
            //manage click action
            _selectAction.action.performed += ctx => _SelectCoutry();
        }

        private void _SelectCoutry()
        {
            //if the mouse is over a UI element, we do nothing
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) { return; }

            // Get the mouse position in the world
            Vector3 t_worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 t_worldPoint2D = new Vector2(t_worldPoint.x, t_worldPoint.y);

            // Get the collider that is hit by the ray
            RaycastHit2D t_hit = Physics2D.Raycast(t_worldPoint2D, Vector2.zero);

            if (t_hit.collider != null)
            {

                // Get the country outline component of the hit object
                CountryOutline t_country = t_hit.collider.GetComponent<CountryOutline>();

                // If the hit object has a country outline component
                if (t_country != null)
                {
                    if (_selectedCountry != null)
                    {
                        // remove outline
                        _selectedCountry.RemoveOutline();
                    }

                    // If the hit country is not the selected country
                    if (_selectedCountry != t_country)
                    {
                        // Outline the country and set it as the selected country
                        _selectedCountry = t_country;
                        _selectedCountry.Outline();

                        UIManager.ShowCountryPanel(t_country.gameObject.GetComponent<Country>());
                    }
                    else
                    {
                        _selectedCountry = null;
                        UIManager.HideCountryPanel();
                    }
                }
                else
                {
                    if (_selectedCountry != null)
                    {
                        // remove outline
                        _selectedCountry.RemoveOutline();
                        _selectedCountry = null;
                    }
                }
            }
        }
    }
}
using UnityEngine;

namespace PFAS.MainMenu 
{
    public class URLOpener : MonoBehaviour
    {
        [SerializeField] private string url;

        public void OpenURL()
        {
            if (!string.IsNullOrEmpty(url))
            {
                Application.OpenURL(url);
            }
            else
            {
                Debug.LogWarning("URL is empty or null.");
            }
        }
    }
}
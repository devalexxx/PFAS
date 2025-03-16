using System.Collections.Generic;
using System.Linq;
using PFAS.Objects;
using PFAS.Timer;
using TMPro;
using UnityEngine;

namespace PFAS.SystemEvent
{
    public class EventManager : MonoBehaviour
    {
        /// <summary>
        /// The UI panel that displays event information.
        /// </summary>
        [Header("UI - Event")]
        public GameObject eventPanel;

        /// <summary>
        /// The UI Text element that displays the title of the event.
        /// </summary>
        public TextMeshProUGUI eventTitle;

        /// <summary>
        /// The UI Text element that displays the description of the event.
        /// </summary>
        public TextMeshProUGUI eventDescription;

        public int eventDay = 30;
        public int bubbleDay = 15;
        int _currentDay = 0;

        [Header("Money Buble")]
        public GameObject moneyBuble;
        public int maxAttempts = 100;


        /// <summary>
        /// A list of available events in the game.
        /// </summary>
        List<EventObject> _events = new List<EventObject>();

        TimerManager _timerManager;

        private void Start()
        {
            _timerManager = GetComponent<TimerManager>();
            eventPanel.SetActive(false);

            _events = new List<EventObject>(Resources.LoadAll<EventObject>("Events"));

            TimerManager.OnTick += ChangeDay;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                PolygonCollider2D t_poly = GameManager.instance.countries[0].gameObject.GetComponent<PolygonCollider2D>();
                SpawnObjectInside(t_poly);
            }
        }

        /// <summary>
        /// This function selects a random event from the list of available events that can be used.
        /// </summary>
        /// <returns>Returns a randomly selected EventObject that is available for use.</returns>
        public EventObject GetRandomEvent()
        {
            List<EventObject> t_events = new List<EventObject>();
            t_events.AddRange(_events.Where(item => item.condition.CanUse(_timerManager)));

            return t_events[UnityEngine.Random.Range(0, t_events.Count)];
        }

        /// <summary>
        /// This function displays the event panel and sets the event title and description in the UI. 
        /// It also invokes the event to apply any changes or actions associated with it.
        /// </summary>
        public void ShowEvent()
        {
            var t_event = GetRandomEvent();

            t_event.instance.Use();

            eventPanel.SetActive(true);
            eventTitle.text  = t_event.eventName;
            eventDescription.text = t_event.eventDescription + "\n" + t_event.instance.ToString();
        }

        public void ChangeDay()
        {
            _currentDay++;
            if(_currentDay % eventDay == 0)
            {
                if (Random.value < 0.5f) ShowEvent();
            }
            else if(_currentDay % bubbleDay == 0)
            {

                PolygonCollider2D t_poly = GameManager.instance.GetRandomCountry().gameObject.GetComponent<PolygonCollider2D>();
                SpawnObjectInside(t_poly);
            }
        }

        public void SpawnObjectInside(PolygonCollider2D p_polygonCollider)
        {
            if (p_polygonCollider == null || moneyBuble == null) return;

            int t_attempts = 0;
            Vector2 t_spawnPos;

            do
            {
                t_spawnPos = _GetRandomPointInBounds(p_polygonCollider.bounds);
                t_attempts++;
            }
            while (!_IsPointInsidePolygon(t_spawnPos, p_polygonCollider) && t_attempts < maxAttempts);

            if (t_attempts < maxAttempts)
            {
                Instantiate(moneyBuble, t_spawnPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Impossible de trouver une position valide dans le polygone !");
            }
        }

        Vector2 _GetRandomPointInBounds(Bounds bounds)
        {
            float t_x = Random.Range(bounds.min.x, bounds.max.x);
            float t_y = Random.Range(bounds.min.y, bounds.max.y);
            return new Vector2(t_x, t_y);
        }

        bool _IsPointInsidePolygon(Vector2 point, PolygonCollider2D p_polygonCollider)
        {
            return p_polygonCollider.OverlapPoint(point);
        }
    }
}

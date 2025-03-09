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
        [Header("UI")]
        public GameObject eventPanel;

        /// <summary>
        /// The UI Text element that displays the title of the event.
        /// </summary>
        public TextMeshProUGUI eventTitle;

        /// <summary>
        /// The UI Text element that displays the description of the event.
        /// </summary>
        public TextMeshProUGUI eventDescription;

        public int EventDay = 30;
        int _currentDay = 0;


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
            if(_currentDay == EventDay)
            {
                if (Random.value < 0.5f) ShowEvent();
                _currentDay = 0;
            }
        }
    }
}

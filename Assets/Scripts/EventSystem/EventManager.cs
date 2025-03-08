using System.Collections.Generic;
using System.Linq;
using PFAS.Objects;
using TMPro;
using UnityEngine;

namespace PFAS.EventSystem
{
    public class EventManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject eventPanel;
        public TextMeshProUGUI eventTitle;
        public TextMeshProUGUI eventDescription;

        List<EventObject> _events = new List<EventObject>();

        private void Start()
        {
            eventPanel.SetActive(false);

            _events = new List<EventObject>(Resources.LoadAll<EventObject>("Events"));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ShowEvent();
            }
        }

        public EventObject GetRandomEvent()
        {
            List<EventObject> t_events = new List<EventObject>();
            t_events.AddRange(_events.Where(item => item.instance.CanUse()));

            return t_events[Random.Range(0, t_events.Count)];
        }

        public void ShowEvent()
        {
            var t_event = GetRandomEvent();

            t_event.instance.Use();

            eventPanel.SetActive(true);
            eventTitle.text = t_event.eventName;
            eventDescription.text = t_event.eventDescription + " " + t_event.instance.ToString();
        }
    }
}

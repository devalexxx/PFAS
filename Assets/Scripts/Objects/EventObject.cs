using PFAS.Attributes;
using PFAS.EventSystem.Events;
using UnityEngine;

namespace PFAS.Objects
{
    [CreateAssetMenu(fileName = "New Event", menuName = "EventSystem/Event", order = 1)]
    public class EventObject : ScriptableObject
    {
        public string eventName;
        public string eventDescription;

        [field: SerializeReference, SubclassPicker]
        public IEvent instance;
    }
}

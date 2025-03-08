using MyBox;
using PFAS.Attribute;
using PFAS.EventSystem.Events;
using UnityEngine;

namespace PFAS.Objects
{
    [CreateAssetMenu(fileName = "New Event", menuName = "EventSystem/Event", order = 1)]
    public class EventObject : ScriptableObject
    {
        [Header("Info")]
        public string eventName;
        public string eventDescription;

        [Separator]
        [Header("Instance")]
        [field: SerializeReference, SubclassPicker]
        public IEvent instance;
    }
}

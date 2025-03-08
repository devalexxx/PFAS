using UnityEngine;

namespace PFAS.EventSystem.Events
{
    public interface IEvent
    {
        public bool CanUse();

        public void Use();

        public string toString();
    }
}

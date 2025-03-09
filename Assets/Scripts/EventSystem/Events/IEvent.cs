using UnityEngine;

namespace PFAS.SystemEvent.Events
{
    public interface IEvent
    {
        public bool CanUse();

        public void Use();
    }
}

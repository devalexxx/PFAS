using PFAS.Timer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PFAS.SystemEvent {
    public class MoneyBuble : MonoBehaviour, IPointerClickHandler
    {
        public float lifeTime = 5;
        public float reduceTime = 2;
        public int moneyGive = 10;

        int _currentDay = 0;

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.instance.addMoney(moneyGive);
            Destroy(gameObject);
        }

        private void Start()
        {
            _currentDay = 0;
            TimerManager.OnTick += OnDayChange;
        }

        public void OnDayChange()
        {
            _currentDay++;
            if (_currentDay >= lifeTime)
            {
                int t_steps = Mathf.CeilToInt(reduceTime / 0.5f);
                Vector3 t_scaleStep = transform.localScale / t_steps;
                transform.localScale -= t_scaleStep;
            }
            if (_currentDay >= lifeTime + reduceTime)
            {
                transform.localScale = Vector3.zero;
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            TimerManager.OnTick -= OnDayChange;
        }
    }
}
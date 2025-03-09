using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// all commentary are in english
namespace PFAS.Timer
{ 
    public class TimerManager : MonoBehaviour
    {
        [Header("UI")]
        // Assign the UI Text for the date
        [SerializeField] private TextMeshProUGUI _timerText;
        // Assign the Slider for the time scale
        [SerializeField] private Slider _timeScaleInput;

        [Header("Time Settings")]
        // Time scale multiplier: 1 = normal speed, 0 = pause, >1 = accelerated (can only use integer)
        [Range(0, 4)]
        [SerializeField] private int _timeScale = 0;
        // Accumulate time (in real seconds)
        private float _accumulator = 0f;
        // Number of seconds for a day to pass
        [SerializeField] private float _accumulatorMax = 1f;

        public DateTime startDate { get; private set; } = DateTime.Now;
        public DateTime currentDate { get; private set; }

        // Delegate for the event
        public delegate void TickAction();
        // This event will be called at each tick (day) to synchronize the game calculations
        // should subscribe to this event for other scripts to update at each tick
        public static event TickAction OnTick;

        private void Awake()
        {
            currentDate = startDate;
            _UpdateTimerUI();
        }

        void Update()
        {
            // If the game is paused (timeScale = 0), we do nothing
            if (_timeScale <= 0f)
                return;

            // We accumulate the real time multiplied by the timeScale
            _accumulator += Time.deltaTime * _timeScale;
            // When the accumulator reaches or exceeds _accumulatorMax, we advance by one day
            if (_accumulator >= _accumulatorMax)
            {
                _accumulator -= _accumulatorMax;
                _AdvanceDay();
            }
        }

        // Advance one day in the game and update the calendar
        private void _AdvanceDay()
        {
            currentDate = currentDate.AddDays(1);
            _UpdateTimerUI();

            // Trigger the event so that other game systems can synchronize
            if (OnTick != null)
                OnTick();
        }

        // Update the timer display in the UI
        private void _UpdateTimerUI()
        {
            if (_timerText != null)
            {
                _timerText.text = currentDate.ToString("dd/MM/yyyy");
            }
        }

        // Method to modify the time speed from other scripts
        public void SetTimeScale(float newTimeScale)
        {
            _timeScale = (int)newTimeScale;

            // If _timeScale is modified by something else than the slider, we update it
            if (_timeScaleInput != null && _timeScaleInput.value != newTimeScale)
            {
                _timeScaleInput.value = _timeScale;
            }
        }
    }
}
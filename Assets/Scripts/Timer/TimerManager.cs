using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("UI")]
    // Assigne ici le Text de l'UI (par exemple, un TextMeshPro -Text (UI) dans le Canvas positionné en haut à droite)
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Time Settings")]
    // Multiplieur de vitesse du temps : 1 = vitesse normale, 0 = pause, >1 = accéléré
    [SerializeField] private float _timeScale = 1f;
    // Le temps accumulé (en secondes réelles)
    private float _accumulator = 0f;
    [SerializeField] private float _accumulatorMax = 1f;

    public DateTime startDate { get; private set; } = DateTime.Now;
    public DateTime currentDate { get; private set; }

    // Cet événement sera appelé à chaque tick (jour) pour synchroniser les calculs du jeu
    public delegate void TickAction();
    public event TickAction OnTick;

    private void Awake()
    {
        currentDate = startDate;
        _UpdateTimerUI();
    }

    void Update()
    {
        // Si le jeu est en pause (timeScale = 0), on ne fait rien
        if (_timeScale <= 0f)
            return;

        // On accumule le temps réel multiplié par le timeScale
        _accumulator += Time.deltaTime * _timeScale;
        // Quand l'accumulateur atteint ou dépasse 1 seconde (1 jour en jeu), on avance d'un jour
        if (_accumulator >= _accumulatorMax)
        {
            _accumulator -= _accumulatorMax;
            _AdvanceDay();
        }
    }

    // Avance d'un jour dans le jeu et met à jour le calendrier
    private void _AdvanceDay()
    {
        currentDate = currentDate.AddDays(1);
        _UpdateTimerUI();

        // Déclenche l'événement pour que les autres systèmes du jeu se synchronisent
        if (OnTick != null)
            OnTick();
    }

    // Met à jour l'affichage du timer dans l'UI
    private void _UpdateTimerUI()
    {
        if (_timerText != null)
        {
            _timerText.text = currentDate.ToString("dd/MM/yyyy");
        }
    }

    // Méthode pour modifier la vitesse du temps depuis d'autres scripts
    public void SetTimeScale(float newTimeScale)
    {
        _timeScale = newTimeScale;
    }
}


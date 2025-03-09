using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("UI")]
    // Assigne ici le Text de l'UI (par exemple, un TextMeshPro -Text (UI) dans le Canvas positionné en haut à droite)
    public TextMeshProUGUI timerText;

    [Header("Time Settings")]
    // Multiplieur de vitesse du temps : 1 = vitesse normale, 0 = pause, >1 = accéléré
    public float timeScale = 1f;
    // Le temps accumulé (en secondes réelles)
    private float accumulator = 0f;

    [Header("Calendar Settings")]
    public int day = 1;
    public int month = 1;
    public int year = 2025; // Date de départ
    public int daysPerMonth = 30;
    public int monthsPerYear = 12;

    // Cet événement sera appelé à chaque tick (jour) pour synchroniser les calculs du jeu
    public delegate void TickAction();
    public event TickAction OnTick;

    void Update()
    {
        // Si le jeu est en pause (timeScale = 0), on ne fait rien
        if (timeScale <= 0f)
            return;

        // On accumule le temps réel multiplié par le timeScale
        accumulator += Time.deltaTime * timeScale;
        // Quand l'accumulateur atteint ou dépasse 1 seconde (1 jour en jeu), on avance d'un jour
        if (accumulator >= 1f)
        {
            accumulator -= 1f;
            AdvanceDay();
        }
    }

    // Avance d'un jour dans le jeu et met à jour le calendrier
    void AdvanceDay()
    {
        day++;
        if (day > daysPerMonth)
        {
            day = 1;
            month++;
            if (month > monthsPerYear)
            {
                month = 1;
                year++;
            }
        }
        UpdateTimerUI();

        // Déclenche l'événement pour que les autres systèmes du jeu se synchronisent
        if (OnTick != null)
            OnTick();
    }

    // Met à jour l'affichage du timer dans l'UI
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Exemple de format : "JJ/MM/AAAA"
            timerText.text = string.Format("{0:00}/{1:00}/{2}", day, month, year);
        }
    }

    // Méthode pour modifier la vitesse du temps depuis d'autres scripts
    public void SetTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
    }
}


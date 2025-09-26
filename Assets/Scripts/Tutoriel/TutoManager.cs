using PFAS.Cam;
using PFAS.Map;
using PFAS.Timer;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [System.Serializable]
    public struct Step
    {
        public string titre;
        [TextArea] public string texte;
    }

    [Header("Contenu")]
    [SerializeField] private List<Step> steps = new List<Step>();

    [Header("UI")]
    [SerializeField] private TutoPopupView popupPrefab; // ton modèle
    [SerializeField] private Transform uiParent;            // Canvas/Panel où instancier
    [SerializeField] private bool pauseGameDuringTutorial = true;

    private int _index = -1;
    private TutoPopupView _popupInstance;

    TimerManager _timerManager;
    CameraController _cameraController;

    private void Start()
    {
        if (steps.Count == 0 || popupPrefab == null || uiParent == null) return;

        _timerManager = GetComponent<TimerManager>();
        _cameraController = GetComponent<MapInputManager>()._cameraController;

        _popupInstance = Instantiate(popupPrefab, uiParent);
        _popupInstance.Hide();

        _timerManager.SetTimeScale(0);
        _cameraController.enabled = false;
        _index = 0;
        uiParent.gameObject.SetActive(true);

        ShowCurrent();
    }

    private void ShowCurrent()
    {
        var isLast = _index >= steps.Count - 1;

        var step = steps[_index];
        _popupInstance.Show(
            step.titre,
            step.texte,
            onConfirm: () =>
            {
                _index++;
                if (_index >= steps.Count) EndTutorial();
                else ShowCurrent();
            },
            buttonLabel: isLast ? "Terminer" : "Compris !"
        );
    }

    private void EndTutorial()
    {
        Destroy(_popupInstance.gameObject);
        _popupInstance.Hide();
        uiParent.gameObject.SetActive(false);
        _cameraController.enabled = true;
        if (pauseGameDuringTutorial) _timerManager.SetTimeScale(1);
    }
}

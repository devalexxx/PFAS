using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputActionReference _pauseInput;
    [SerializeField] private UIPausePanel         _pausePanel;

    void Awake()
    {
        _pauseInput.action.performed += _ => _pausePanel.Toggle();
    }

    void OnEnable()
    {
        _pauseInput.action.Enable();
    }

    void OnDisable()
    {
        _pauseInput.action.Disable();
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PFAS.Cam
{
    public class CameraController : MonoBehaviour
    {

        [Header("Zoom Settings")]
        public float zoomSpeed = 1f;
        public float maxZoom = 1f;
        public float maxUnzoom = 15f;

        [Header("Pan Settings")]
        public float PanSpeed = 5f;
        //thickness of the screen edge in pixels to trigger the pan
        public float edgeThickness = 20f;

        [Header("Pan Boundaries")]
        public Vector2 panLimitMin;             // Limite minimale (x, y) du déplacement en world space
        public Vector2 panLimitMax;             // Limite maximale (x, y) du déplacement en world space

        private Camera _cam;

        [Header("Input Action References")]
        [SerializeField] private InputActionReference _panAction;
        [SerializeField] private InputActionReference _panAxisAction;
        [SerializeField] private InputActionReference _zoomAction;
        [SerializeField] private InputActionReference _mousePosAction;

        private Vector2 _panAxis;
        private Vector2 _mousePos;
        private Vector3 _newPos;
        private bool _isDragging = false;


        private void Awake()
        {
            _cam = GetComponent<Camera>();
            _panAction.action.started += ctx => _isDragging = true;
            _panAction.action.canceled += ctx => _isDragging = false;
        }

        void Update()
        {
            // Get the scroll
            float t_scroll = _zoomAction.action.ReadValue<float>();
            if ( t_scroll != 0)
            {
                HandleZoom(t_scroll);
            }

            _panAxis = _panAxisAction.action.ReadValue<Vector2>();
            _mousePos = _mousePosAction.action.ReadValue<Vector2>();

            Debug.Log("Pan Axis: " + _panAxis);
            Debug.Log("Mouse Pos: " + _mousePos);

            if (_isDragging)
            {
                _newPos = transform.position + new Vector3(-_panAxis.x, -_panAxis.y, 0) * PanSpeed * Time.deltaTime;
                
                // Application des limites de déplacement sur la caméra
                _newPos.x = Mathf.Clamp(_newPos.x, panLimitMin.x, panLimitMax.x);
                _newPos.y = Mathf.Clamp(_newPos.y, panLimitMin.y, panLimitMax.y);
                _newPos.z = transform.position.z; // Conserver la position en Z

                transform.position = _newPos;
            }
            else
            {
                Vector3 t_move = Vector3.zero;

                if (_mousePos.x < edgeThickness)
                {
                    t_move.x -= PanSpeed * Time.deltaTime;
                }
                if (_mousePos.x > Screen.width - edgeThickness)
                {
                    t_move.x += PanSpeed * Time.deltaTime;
                }
                if (_mousePos.y < edgeThickness)
                {
                    t_move.y -= PanSpeed * Time.deltaTime;
                }
                if (_mousePos.y > Screen.height - edgeThickness)
                {
                    t_move.y += PanSpeed * Time.deltaTime;
                }

                _newPos = transform.position + t_move;

                // Application des limites de déplacement sur la caméra
                _newPos.x = Mathf.Clamp(_newPos.x, panLimitMin.x, panLimitMax.x);
                _newPos.y = Mathf.Clamp(_newPos.y, panLimitMin.y, panLimitMax.y);
                _newPos.z = transform.position.z; // Conserver la position en Z

                transform.position = _newPos;
            }
        }

        // Gestion du zoom via la molette de la souris
        void HandleZoom(float p_scroll)
        {
            float t_newSize = _cam.orthographicSize - p_scroll * zoomSpeed;
            _cam.orthographicSize = Mathf.Clamp(t_newSize, maxZoom, maxUnzoom);
        }
    }
}
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
        public float panSpeed = 0.02f;
        //thickness of the screen edge in pixels to trigger the pan
        public float edgeThickness = 20f;

        [Header("Pan Boundaries")]
        //coordinates of the boundaries in world space
        public Vector2 panLimitMin;
        public Vector2 panLimitMax;

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

            _panAction.action.started += ctx => _Drag();
            _panAction.action.canceled += ctx => _Undrag();
        }

        void Update()
        {
            // Get the scroll
            float t_scroll = _zoomAction.action.ReadValue<float>();
            if ( t_scroll != 0)
            {
                _HandleZoom(t_scroll);
            }

            _panAxis = _panAxisAction.action.ReadValue<Vector2>();
            _mousePos = _mousePosAction.action.ReadValue<Vector2>();

            if (_isDragging)
            {
                //get new pos of camera
                _newPos = transform.position + new Vector3(-_panAxis.x, -_panAxis.y, 0) * panSpeed;

                //Apply pan limits to the camera
                _newPos.x = Mathf.Clamp(_newPos.x, panLimitMin.x, panLimitMax.x);
                _newPos.y = Mathf.Clamp(_newPos.y, panLimitMin.y, panLimitMax.y);
                _newPos.z = transform.position.z;

                //apply new position to camera
                transform.position = _newPos;
            }
            else
            {
                Vector3 t_move = Vector3.zero;

                //Handle the camera pan when the mouse is at the edge of the screen
                if (_mousePos.x < edgeThickness)
                {
                    t_move.x -= panSpeed;
                }
                if (_mousePos.x > Screen.width - edgeThickness)
                {
                    t_move.x += panSpeed;
                }
                if (_mousePos.y < edgeThickness)
                {
                    t_move.y -= panSpeed;
                }
                if (_mousePos.y > Screen.height - edgeThickness)
                {
                    t_move.y += panSpeed;
                }

                // Get new position of the camera
                _newPos = transform.position + t_move;

                // Apply pan limits to the camera
                _newPos.x = Mathf.Clamp(_newPos.x, panLimitMin.x, panLimitMax.x);
                _newPos.y = Mathf.Clamp(_newPos.y, panLimitMin.y, panLimitMax.y);
                _newPos.z = transform.position.z;

                // Apply new position to camera
                transform.position = _newPos;
            }
        }

        // Handle the zoom of the camera
        private void _HandleZoom(float p_scroll)
        {
            float t_newSize = _cam.orthographicSize - p_scroll * zoomSpeed;
            _cam.orthographicSize = Mathf.Clamp(t_newSize, maxZoom, maxUnzoom);
        }

        private void _Drag()
        {
            //if the mouse is over a UI element, we do nothing
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) { return; }

            _isDragging = true;
        }
        private void _Undrag()
        {
            _isDragging = false;
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace PFAS.Cam
{
    public class CameraController : MonoBehaviour
    {

        [Header("Zoom Settings")]
        [SerializeField] private float _zoomSpeed = 3f;
        [SerializeField] private float _maxZoom = 1f;
        [SerializeField] private float _maxUnzoom = 15f;

        [Header("Pan Settings")]
        [SerializeField] private float _panDragSpeed = 100f;
        [SerializeField] private float _panEdgeSpeed = 20f;
        //thickness of the screen edge in pixels to trigger the pan
        [SerializeField] private float _edgeThickness = 15f;

        [Header("Pan Boundaries (World Coordinates)")]
        //coordinates of the boundaries in world space
        [SerializeField] private Vector2 _panLimitMin;
        [SerializeField] private Vector2 _panLimitMax;


        private Camera _cam;

        [Header("Input Action References")]
        [SerializeField] private InputActionReference _panAction;
        [SerializeField] private InputActionReference _zoomAction;
        [SerializeField] private InputActionReference _mousePosAction;

        private bool _isDragging = false;
        private Vector3 _lastDragWorldPos;


        private void Awake()
        {
            _cam = GetComponent<Camera>();

            _panAction.action.started += ctx => _StartDrag();
            _panAction.action.canceled += ctx => _EndDrag();
        }

        void Update()
        {
            //Zooming
            float t_scroll = _zoomAction.action.ReadValue<float>();
            if (t_scroll != 0)
            {
                _HandleZoom(t_scroll);
            }

            //Panning
            if (_isDragging)
            {
                _HandleDrag();
            }
            else
            {
                _HandleEdgePan();
            }
        }

        #region Drag and Pan

        private void _StartDrag()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            _isDragging = true;
            float t_dragDistance = -_cam.transform.position.z;
            _lastDragWorldPos = _cam.ScreenToWorldPoint(new Vector3(_mousePosAction.action.ReadValue<Vector2>().x, _mousePosAction.action.ReadValue<Vector2>().y, t_dragDistance));
        }

        private void _EndDrag()
        {
            _isDragging = false;
        }

        // Use World Space to compute delta of the drag
        private void _HandleDrag()
        {
            float t_dragDistance = -_cam.transform.position.z;
            Vector3 t_currentWorldPos = _cam.ScreenToWorldPoint(new Vector3(_mousePosAction.action.ReadValue<Vector2>().x, _mousePosAction.action.ReadValue<Vector2>().y, t_dragDistance));
            Vector3 t_dragDelta = t_currentWorldPos - _lastDragWorldPos;
            // To simulate a "grab" of the scene, camera move in opposite direction of the mouse
            Vector3 t_newPos = transform.position - t_dragDelta * _panDragSpeed * Time.deltaTime;

            t_newPos = _ClampCameraPosition(t_newPos);
            transform.position = t_newPos;
            _lastDragWorldPos = t_currentWorldPos;
        }

        // Panning by screen borders
        private void _HandleEdgePan()
        {
            Vector3 t_move = Vector3.zero;
            Vector2 t_mousePos = _mousePosAction.action.ReadValue<Vector2>();

            if (t_mousePos.x < _edgeThickness)
            {
                t_move.x -= _panEdgeSpeed * Time.deltaTime;
            }
            if (t_mousePos.x > Screen.width - _edgeThickness)
            {
                t_move.x += _panEdgeSpeed * Time.deltaTime;
            }
            if (t_mousePos.y < _edgeThickness)
            {
                t_move.y -= _panEdgeSpeed * Time.deltaTime;
            }
            if (t_mousePos.y > Screen.height - _edgeThickness)
            {
                t_move.y += _panEdgeSpeed * Time.deltaTime;
            }

            Vector3 t_newPos = transform.position + t_move;
            t_newPos = _ClampCameraPosition(t_newPos);
            transform.position = t_newPos;
        }

        // Clamp the camera position to the pan limits
        private Vector3 _ClampCameraPosition(Vector3 p_pos)
        {
            // For an orthographic camera, we can calculate the visible extents in world space
            float t_halfHeight = _cam.orthographicSize;
            float t_halfWidth = t_halfHeight * _cam.aspect;


            // We put the camera's position within the limits

            p_pos.x = Mathf.Clamp(p_pos.x, _panLimitMin.x + t_halfWidth, _panLimitMax.x - t_halfWidth);
            p_pos.y = Mathf.Clamp(p_pos.y, _panLimitMin.y + t_halfHeight, _panLimitMax.y - t_halfHeight);
            p_pos.z = transform.position.z; // We don't want to change the z position
            return p_pos;
        }

        #endregion

        #region Zoom

        private void _HandleZoom(float p_scroll)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            float t_newSize = _cam.orthographicSize - p_scroll * _zoomSpeed;
            _cam.orthographicSize = Mathf.Clamp(t_newSize, _maxZoom, _maxUnzoom);

            // We also need to clamp the camera position after zooming
            transform.position = _ClampCameraPosition(transform.position);
        }

        #endregion

        {
            //if the mouse is over a UI element, we do nothing
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) { return; }

        }

        {
        }
    }
}
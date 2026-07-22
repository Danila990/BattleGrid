using UnityEngine;

namespace BattleGridGame
{
    public class Mouse3D : MonoBehaviour
    {
        [SerializeField] private LayerMask _rayLayermask;
        
        private Vector3 _lastPos;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public Vector3 GetMouseClickPos()
        {
            Vector3 pos = Input.mousePosition;
            pos.z = _camera.nearClipPlane;
            Ray ray = _camera.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _rayLayermask))
                _lastPos = hit.point;

            return _lastPos;
        }
    }
}
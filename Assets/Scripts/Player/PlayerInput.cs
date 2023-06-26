using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask _fireLayerMask;
    private PlayerController _controller;
    // Для определения позиции мыши
    private Ray _ray;
    private RaycastHit _hit;
    private float _distance = 100f;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            _controller.Fire(_hit.point);
        }
    }
    void CastRay()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit, _distance, _fireLayerMask);
    }
}

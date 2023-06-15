using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask _fireLayerMask;
    private PlayerController _controller;
    //определение позиции мыши
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CastRay();
            Debug.Log(hit.point);
            _controller.Fire(hit.point);
        }
    }
    void CastRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 100, _fireLayerMask);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HidingEnemy : MonoBehaviour
{
    [SerializeField] private bool _hasHideoutPoint;
    [SerializeField] private Transform _hideoutPoint;
    [SerializeField] private Transform _positionPoint;
    [SerializeField] private float _hidingSpeed = 1;
    [SerializeField] private float _hidingTime = 2;
    [SerializeField] private float _showingTime = 3;

    private float _currentHidingTime;
    private float _currentShowingTime;
    private bool _behindHideout = false;

    private void Awake()
    {
        _currentShowingTime = _showingTime * 1f;
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    protected virtual void OnFixedUpdate()
    {
        if (_hasHideoutPoint)
        {
            if (_behindHideout)
            {
                transform.position = Vector3.MoveTowards(transform.position, _hideoutPoint.position, Time.fixedDeltaTime * _hidingSpeed);
                _currentHidingTime -= Time.fixedDeltaTime;
                if (_currentHidingTime < 0)
                    Show();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _positionPoint.position, Time.fixedDeltaTime * _hidingSpeed);
                _currentShowingTime -= Time.fixedDeltaTime;
                if (_currentShowingTime < 0)
                    Hide();
            }
        }
    }

    protected virtual void Hide()
    {
        _behindHideout = true;
        _currentHidingTime = _hidingTime * Random.Range(0.5f,1.5f);
    }
    protected virtual void Show()
    {
        _behindHideout = false;
        _currentShowingTime = _showingTime * Random.Range(0.5f, 1.5f);
    }
}

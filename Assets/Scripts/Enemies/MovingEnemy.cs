using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Damageable))]
public class MovingEnemy : SimpleEnemy
{
    [SerializeField] private float _hidingSpeed = 1;
    [SerializeField] private float _waitTime = 2;
    //Временная мера, в идеале отдельный аниматор для мобов
    [SerializeField] private bool _hasAnimation = false;
    [SerializeField] private Animator _animator;

    private float _currentTimeInPosition1;
    private float _currentTimeInPosition2;
    private bool _in2position = false;

    private void Awake()
    {
        _currentTimeInPosition1 = _waitTime * 1f;
    }

    protected override void OnFixedUpdate()
    {
        if (_hasSupportPoint)
        {
            if (_in2position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _supportPoint.position, Time.fixedDeltaTime * _hidingSpeed);
                _currentTimeInPosition2 -= Time.fixedDeltaTime;
                if (_currentTimeInPosition2 < 0)
                    MovingPosition2();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _positionPoint.position, Time.fixedDeltaTime * _hidingSpeed);
                _currentTimeInPosition1 -= Time.fixedDeltaTime;
                if (_currentTimeInPosition1 < 0)
                    MovingPosition1();
            }
        }
    }

    protected virtual void MovingPosition1()
    {
        _in2position = true;
        _currentTimeInPosition2 = _waitTime * Random.Range(0.5f,1.5f);
        if (_hasAnimation)
            _animator.SetTrigger("Jump");
    }
    protected virtual void MovingPosition2()
    {
        _in2position = false;
        _currentTimeInPosition1 = _waitTime * Random.Range(0.5f, 1.5f);
        if (_hasAnimation)
            _animator.SetTrigger("Jump");
    }
}

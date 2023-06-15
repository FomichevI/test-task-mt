using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _hideTimeAfterFire = 0.5f;
    private Animator _animator;
    private float _currentTimeToHide;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Look()
    {
        _animator.SetBool("Fire", true);
        _animator.SetBool("WithHideout", true);
        _currentTimeToHide = _hideTimeAfterFire * 1f;
    }
    public void FireWithMove()
    {
        _animator.SetBool("Fire", true);
        _animator.SetBool("WithHideout", false);
        _currentTimeToHide = _hideTimeAfterFire * 1f;
    }
    public void Idle()
    {
        _animator.SetBool("Fire", false);
        _animator.SetBool("WithHideout", false);
    }

    private void FixedUpdate()
    {
        if(_currentTimeToHide > 0)
        {
            _currentTimeToHide -= Time.fixedDeltaTime;
        }
        if (_currentTimeToHide < 0)
            _animator.SetBool("Fire", false);

    }

}

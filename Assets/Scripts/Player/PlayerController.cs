using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Transform NextHideoutPoint;
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private GameObject _weaponGo;
    private PlayerAnimator _playerAnimator;
    private IWeapon _weapon;
    private bool _isRunning = false;    

    private void Awake()
    {
        _weapon = _weaponGo.GetComponent<IWeapon>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {        
        if (_isRunning)
        {
            Debug.Log("run");
            transform.position = Vector3.MoveTowards(transform.position, NextHideoutPoint.position, _moveSpeed * Time.fixedDeltaTime);
        }
        if (Vector3.Magnitude(transform.position - NextHideoutPoint.position) == 0 && _isRunning)
        {
            StopRunning();
        }
    }

    public void Fire(Vector3 target)
    {
        if (_isRunning)
        {
            _weapon.Fire((target- _weaponGo.transform.position).normalized);
            _playerAnimator.FireWithMove();
        }
        else
        {
            _weapon.Fire((target - _weaponGo.transform.position).normalized);
            //Разворачиваем персонажа в зависимости от того, с какой стороны укрытия нужно стрелять и запускаем соответствующую анимацию
            if (target.x - transform.position.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); //Разворачиваем в нужную сторону саму модель
                _playerAnimator.Look();
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); //Разворачиваем в нужную сторону саму модель
                _playerAnimator.Look();
            }
        }
    }

    private void StopRunning()
    {
        GameManager.Instance.SetNewPointOfView();
        _isRunning = false;
    }
    public void StartRunning(Transform hideout)
    {
        transform.localScale = new Vector3(1, 1, 1); //разворачиваем в нужную сторону саму модель
        NextHideoutPoint = hideout;
        _isRunning = true;
    }
}

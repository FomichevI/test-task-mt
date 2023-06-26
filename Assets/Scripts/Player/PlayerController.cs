using UnityEngine;

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
            _weapon.Fire((target - _weaponGo.transform.position).normalized);
            _playerAnimator.FireWithMove();
        }
        else
        {
            _weapon.Fire((target - _weaponGo.transform.position).normalized);
            //Разворачиваем персонажа в зависимости от того, с какой стороны укрытия нужно стрелять и запускаем соответствующую анимацию
            if (target.x - transform.position.x < 0)
            {
                //Разворачиваем в нужную сторону саму модель
                transform.localScale = new Vector3(-1, 1, 1); 
                _playerAnimator.Look();
            }
            else
            {
                //Разворачиваем в нужную сторону саму модель
                transform.localScale = new Vector3(1, 1, 1); 
                _playerAnimator.Look();
            }
        }
    }

    private void StopRunning()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        CustomEventSystem.PlayerOnNewPoint.Invoke();
        _isRunning = false;
    }
    public void StartRunning(Transform hideout)
    {
        //Разворачиваем в нужную сторону саму модель
        transform.localScale = new Vector3(1, 1, 1); 
        transform.LookAt(hideout);
        transform.rotation *= Quaternion.FromToRotation(Vector3.right, Vector3.forward);
        NextHideoutPoint = hideout;
        _isRunning = true;
        _playerAnimator.Idle();
    }
}

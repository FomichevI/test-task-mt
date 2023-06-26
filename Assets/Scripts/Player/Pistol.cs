using UnityEngine;

public interface IWeapon
{
    public void Fire(Vector3 direction)
    { }
}

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    void IWeapon.Fire(Vector3 direction)
    {
        Bullet bullet = Instantiate<Bullet>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.SetDirection(direction);
    }
}

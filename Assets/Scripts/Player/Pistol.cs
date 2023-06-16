using UnityEngine;

public interface IWeapon
{
    public void Fire(Vector3 direction)
    { }
}

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    void IWeapon.Fire(Vector3 direction)
    {
        GameObject bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}

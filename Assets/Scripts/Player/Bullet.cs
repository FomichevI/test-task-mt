using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    private Vector3 _direction;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + _direction, Time.fixedDeltaTime * _moveSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Damageable>())
        {
            collision.gameObject.GetComponentInParent<Damageable>().GetDamage();
        }
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    private Vector3 _direction;
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, Time.fixedDeltaTime* _moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}

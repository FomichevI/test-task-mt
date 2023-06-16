using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 1;

    public void GetDamage()
    {
        _healthPoints--;
        if (_healthPoints == 0)
            Death();
    }
    private void Death()
    {
        if (gameObject.GetComponent<SimpleEnemy>() != null)
        {
            CustomEventSystem.DethEnemy.Invoke(gameObject.GetComponent<SimpleEnemy>());
            Destroy(gameObject);
        }
    }
}

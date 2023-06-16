using UnityEngine;

//Точки спавна, которые будут срабатывать во время перемещения персонажа
public class SpawnPointWithTrigger : SpawnPoint
{
    [SerializeField] private float _lifeTime = 10;
    private float _currentLifeTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerController>())
        {
            StartSpawn();
            _currentLifeTime = _lifeTime * 1f;
        }
    }

    private void FixedUpdate()
    {
        if (_currentLifeTime > 0)
        {
            _currentLifeTime -= Time.fixedDeltaTime;
            if (_currentLifeTime <= 0 && CurrentEnemy != null)
                Destroy(CurrentEnemy.gameObject);
        }
    }
}

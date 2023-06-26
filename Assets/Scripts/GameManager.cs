using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FollowDynamicCamera _followCamera;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MobCounter[] _mobCounters;
    [SerializeField] private Transform _finalPoint;

    private MobCounter _currentMobCounter;
    private int _currentMobCounterIndex = 0;
    private bool _isLevelCompleted;

    private void OnEnable()
    {
        CustomEventSystem.DeathEnemy.AddListener(EnemyDeath);
        CustomEventSystem.PlayerOnNewPoint.AddListener(PlayerOnNewPoint);
    }
    private void OnDisable()
    {
        CustomEventSystem.DeathEnemy.RemoveListener(EnemyDeath);
        CustomEventSystem.PlayerOnNewPoint.RemoveListener(PlayerOnNewPoint);
    }
    private void Start()
    {
        RunToNewPoint();
    }

    private void EnemyDeath(SimpleEnemy enemy)
    {
        int commpletedPoints = 0;
        //Находим и убераем убитого врага
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints) 
        {
            if (!sp.IsCompleted)
            {
                if (enemy == sp.CurrentEnemy)
                {
                    sp.EnemyDeath();
                    break;
                }
            }
        }
        //Проверяем условие прохождение этапа уровня
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints) 
        {
            if (sp.IsCompleted)
            {
                commpletedPoints++;
            }
        }
        if (commpletedPoints == _currentMobCounter.SpawnPoints.Length)
        {
            RunToNewPoint();
        }
    }

    private void RunToNewPoint()
    {
        if (_currentMobCounterIndex != _mobCounters.Length - 1)
        {
            if (_currentMobCounter == null)
            {
                _currentMobCounter = _mobCounters[0];
            }
            else
            {
                _currentMobCounterIndex += 1;
                _currentMobCounter = _mobCounters[_currentMobCounterIndex];
            }
            _followCamera.StartFallow();
            _playerController.StartRunning(_currentMobCounter.HideoutPoint);
        }
        else
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        _followCamera.StartFallow();
        _playerController.StartRunning(_finalPoint);
        _isLevelCompleted = true;
    }

    private void StartSpawnEnemies()
    {
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints)
        {
            sp.StartSpawn();
        }
    }

    //Событие, небходимое для переключения камеры с игрока на новую точку
    public void PlayerOnNewPoint() 
    {
        if (!_isLevelCompleted)
        {
            _followCamera.StopFallow(_currentMobCounter.PointOfCameraPosition);
            StartSpawnEnemies();
        }
    }
}

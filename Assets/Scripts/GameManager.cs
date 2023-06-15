using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private FollowDynamicCamera _followCamera;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MobCounter[] _mobCounters;

    private MobCounter _currentMobCounter;
    private int _currentMobCounterIndex = 0;

    private void OnEnable()
    {
        CustomEventSystem.DethEnemy.AddListener(EnemyDeath);
    }
    private void OnDisable()
    {
        CustomEventSystem.DethEnemy.RemoveListener(EnemyDeath);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        RunToNewPoint();
    }

    private void EnemyDeath(HidingEnemy enemy)
    {
        int commpletedPoints = 0;
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints) //Ќаходим и убераем убитого врага
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
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints) //ѕровер€ем условие прохождение этапа уровн€
        {
            if (sp.IsCompleted)
            {
                commpletedPoints++;
            }
        }
        if(commpletedPoints == _currentMobCounter.SpawnPoints.Length) 
        {
            RunToNewPoint();
        }
    }

    private void RunToNewPoint()
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

    private void StartSpawnEnemies()
    {
        foreach (SpawnPoint sp in _currentMobCounter.SpawnPoints)
        {
            sp.StartSpawn();
        }
    }

    public void PlayerOnNewPoint() //—обытие, когда игрок добегает до укрыти€
    {
        _followCamera.StopFallow(_currentMobCounter.PointOfCameraPosition); 
        StartSpawnEnemies();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RunToNewPoint();
        }
    }

}

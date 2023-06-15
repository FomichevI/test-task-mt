using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private FollowDynamicCamera _followCamera;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MobCounter[] _mobCounters;

    private MobCounter _currentMobCounter;
    private int _currentMobCounterIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        RunToNewPoint();
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

    public void SetNewPointOfView()
    {
        _followCamera.StopFallow(_currentMobCounter.PointOfCameraPosition);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RunToNewPoint();
        }
    }

}

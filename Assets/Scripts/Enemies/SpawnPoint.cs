using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType {Simpe, Hiding, Jumping}

public class SpawnPoint : MonoBehaviour
{
    public SimpleEnemy CurrentEnemy;
    public bool IsCompleted { get { return _isCompleted; } }
    private bool _isCompleted = false;
    [SerializeField] private Transform _mainPoint;
    [SerializeField] private Transform _supportPoint;
    [SerializeField] private EnemyType[] _enemies;
    [SerializeField] private float _spawnDelay = 2;
    private int _currrentEnemyIndex = 0;

    public void StartSpawn()
    {
        _isCompleted = false;
        _currrentEnemyIndex = 0;
        StartCoroutine(SpawnNewEnemy());
    }
    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(_spawnDelay);
        GameObject enemy = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Enemies/" + GetCurrentEnemyName()), _mainPoint.position, _mainPoint.rotation);
        CurrentEnemy = enemy.GetComponent<SimpleEnemy>();
        CurrentEnemy.SetPoints(_mainPoint, _supportPoint);
        _currrentEnemyIndex++;
    }

    private string GetCurrentEnemyName()
    {
        string enemyType = "";
        switch (_enemies[_currrentEnemyIndex])
        {
            case EnemyType.Simpe:
                enemyType = "SimpleEnemy";
                break;
            case EnemyType.Hiding:
                enemyType = "HidingEnemy";
                break;
            case EnemyType.Jumping:
                enemyType = "JumpingEnemy";
                break;
        }
        return enemyType;
    }

    private void CompletePoint()
    {
        _isCompleted = true;
        Debug.Log(name + " завершен!");
    }
    public void EnemyDeath()
    {
        if (_currrentEnemyIndex == _enemies.Length)
        { CompletePoint(); }
        else
        { StartCoroutine(SpawnNewEnemy()); }
    }
}

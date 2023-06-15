using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType {Simpe, Hiding}

public class SpawnPoint : MonoBehaviour
{
    public Transform HideoutPoint;
    public HidingEnemy CurrentEnemy;
    public bool IsCompleted { get { return _isCompleted; } }
    private bool _isCompleted = false;
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
        _currrentEnemyIndex++;
        GameObject enemy = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/HidingEnemy"), transform.position, transform.rotation);
        CurrentEnemy = enemy.GetComponent<HidingEnemy>();
        CurrentEnemy.SetPoints(transform, HideoutPoint);
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

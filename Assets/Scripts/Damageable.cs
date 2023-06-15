using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 1;
    private static int s_bulletLayerIndex = 6;


    public void GetDamage()
    {
        _healthPoints --;
        if( _healthPoints == 0 )
            Death();
    }
    private void Death()
    {
        if(gameObject.GetComponent<HidingEnemy>() != null)
        {
            CustomEventSystem.DethEnemy.Invoke(gameObject.GetComponent<HidingEnemy>());
            Destroy(gameObject);
        }
    }

}

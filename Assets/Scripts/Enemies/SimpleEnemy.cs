using UnityEngine;

//Просто противник, стоящий на месте. Основа для будущих противников
public class SimpleEnemy : MonoBehaviour
{
    protected bool _hasSupportPoint;
    protected Transform _supportPoint;
    protected Transform _positionPoint;

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    public void SetPoints(Transform mainPos, Transform supportPos)
    {
        _supportPoint = supportPos;
        _positionPoint = mainPos;
        _hasSupportPoint = true;
    }
    public void SetPoints(Transform mainPos)
    {
        _hasSupportPoint = false;
    }

    protected virtual void OnFixedUpdate()
    {
       
    }
}

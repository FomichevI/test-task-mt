using UnityEngine;

//Экземпляры класса содержат информацию обо всех "точках остановки", куда будет перемещаться камера, какие точки спавна мобов будут задействованы, точку самой остановки персонажа
public class MobCounter : MonoBehaviour
{
    public Transform PointOfCameraPosition;
    public Transform HideoutPoint;
    public SpawnPoint[] SpawnPoints;
}

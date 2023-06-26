using UnityEngine;
using UnityEngine.Events;

public class EventWithSimpleEnemy : UnityEvent<SimpleEnemy> { }
public class CustomEventSystem : MonoBehaviour
{
    public static EventWithSimpleEnemy DeathEnemy = new EventWithSimpleEnemy();
    public static UnityEvent PlayerOnNewPoint = new UnityEvent();

}

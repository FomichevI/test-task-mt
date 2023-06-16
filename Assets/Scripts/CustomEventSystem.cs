using UnityEngine;
using UnityEngine.Events;

public class EventWithSimpleEnemy : UnityEvent<SimpleEnemy> { }
public class CustomEventSystem : MonoBehaviour
{
    public static EventWithSimpleEnemy DethEnemy = new EventWithSimpleEnemy();

}

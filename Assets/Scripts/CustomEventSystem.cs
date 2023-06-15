using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithHidingEnemy : UnityEvent<HidingEnemy> { }
public class CustomEventSystem : MonoBehaviour
{
    public static EventWithHidingEnemy DethEnemy = new EventWithHidingEnemy();

}

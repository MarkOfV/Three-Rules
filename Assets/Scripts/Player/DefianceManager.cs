using System;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DefianceManager : MonoBehaviour
{
    public int revivesLeft = 3;
    public UnityEvent onPlayerRevive;
    public UnityEvent onOutOfRevives;

    private void Awake()
    {
        onPlayerRevive ??= new UnityEvent();
        onOutOfRevives ??= new UnityEvent();
    }

    public bool TryRevive(PlayerHealth player)
    {
        if (revivesLeft > 0)
        {
            revivesLeft--;
            player.Revive();          // do the state change here
            onPlayerRevive?.Invoke(); // then notify world
            return true;
        }

        onOutOfRevives?.Invoke();
        return false;
    }
}


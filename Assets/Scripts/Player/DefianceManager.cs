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
            player.Revive();          
            onPlayerRevive?.Invoke(); 
            return true;
        }

        onOutOfRevives?.Invoke();
        return false;
    }
}


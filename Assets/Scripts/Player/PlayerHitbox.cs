using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHitbox : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private readonly Dictionary<Collider2D, float> _lastTick = new ();

        private void Awake()
        {
            _playerHealth = GetComponentInParent<PlayerHealth>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {   
            if (!other.enabled) return;
            var src = other.GetComponent<DamageSource>();
            if (src == null) return;

            var now = Time.time;
            
            if (_lastTick.TryGetValue(other, out var t) && !(now - t >= src.tickRate)) return;
            
            _lastTick[other] = now;

            _playerHealth.ApplyIncomingDamage(src.damage, src.type, src.knockback);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _lastTick.Remove(other);
        }
    }
}
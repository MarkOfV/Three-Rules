using Player;
using UnityEngine;

namespace Rules
{
    public class Rule_PlayerKeepMoving : RuleBase
    {
        public float minSpeed = 0.1f, grace = 0.8f, tick = 0.8f;
        public int damage = 1;

        public Rigidbody2D rb; public PlayerHealth ph;
        float still, timer;

        void Awake(){ rb = FindFirstObjectByType<Rigidbody2D>(); ph = FindFirstObjectByType<PlayerHealth>(); }
        void OnDisable() { still = 0f; timer = 0f; }

        void Update()
        {
            if (!enabled || !rb || !ph) return;
            if (rb.linearVelocity.magnitude < minSpeed)
            {
                still += Time.deltaTime;
                if (still >= grace)
                {
                    timer += Time.deltaTime;
                    if (timer >= tick)
                    {
                        timer = 0f;
                        ph.ApplyIncomingDamage(damage, DamageType.Hazard, Vector2.zero);
                    }
                }
            }
            else { still = 0f; timer = 0f; }
        }
    }
}
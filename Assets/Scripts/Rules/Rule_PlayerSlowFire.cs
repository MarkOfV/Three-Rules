using Player;
using UnityEngine;

namespace Rules
{
    public class Rule_PlayerSlowFire : RuleBase
    {
        public float multiplier = 1.6f;

        private PlayerShooting shooting;
        float baseCd; bool init;

        void Awake()
        {
            if (!shooting) shooting = FindFirstObjectByType<PlayerShooting>();
            if (shooting) { baseCd = shooting.fireCooldown; init = true; }
        }
        
        void OnEnable() =>  SetCooldown();
        void OnDisable() => ResetCooldown();

        void SetCooldown() { if (init && shooting) shooting.fireCooldown = baseCd * multiplier; }
        void ResetCooldown() { if (init && shooting) shooting.fireCooldown = baseCd; }
    }
}

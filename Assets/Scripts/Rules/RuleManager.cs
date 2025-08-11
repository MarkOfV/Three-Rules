using UnityEngine;
using System.Collections.Generic;

namespace Rules
{
 public class RuleManager : MonoBehaviour
    {
        [SerializeField] private List<RuleBase> allRules = new();
        [SerializeField] private int rulesPerRoom = 3;

        private readonly List<RuleBase> active = new();
        public IReadOnlyList<RuleBase> ActiveRules => active;

        void Awake()
        {
            foreach (var r in allRules) if (r) r.enabled = false;
            active.Clear();
        }

        public void ApplyRulesForRoom(int seed)
        {
            ClearActive();
            if (allRules.Count == 0) return;

            var pool = new List<RuleBase>(allRules);
            int picks = Mathf.Min(rulesPerRoom, pool.Count);
            var rand = new System.Random(seed);

            for (int i = 0; i < picks; i++)
            {
                int idx = rand.Next(pool.Count);
                var rule = pool[idx];
                pool.RemoveAt(idx);
                if (!rule) continue;
                rule.gameObject.SetActive(true);
                rule.enabled = true;
                active.Add(rule);
            }
        }

        public void ClearActive()
        {
            for (int i = 0; i < active.Count; i++)
                if (active[i]) active[i].enabled = false;
            active.Clear();
        }
    }
}
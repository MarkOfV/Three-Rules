
using UnityEngine;

namespace Rules
{
    public class Rule_LightHazard : RuleBase
    {
        public GameObject hazardsParent;
        void OnEnable() { if (hazardsParent) hazardsParent.SetActive(true); }
        void OnDisable() { if (hazardsParent) hazardsParent.SetActive(false); }
    }
}
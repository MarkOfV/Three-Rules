using UnityEngine;

namespace Rules
{
    public enum RuleTarget { Player, Enemy, Environment }

    public abstract class RuleBase : MonoBehaviour
    {
        [Header("Rule Meta")]
        [SerializeField] private RuleTarget target;
        [TextArea] [SerializeField] private string shortDescription;
        
        public RuleTarget Target => target;
        public string ShortDescription => shortDescription;
    }
}
using UnityEngine;
using TMPro;
using Rules; // Required for TextMeshPro

namespace UI
{
    public class RulesUI : MonoBehaviour
    {
        public RuleManager ruleManager;
        private TMP_Text[] labels; // will be auto-assigned to first 3 TMP_Text children


        private void Awake()
        {
            // Auto-assign first 3 TMP_Text components in children
            var foundLabels = GetComponentsInChildren<TMPro.TMP_Text>(true);
            if (foundLabels.Length < 3)
            {
                Debug.LogError($"[RulesUI] Could not find 3 TMP_Text components in children of {gameObject.name}.");
                enabled = false;
                return;
            }
            labels = new TMPro.TMP_Text[3];
            for (int i = 0; i < 3; i++) labels[i] = foundLabels[i];
        }

         void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            for (int i = 0; i < labels.Length; i++)
            {
                if (!labels[i]) continue;
                if (i < ruleManager.ActiveRules.Count)
                    labels[i].text = ruleManager.ActiveRules[i].ShortDescription;
                else
                    labels[i].text = "";
            }
        }
    }
}
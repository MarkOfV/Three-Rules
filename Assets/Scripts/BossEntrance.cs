using UnityEngine;
using Rules;

public class BossEntrance : MonoBehaviour
{
    public RuleManager ruleManager;
    public Collider2D bossExitDoor;   // the room’s exitTrigger collider GameObject
    public DialoguePopup popup;

    bool fired;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fired || !other.CompareTag("Player")) return;
        fired = true;

        ruleManager.ClearActive();              // rules stop applying
        if (bossExitDoor) bossExitDoor.gameObject.SetActive(true);
        if (popup) popup.Show("...you’re f-ing kidding me...");
    }
}

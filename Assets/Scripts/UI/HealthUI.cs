using UnityEngine;
using TMPro;
using Player;

public class HealthUI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private TMP_Text label;

    void Awake()
    {
        if (!playerHealth) playerHealth = FindFirstObjectByType<PlayerHealth>();
        if (!label) label = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        if (playerHealth && label)
            label.text = $"Health: {playerHealth.Hp}";
    }
}

using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
{
    public float iFramesAfterRevive = 1.2f;
    public float knockbackScale = 1f;

    private bool _invulnerable;
    private DefianceManager _defiance;
    private Rigidbody2D _rigidBody;

    private new void Awake()
    {
        base.Awake();
        _defiance = FindFirstObjectByType<DefianceManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ApplyIncomingDamage(int amount, DamageType type, Vector2 knock)
    {
        if (_invulnerable || IsDead) return;
        if (_rigidBody != null && knock != Vector2.zero)
            _rigidBody.AddForce(knock * knockbackScale, ForceMode2D.Impulse);
        TakeDamage(amount);
    }

    protected override void Die()
    {
        if (_defiance != null && _defiance.TryRevive(this)) return;

        // No revives left: don't reload here — let RoomController/Director decide.
        // (Optionally expose a UnityEvent onFinalDeath if you want.)
        // onFinalDeath?.Invoke();
    }

    // now public so Defiance can call it
    public void Revive()
    {
        IsDead = false;
        hp = maxHp;
        StartCoroutine(IFrames());
    }

    private IEnumerator IFrames()
    {
        _invulnerable = true;
        yield return new WaitForSeconds(iFramesAfterRevive);
        _invulnerable = false;
    }
}
}

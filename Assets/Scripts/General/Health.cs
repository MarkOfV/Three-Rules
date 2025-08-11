using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHp = 3;
    public int MaxHp => maxHp;

    [SerializeField] protected int hp;
    public int Hp => hp;

    protected bool IsDead { get; set; }
    
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    protected virtual void Awake()
    {
        hp = maxHp;
    }

    public virtual void TakeDamage(int dmg)
    {
        if (IsDead) return;
        hp -= Mathf.Max(1, dmg);
        onDamaged?.Invoke();

        if (hp > 0) return;
        
        IsDead = true;
        Die();
    }

    public virtual void Heal(int amount)
    {
        if (IsDead) return;
        hp = Mathf.Min(maxHp, hp + Mathf.Max(1, amount));
    }

    protected virtual void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
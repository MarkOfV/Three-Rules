using UnityEngine;

public enum DamageType { Contact, Projectile, Hazard, Fire, Light }

public class DamageSource : MonoBehaviour
{
    public int damage = 1;
    public float tickRate = 0.4f; 
    public DamageType type = DamageType.Contact;
    public Vector2 knockback = Vector2.zero; 
}
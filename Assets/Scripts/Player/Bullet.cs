using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1;
    private Rigidbody2D _rigidBody;
    private float _timer;

    private void Awake() { _rigidBody = GetComponent<Rigidbody2D>(); }

    public void Fire(Vector2 direction)
    {
        _rigidBody.linearVelocity = direction.normalized * speed;
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("PlayerHitbox")) return;
        
        var hp = other.GetComponent<Health>();
        if (hp != null) hp.TakeDamage(damage);
        
        //need to change later to specific layers
        Destroy(gameObject);
    }
}
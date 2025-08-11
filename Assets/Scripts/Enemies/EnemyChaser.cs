using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D _rigidBody;
    private Transform _player;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) _player = p.transform;
    }

    private void FixedUpdate()
    {
        if (!_player) return;
        Vector2 toPlayer = _player.position - transform.position;
        Vector2 dir = toPlayer.normalized;
     
        _rigidBody.MovePosition(_rigidBody.position + dir * (moveSpeed * Time.fixedDeltaTime));
    }
}

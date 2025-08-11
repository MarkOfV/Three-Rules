using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 8f;

        private Rigidbody2D _rb;
        private Vector2 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
            _input = _input.sqrMagnitude > 1f ? _input.normalized : _input;
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _input * moveSpeed;
        }
    }
}

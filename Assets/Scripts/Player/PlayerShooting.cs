using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        public float fireCooldown = 0.3f;
        private float _coolDown;

        private Camera _cam;

        private void Awake() { _cam = Camera.main; }

        private void Update()
        {
            _coolDown -= Time.deltaTime;

            // Aim toward mouse (rotate player or just firePoint direction)
            var mouse = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDir = (mouse - firePoint.position);
            var angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (!Input.GetMouseButton(0) || !(_coolDown <= 0f)) return;
        
            Fire(aimDir);
            _coolDown = fireCooldown;
        }

        private void Fire(Vector2 dir)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        
            bullet.Fire(dir);
        }
    }
}
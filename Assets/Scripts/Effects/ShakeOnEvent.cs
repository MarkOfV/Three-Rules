using UnityEngine;

namespace Effects
{
    
    public class ShakeOnEvent : MonoBehaviour
    {
        public CameraShake shaker;
        public float intensity = 0.15f;

        void Awake()
        {
            if (!shaker) shaker = GetComponent<CameraShake>();
        }

        // Hook this in the Inspector
        public void TriggerShake()
        {
            if (shaker) shaker.Shake(intensity);
        }
    }
}
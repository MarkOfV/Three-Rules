using UnityEngine;

namespace Effects
{
    public class CameraShake : MonoBehaviour
    {
        Vector3 orig; void Awake() { orig = transform.localPosition; }
        public void Shake(float amp = 0.1f, float dur = 0.1f) { StartCoroutine(S(amp, dur)); }
        System.Collections.IEnumerator S(float a, float d)
        {
            float t = 0; while (t < d) { t += Time.deltaTime; transform.localPosition = orig + Random.insideUnitSphere * a; yield return null; }
            transform.localPosition = orig;
        }
    }
}
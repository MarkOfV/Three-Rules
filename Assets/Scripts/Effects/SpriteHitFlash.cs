using UnityEngine;

namespace Effects
{
    public class SpriteHitFlash : MonoBehaviour
    {
        public SpriteRenderer sr; public float t = 0.08f;
        Color baseC; void Awake() { if (!sr) sr = GetComponent<SpriteRenderer>(); baseC = sr.color; }
        public void Flash() => StartCoroutine(F());
        System.Collections.IEnumerator F() { sr.color = Color.white; yield return new WaitForSeconds(t); sr.color = baseC; }
    }
}
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] AudioClip explosionSFX;
    
    private void OnEnable()
    {
        StartCoroutine(ShrinkScale());
    }

    IEnumerator ShrinkScale()
    {
        AudioManager.instance.PlaySfx(explosionSFX, 1);
        //float startScale = 0.3f;
        float startScale = transform.localScale.x;
        float scale;
        float timer = 0;
        while(timer < duration)
        {
            float t = timer / duration;
            scale = Mathf.Lerp(startScale, 0, curve.Evaluate(t));
            transform.localScale = Vector3.one * scale;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
}

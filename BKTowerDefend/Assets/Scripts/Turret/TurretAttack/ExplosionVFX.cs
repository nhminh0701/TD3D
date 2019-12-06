using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    [SerializeField] float duration = 2f;
    public AnimationCurve scaleCurve = default;
    float vFXRadius;

    public void ActivateVFX(float radius)
    {
        transform.localScale = Vector3.zero;
        vFXRadius = radius;
        Explore();
        
    }

    private void Explore()
    {
        StartCoroutine(ExploreCoroutine());
    }

    IEnumerator ExploreCoroutine()
    {
        transform.localScale = Vector3.zero;
        float timer = 0;
        float scale = 0;

        while (timer < duration)
        {
            scale = vFXRadius * scaleCurve.Evaluate(timer/duration);
            transform.localScale = Vector3.one * scale;
            yield return new WaitForSeconds(Time.deltaTime);
            timer += Time.deltaTime;
        }

        SimplePool.Despawn(gameObject);
    }
}

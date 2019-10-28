using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] AnimationCurve animationCurve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color backgroundColor = backgroundImage.color;

        float alpha = 1;
        float timer = fadeDuration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            alpha = animationCurve.Evaluate(timer);
            backgroundColor.a = alpha;
            backgroundImage.color = backgroundColor;

            yield return null;
        }
    }
    
    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {
        Color backgroundColor = backgroundImage.color;

        float alpha = 0;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            alpha = animationCurve.Evaluate(timer);
            backgroundColor.a = alpha;
            backgroundImage.color = backgroundColor;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

}

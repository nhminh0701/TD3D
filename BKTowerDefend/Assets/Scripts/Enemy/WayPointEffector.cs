using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEffector : MonoBehaviour
{
   
    [Header("Light Parameters")]
    [SerializeField] Light glowLight;
    [SerializeField] float lightIntensity;
    [Space(10)]
    [SerializeField] ParticleSystem glowParticles;
    

    public void ObjectStateChange()
    {
        StartCoroutine(ObjectStateChangeCoroutine());
    }

    IEnumerator ObjectStateChangeCoroutine()
    {
        glowParticles.Play();

        float effectDur = glowParticles.main.duration;

        while (glowLight.intensity < lightIntensity)
        {
            glowLight.intensity += lightIntensity * Time.deltaTime / effectDur;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        while (glowLight.intensity > 0)
        {
            glowLight.intensity -= lightIntensity * Time.deltaTime / effectDur;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}

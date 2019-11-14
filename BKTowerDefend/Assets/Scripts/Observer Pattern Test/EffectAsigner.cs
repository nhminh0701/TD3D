using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAsigner : MonoBehaviour
{
    [SerializeField] string effectDescription;

    public void AsignEffect(EffectAsginee target)
    {
        target.OnReceiveEffect(effectDescription + " on" + gameObject.name);
    }
}

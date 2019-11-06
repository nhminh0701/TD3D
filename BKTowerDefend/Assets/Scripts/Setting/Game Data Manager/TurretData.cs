using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData 
{
    public string className;

    public int reachableLv;

    public TurretData(string _className, int _reachableLv)
    {
        className = _className;
        reachableLv = _reachableLv;
    }
}

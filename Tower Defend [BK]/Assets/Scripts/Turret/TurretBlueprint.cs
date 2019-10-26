using UnityEngine;

[CreateAssetMenu(fileName = "Turret Blueprint")]
public class TurretBlueprint : ScriptableObject
{
    public GameObject prefab;
    public GameObject buildEffect;
    public int cost;

    public GameObject bulletPrefab { get
        {
            if (prefab)
            {
                return prefab.GetComponent<Turret>().bulletPrefab;
            }
            else return null;
        }
    }

    public GameObject bulletImpactEffect
    {
        get
        {
            if (bulletPrefab)
            {
                return bulletPrefab.GetComponent<Bullet>().impactEffect;
            }
            else return null;
        }
    }

    #region update turret
    public TurretBlueprint upgradedTurret = null;

    public GameObject upgradedPrefab
    {
        get
        {
            return upgradedTurret.prefab;
        }
    }

    public GameObject ugradedBuildEffect
    {
        get
        {
            return upgradedTurret.buildEffect;
        }
    }

    public int upgradeCost
    {
        get
        {
            return upgradedTurret.cost;
        }
    }
    #endregion
}

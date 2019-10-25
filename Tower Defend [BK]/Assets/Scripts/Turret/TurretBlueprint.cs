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
}

using UnityEngine;

[CreateAssetMenu(fileName = "Turret Blueprint", menuName = "Turret/Turret Blueprint")]
public class TurretBlueprint : ScriptableObject
{
    public int level;
    public GameObject prefab;
    public GameObject buildEffect;
    public int cost;
    public int price;

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

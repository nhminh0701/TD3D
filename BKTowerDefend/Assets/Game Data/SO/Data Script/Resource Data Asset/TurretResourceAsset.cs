using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Resources", menuName = "Data/Resource Data Asset/Resource Turret Asset")]
public class TurretResourceAsset : ItemResource
{
    public List<Sprite> listTurretsAvatar;

    public List<GameObject> listTurretPrefabs;

    public List<GameObject> listTurretBuildEffect;
}

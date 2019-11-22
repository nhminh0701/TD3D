using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Resources", menuName = "Data/Resource Data Asset/Resource Data Asset Controller")]
public class ResourceDataAsset : ScriptableObject
{
    public List<TurretResourceAsset> listTurretResourceAsset;

    public List<DBHResourceAsset> listDBHResourceAssets;

    public Dictionary<string, Sprite> otherIConDictionary;
}

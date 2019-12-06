using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletArtFactory", menuName = "Factory/DBH Factory/Bullet DBH Factory")]
public class BulletArtFactory : DBHVFXFactory
{
    [SerializeField] Color colorEffect;

    public void UpdateBullet(MeshRenderer meshRenderer)
    {
        Material mat = meshRenderer.material;

        mat.color = colorEffect;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", colorEffect*4);
    }
}

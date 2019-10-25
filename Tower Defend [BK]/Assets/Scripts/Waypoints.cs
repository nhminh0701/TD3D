using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] wayPoints;

    // The list reset every new scene
    private void Awake()
    {
        wayPoints = new Transform[transform.childCount];

        for(var index = 0; index < wayPoints.Length; index ++)
        {
            wayPoints[index] = transform.GetChild(index);
        }
    }
}

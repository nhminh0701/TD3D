using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] Text livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.lives.ToString() + " Lives";
    }
}

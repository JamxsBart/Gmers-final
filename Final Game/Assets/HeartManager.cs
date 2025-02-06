using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public int heartCount;
    public Text heartText;

    void Update()
    {
        if (heartText != null)
        {
            heartText.text = heartCount.ToString();
        }
    }
}

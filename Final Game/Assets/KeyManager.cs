using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int keyCount;
    public Text keyText;
    public GameObject door;
    private bool hasKeyDisplayed = false;

    void Update()
    {
        if (keyText != null && keyCount > 0 && !hasKeyDisplayed)
        {
            keyText.text = "Room Key Acquired!";
            hasKeyDisplayed = true;
            Invoke("ClearText", 3f);
        }

        if (keyCount >= 1 && door != null)
        {
            Destroy(door);
            door = null; // Prevents repeated attempts to destroy it
        }
    }

    void ClearText()
    {
        if (keyText != null)
        {
            keyText.text = "";
        }
    }
}

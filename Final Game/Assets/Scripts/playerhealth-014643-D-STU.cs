using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthbar;
    private CameraShake cameraShake;

    void Start()
    {
        maxHealth = health;
        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            StartCoroutine(HandleDeath());
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(0.15f, 0.1f));
        }
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
        }
    }


    IEnumerator HandleDeath()
    {
        if (cameraShake != null)
        {
            yield return StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
        }
        RestartGame();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

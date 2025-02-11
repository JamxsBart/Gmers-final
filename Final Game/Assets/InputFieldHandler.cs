using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class InputFieldHandler : MonoBehaviour
{
    // Reference to the TMP_InputField
    public TMP_InputField inputField;

    // Reference to the enemy
    public Enemy targetEnemy;

    // Amount of damage the player should deal
    public float damageAmount = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Check if inputField is assigned, and add listener
        if (inputField != null)
        {
            inputField.onEndEdit.AddListener(HandleInputSubmit);
        }
    }

    // This method is called when the user presses Enter or submits the input field
    void HandleInputSubmit(string userInput)
    {
        if (!string.IsNullOrEmpty(userInput))
        {
            Debug.Log("User entered: " + userInput);
            // Call your function or script logic here based on the user input
            TriggerScriptLogic(userInput);
        }
    }

    // A method that executes your logic based on user input
    void TriggerScriptLogic(string input)
    {
        // Example of what to do with the input
        if (input.ToLower() == "attack") // If user input is "attack"
        {
            Debug.Log("Correct input! Player attacks the enemy.");
            DealDamageToEnemy(); // Deal damage to the enemy
        }
        else
        {
            Debug.Log("Input not recognized: " + input);
        }
    }

    // Method to deal damage to the enemy
    void DealDamageToEnemy()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damageAmount); // Deal damage to the enemy
        }
        else
        {
            Debug.LogWarning("Target enemy not assigned!");
        }
    }
}

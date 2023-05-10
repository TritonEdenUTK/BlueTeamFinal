using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this script is used to end the game and return to the main menu after dropping out of the end scene
public class FinishGame : MonoBehaviour
{
    // Check for collision, on collision call CompleteGame()
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CompleteGame();
        }
    }

    // Function to move the Player to the main menu
    private void CompleteGame()
    {
        SceneManager.LoadScene(0); // Goes back to the main menu
    }
}

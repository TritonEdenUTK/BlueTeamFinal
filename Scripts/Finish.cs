using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this is the script used by the doors at the end of each level
public class Finish : MonoBehaviour
{
    // Check for collision, on collision call CompleteLeve()
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CompleteLevel();
        }
    }

    // Function to move the Player to the next scene
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Goes to the next scene
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Detection zone is used for the knight enemy and it finds what colliders it's detecting and add it to it's detected colliders list
// This is used to trigger the attack event and allow for the knight to "see" the player
public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}

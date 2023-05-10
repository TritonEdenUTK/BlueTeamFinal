using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a bullet at a certain point when an event is triggered
public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;

        // flip the projectile's facing direction based on when the player fires
        projectile.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? 1: -1,
            originalScale.y,
            originalScale.z
            );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform FollowTarget;

    // Starting position for parallax game object
    Vector2 startingPosition;
    float startingZ;

    Vector2 cameraMoveSinceStart => (Vector2) cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - FollowTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When the target moves, move the parallax object the same distance (with multiplier)
        Vector2 newPosition = startingPosition + cameraMoveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed * parallax factor, but Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}

//https://www.youtube.com/watch?v=tMXgLBwtsvI

using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;
    float startingZ;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;
        float zDistanceFromTarget = transform.position.z - followTarget.position.z;
        float clippingPlane = (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
        float parallaxFactor = Mathf.Abs(zDistanceFromTarget) / clippingPlane;

        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}

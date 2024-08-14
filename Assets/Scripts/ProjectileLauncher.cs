using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    public void FirePrejectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 scale = projectile.transform.localScale;
        scale.x *= Mathf.Sign(transform.localScale.x);
        projectile.transform.localScale = scale;
    }
}

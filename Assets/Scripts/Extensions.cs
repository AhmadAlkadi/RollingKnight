
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) {
        if(rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.2f;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        Debug.Log("rigidbody.position " + rigidbody.position);
        Debug.Log("radius " + radius);
        Debug.Log("distance " + distance);
        Debug.Log("hit " + hit.collider != null && hit.rigidbody != rigidbody);

        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection){
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized,testDirection) > 0.25f;
    }
}

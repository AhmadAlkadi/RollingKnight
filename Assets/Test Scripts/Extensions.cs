using UnityEngine;

public static class Extensions
{
    private static Vector2 offset;

    private static LayerMask layerMask = LayerMask.GetMask("Ground");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, float radius, float distance, float offset_x, float offset_y)
    {
        if (rigidbody.bodyType == RigidbodyType2D.Kinematic)
        {
            return false;
        }

        offset.x = offset_x;
        offset.y = offset_y;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position + offset, radius, direction.normalized, distance, layerMask);


        //Debug.Log("rigidbody.position: " + rigidbody.position);
        //Debug.Log("direction: " + direction);
        //Debug.Log("radius: " + radius);
        //Debug.Log("distance: " + distance);
        //Debug.Log("hit: " + (hit.collider != null && hit.rigidbody != rigidbody).ToString());


        return (hit.collider != null && hit.rigidbody != rigidbody);
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
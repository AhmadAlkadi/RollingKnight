using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    public Transform player;

    private void LateUpdate() {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x,player.position.x);
        transform.position = cameraPosition;
    }
}

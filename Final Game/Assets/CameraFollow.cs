using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private float fixedZPosition;
    void Start()
    {
        fixedZPosition = transform.position.z;

        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, fixedZPosition);
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, fixedZPosition);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }
    }
}
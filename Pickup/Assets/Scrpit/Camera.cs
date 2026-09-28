using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 200f;
    public float rotationSmoothSpeed = 10f;

    [Header("Camera")]
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;

    [Header("Camera Angle Limit")]
    public float minVerticalAngle = -10f;
    public float maxVerticalAngle = 30f;

    private float xRotation = 20f;
    private float targetYRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
            targetYRotation = player.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") *
                       mouseSensitivity *
                       Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") *
                       mouseSensitivity *
                       Time.deltaTime;

        // Camera up/down
        xRotation -= mouseY;

        // Vertical limit
        xRotation = Mathf.Clamp(
            xRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        // Player rotation
        targetYRotation += mouseX;

        Quaternion targetRotation =
            Quaternion.Euler(0f, targetYRotation, 0f);

        player.rotation = Quaternion.Slerp(
            player.rotation,
            targetRotation,
            rotationSmoothSpeed * Time.deltaTime
        );

        // Camera rotation
        transform.rotation =
            Quaternion.Euler(
                xRotation,
                targetYRotation,
                0f
            );

        // Camera follow
        transform.position =
            player.position
            - player.forward * cameraDistance
            + Vector3.up * cameraHeight;
    }
}
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header ("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    public float yRotation = 0f;

    [Header ("Rotation Clamps")]
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    
    void Start()
    {
        // ----------- Lock and Hide Cursor -----------
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ----------- Mouse Inputs -----------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ----------- Look Up and Down -----------
        xRotation -= mouseY;

        // ----------- Clamp Rotation -----------
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // ----------- Look Left and Right -----------
        yRotation += mouseX;

        // ----------- Rotate Player Body -----------
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject cam;

    public bool firstPerson = true;
    public bool cameraLocked = true;

    public float mouseSensitivity = 100f;

    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private float _firstPersonCamRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        cameraMovement();
    }

    void cameraMovement()
    {
        // For determining what direction to rotate the character if
        // in third person and camera not locked
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // For use of the first person camera
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        GameObject fpsCam = cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject;

        // If first person, rotate player's model regardless of if moving
        // Then do first person look around
        if (firstPerson)
        {
            _firstPersonCamRotation -= mouseY;
            _firstPersonCamRotation = Mathf.Clamp(_firstPersonCamRotation, -70f, 70f);

            fpsCam.transform.localRotation = Quaternion.Euler(_firstPersonCamRotation, 0f, 0f);
            transform.Find("Graphics/Hands").gameObject.transform.localRotation = Quaternion.Euler(_firstPersonCamRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        // If camera is locked, rotate player's model regardless of if moving
        if (cameraLocked)
        {
            turnPlayerThirdPerson(direction);
        }

        // If wasd movement
        if (direction.magnitude >= 0.1f)
        {
            // Third person camera
            if (firstPerson == false)
            {
                turnPlayerThirdPerson(direction);
            }
        }
    }

    // Control player turning in third person
    void turnPlayerThirdPerson(Vector3 direction)
    {
        float targetAngle = 0f;
        float currentAngle = 0f;
        if (cameraLocked == false)
            // If camera isn't locked, then moving left and right will rotate the character
            // Get the direction of movement as an angle from 0 - 2 pi
            // in radians
            targetAngle = Mathf.Atan2(direction.x, direction.z);
        // Convert radians to degrees
        targetAngle = targetAngle * Mathf.Rad2Deg;
        // Add the offset of the camera's angle so that it follows the camera
        targetAngle = targetAngle + cam.transform.eulerAngles.y;
        // Smooth the camera movement
        currentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        // Rotate the physical character model
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
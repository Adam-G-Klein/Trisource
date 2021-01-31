using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerCameraController cameraController;
    public float jumpHeight = 3f;
    public KeyCode cameraToggleKey = KeyCode.R;

    private GroundMovement moveController;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<GroundMovement>();
    }

    void FixedUpdate()
    {
        Vector3 inputs = Vector3.zero;
        float moveAngle = 0f;
        bool firstPerson = cameraController.firstPerson;
        inputs.x = Input.GetAxisRaw("Horizontal");
        inputs.z = Input.GetAxisRaw("Vertical");
        if (!firstPerson)
        {
            moveAngle = cameraController.cam.transform.eulerAngles.y;
            inputs = Quaternion.Euler(0f, moveAngle, 0f) * inputs;
        }
        else
        {
            inputs = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * inputs;
        }
        inputs = inputs.normalized;
        moveController.moveHorizontal(inputs);
    }

    // Update is called once per frame
    void Update()
    {
        checkJump();
        checkCameraToggle();
    }

    void checkJump()
    {
        // Check for a jump
        if (Input.GetButtonDown("Jump") && moveController.isGrounded())
        {
            moveController.moveVertical(Mathf.Sqrt(jumpHeight * 2.0f * moveController.gravity));
        }
    }

    // Move this somewhere else later
    void checkCameraToggle()
    {
        CinemachineFreeLook thirdPersonCam = GetComponentInChildren<CinemachineFreeLook>();
        if (Input.GetKeyDown(cameraToggleKey))
        {
            cameraController.firstPerson = !cameraController.firstPerson;
            thirdPersonCam.enabled = !thirdPersonCam.enabled;
            thirdPersonCam.m_XAxis.Value = transform.eulerAngles.y;
        }
    }
}

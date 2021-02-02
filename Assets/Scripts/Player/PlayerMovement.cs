using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerCameraController cameraController;
    public float jumpHeight = 3f;
    public KeyCode cameraToggleKey = KeyCode.R;

    private GroundMovement _moveController;
    private AdvancedCollisionDetector detector;
    private Vector3 _inputs = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _moveController = GetComponent<GroundMovement>();
        detector = GetComponentInChildren<AdvancedCollisionDetector>();
    }

    void FixedUpdate()
    {
        Vector3 moveDir;
        float moveAngle = 0f;
        bool firstPerson = cameraController.firstPerson;
        if (!firstPerson)
        {
            moveAngle = cameraController.cam.transform.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, moveAngle, 0f) * _inputs;
        }
        else
        {
            moveDir = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * _inputs;
        }
        moveDir = moveDir.normalized;
        moveDir = detector.detectCollision(moveDir);
        _moveController.moveHorizontal(moveDir);
    }

    // Update is called once per frame
    void Update()
    {
        checkJump();
        checkCameraToggle();
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.z = Input.GetAxisRaw("Vertical");
    }

    void checkJump()
    {
        // Check for a jump
        if (Input.GetButtonDown("Jump") && _moveController.isGrounded())
        {
            _moveController.moveVertical(Mathf.Sqrt(jumpHeight * 2.0f * _moveController.gravity));
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

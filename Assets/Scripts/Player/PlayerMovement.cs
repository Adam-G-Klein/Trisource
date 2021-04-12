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
    private ActivateResource activeResource;
    private Vector3 _inputs = Vector3.zero;
    private bool _prevGrounded = false;
    private Vector3 _lastHeight;
    private PlayerInterface _interface;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _moveController = GetComponent<GroundMovement>();
        detector = GetComponentInChildren<AdvancedCollisionDetector>();
        activeResource = GetComponent<ActivateResource>();
        _lastHeight = transform.position;
        _interface = GetComponent<PlayerInterface>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
        checkFallDamage();
        moveDir = moveDir.normalized;
        if (activeResource.getActive() == 1)
            moveDir = detector.detectCollision(moveDir);
        if (moveDir.magnitude > 0 && _moveController.checkApproximatelyGrounded())
        {
            _audioManager.playSteps(_moveController.getSpeed());
        }
        else
        {
            _audioManager.stopSteps();
        }
        _moveController.moveHorizontal(moveDir);
    }

    void checkFallDamage()
    {
        if(_prevGrounded == false && _moveController.isGrounded())
        {
            if (_lastHeight.y - transform.position.y > 20f)
            {
                _interface.killPlayer();
            }
        }
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

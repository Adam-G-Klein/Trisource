using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OldPlayerMovement : MonoBehaviour
{
    public PlayerCameraController cameraController;
    public float speed = 6f;
    public float jumpHeight = 3f;
    public float groundCheckDistance = 0.1f;
    public float gravity = 9.81f;
    public float maxVelocityChange = 10.0f;
    public KeyCode cameraToggleKey = KeyCode.R;

    private Vector3 _velocity = Vector3.zero;
    //private float _controlsDeadzone = 0.1f;
    private bool _grounded;
    private Rigidbody _body;
    private Vector3 _inputs;
    private Vector3 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        checkCameraToggle();
    }

    // Controller for character movement
    void movement()
    {
        RaycastHit hit;
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.z = Input.GetAxisRaw("Vertical");

        // Check if the player is on the ground
        _grounded = _body.SweepTest(Vector3.down, out hit, groundCheckDistance);
        //_grounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        _body.velocity = new Vector3(0f, _body.velocity.y, 0f);

        handleVertical();
    }

    void handleVertical()
    {
        Vector3 jumpVelocity = new Vector3(0, 0, 0);
        // Check for a jump
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            jumpVelocity.y += Mathf.Sqrt(jumpHeight * 2.0f * gravity);
        }
        // Handle gravity
        if (!_grounded)
        {
            jumpVelocity.y -= gravity * Time.deltaTime;
        }
        _body.AddForce(jumpVelocity, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        float moveAngle = 0f;
        bool firstPerson = cameraController.firstPerson;
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.z = Input.GetAxisRaw("Vertical");
        if (!firstPerson)
        {
            moveAngle = cameraController.cam.transform.eulerAngles.y;
            _inputs = Quaternion.Euler(0f, moveAngle, 0f) * _inputs;
        }
        else
        {
            _inputs = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f) * _inputs;
        }
        _inputs = _inputs.normalized;
        // Check for steep slope
        if (_body.SweepTest(_inputs, out hit, groundCheckDistance))
        {
            if (Vector3.Angle(Vector3.up, hit.normal) > 70f)
            {
                _inputs = _inputs - Vector3.Project(_inputs, hit.normal);
                _inputs.y = 0f;
            }
        }
        _body.MovePosition(_body.position + (_inputs * speed * Time.fixedDeltaTime));
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

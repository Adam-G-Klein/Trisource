using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = 9.81f;

    private Rigidbody _body;
    private Vector3 _moveDirection;
    private float _checkSlopeDistance = 0.1f;
    private float _maxSlopeAngle = 70f;
    private bool _grounded;
    private float _groundCheckDistance = 0.1f;
    private Vector3 _verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody of the entity
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        doHorizontal();
        applyGravity();
    }

    public void moveHorizontal(Vector3 moveDirection)
    {
        // Set the direction for the entity to move towards
        _moveDirection = moveDirection;
    }

    private void doHorizontal()
    {
        RaycastHit hit;
        // Check in front of the entity for a collision
        if (_body.SweepTest(_moveDirection, out hit, _checkSlopeDistance))
        {
            // Check for a slope with an angle of steeper than 70 degrees
            if (Vector3.Angle(Vector3.up, hit.normal) > _maxSlopeAngle)
            {
                // Stop the entity from moving up the slope
                _moveDirection = _moveDirection - Vector3.Project(_moveDirection, hit.normal);
                _moveDirection.y = 0f;
            }
        }
        // Move the entity towards the position
        _body.MovePosition(_body.position + (_moveDirection * speed * Time.fixedDeltaTime));
    }

    public void moveVertical(float verticalVelocity)
    {
        Vector3 velocity = new Vector3(0, verticalVelocity, 0);
        _body.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void applyGravity()
    {
        RaycastHit hit;
        // Check if the player is on the ground
        _grounded = _body.SweepTest(Vector3.down, out hit, _groundCheckDistance);
        // Handle gravity
        if (!_grounded)
        {
            _body.AddForce(new Vector3(0, -gravity * Time.deltaTime, 0), ForceMode.VelocityChange);
        }
    }

    public bool isGrounded()
    {
        return _grounded;
    }
}

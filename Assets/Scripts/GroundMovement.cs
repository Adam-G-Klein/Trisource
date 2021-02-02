using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float gravity = 9.81f;

    private float _groundCheckDistance = 0.1f;
    private float _speed;
    private Rigidbody _body;
    private Vector3 _moveDirection;
    private float _maxSlopeAngle = 70f;
    private bool _grounded;
    private bool _normalMovement = true;


    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody of the entity
        _body = GetComponent<Rigidbody>();
        _speed = GetComponent<EntityConst>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    void FixedUpdate()
    {
        if (_normalMovement)
        {   doHorizontal(); }
        applyGravity();
    }

    public void moveHorizontal(Vector3 moveDirection)
    {
        // Set the direction for the entity to move towards
        _moveDirection = moveDirection;
    }

    private void doHorizontal()
    {
        _body.MovePosition(_body.position + (_moveDirection * _speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(Vector3.up, collision.GetContact(0).normal) > _maxSlopeAngle)
        {
            _moveDirection = _moveDirection - Vector3.Project(_moveDirection, collision.GetContact(0).normal);
            _moveDirection.y = 0f;
        }
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

    public float getSpeed()
    {
        return _speed;
    }

    public void setSpeed(float speed)
    {
        _speed = speed;
    }

    public bool isGrounded()
    {
        return _grounded;
    }

    public void disableNormalMovement()
    {
        _normalMovement = false;
    }

    public void enableNormalMovementl()
    {
        _normalMovement = true;
    }

    public IEnumerator resumeNormalMovement()
    {

        for (; _body.velocity.magnitude < 0.1f;)
        {
            yield return new WaitForFixedUpdate();
        }
        _normalMovement = true;
    }
}

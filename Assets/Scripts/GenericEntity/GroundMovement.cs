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
    private Vector3 _gravity;
    private float _maxSlopeAngle = 70f;
    private bool _grounded;
    private bool _normalMovement = true;

    private List<Transform> _downPointsList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody of the entity
        _body = GetComponent<Rigidbody>();
        _speed = GetComponent<EntityConst>().speed;
        setupDownPointsList();
    }

    void setupDownPointsList()
    {
        _downPointsList.Add(transform.Find("CastPoints/Down/d_northwest"));
        _downPointsList.Add(transform.Find("CastPoints/Down/d_northeast"));
        _downPointsList.Add(transform.Find("CastPoints/Down/d_southwest"));
        _downPointsList.Add(transform.Find("CastPoints/Down/d_southeast"));
        _downPointsList.Add(transform.Find("CastPoints/Down/d_center"));
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
        bool prevGrounded = _grounded;
        // Check if the player is on the ground
        _grounded = checkGrounded();
        // Handle gravity
        if (!_grounded)
        {
            if (prevGrounded)
            {
                // reduce the "floaty" feel of the gravity when jumping
                _gravity.y = (-gravity/2) * Time.fixedDeltaTime;
            }
            _gravity.y = _gravity.y + -gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            _body.AddForce(_gravity, ForceMode.VelocityChange);
        }
        else
        {
            _gravity = Vector3.zero;
        }
    }

    private bool checkGrounded()
    {
        RaycastHit hit;
        for (int i = 0; i < _downPointsList.Count; i++)
        {
            if (Physics.Raycast(_downPointsList[i].position, -transform.up, out hit, _groundCheckDistance))
                return true;
        }
        return false;
    }

    public bool checkApproximatelyGrounded()
    {
        RaycastHit hit;
        for (int i = 0; i < _downPointsList.Count; i++)
        {
            if (Physics.Raycast(_downPointsList[i].position, -transform.up, out hit, _groundCheckDistance*3f))
                return true;
        }
        return false;
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

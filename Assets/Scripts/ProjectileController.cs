using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float maxDistance = 200f;
    private float _speed = 10f;
    private Vector3 _moveDirection;
    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    private void FixedUpdate()
    {
        Rigidbody _body = GetComponent<Rigidbody>();
        _moveDirection = transform.TransformDirection(Vector3.forward);
        _body.MovePosition(_body.position + (_moveDirection * _speed * Time.fixedDeltaTime));
        if (Vector3.Distance(transform.position, _startPosition) > maxDistance)
        {
            Object.Destroy(this.gameObject);
        }
        return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(this.gameObject);
    }

    public void setSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void setStartPosition(Vector3 startPosition)
    {
        _startPosition = startPosition;
    }
}

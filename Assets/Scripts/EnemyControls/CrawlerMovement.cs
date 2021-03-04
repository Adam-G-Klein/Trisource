using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerMovement : MonoBehaviour
{
    public bool stayStill = false;
    private GameObject _player;
    private Transform _playerTrans;
    private bool _regularMovement = true;
    private Vector3 _movementDirection;
    private NavMeshAgent _agent;
    private EntityConst _const;
    private Rigidbody _body;
    private GroundMovement _moveController;

    private Vector3 _startFallPoint;
    private float _moveBackTime = 1f;
    private bool _isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
        _agent = GetComponentInChildren<NavMeshAgent>();
        _const = GetComponent<EntityConst>();
        _agent.speed = _const.speed;
        _body = GetComponent<Rigidbody>();
        _moveController = GetComponent<GroundMovement>();
        _moveController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        NavMeshHit hit;
        // If the nav mesh agent is enabled but falls off of the nav mesh
        if (_agent.enabled == true && !_agent.isOnNavMesh && _isFalling == false)
        {
            // disable nav mesh pathing and enable falling
            disablePathing();
            _moveController.enabled = true;
            _startFallPoint = transform.position;
            // give the crawler a small amount of time to actually start falling
            // before the next if statement immediately happens and its magnitude is low
            StartCoroutine(fallingTimer());
        }
        // if the crawler is already falling 
        else if (_isFalling)
        {
            // check if its velocity is 0, meaning it hit the ground
            if (_body.velocity.magnitude < 0.1f)
            {
                // first check if took enough fall height to die
                if (_startFallPoint.y - transform.position.y > 20f)
                {
                    GetComponent<CrawlerInterface>().kill();
                }
                // stop its falling and turn back on pathing
                _isFalling = false;
                enablePathing();
                // put the nav mesh agent back on the nav mesh
                NavMesh.SamplePosition(transform.position, out hit, 0.5f, NavMesh.AllAreas);
                _agent.Warp(hit.position);
            }
        }


        if (!stayStill)
        {
            if (_agent.enabled == true && _agent.isOnNavMesh)
                _agent.isStopped = false;
            moveCrawler();
        }
        else
        {
            if (_agent.enabled == true && _agent.isOnNavMesh)
                _agent.isStopped = true;
        }
    }

    private void moveCrawler()
    {
        if (_regularMovement && _agent.isOnNavMesh)
        {
            _agent.destination = _player.transform.position;
        }
    }

    public void moveBack()
    {
        // Move the crawler away from the player after it does damage
        _movementDirection = -10 * (_playerTrans.position - transform.position).normalized;
        _agent.destination = _movementDirection + transform.position;
        _regularMovement = false;
        StartCoroutine(moveBackTimer());
    }

    public void disableRegularMovement()
    {
        _regularMovement = false;
    }

    public void enableRegularMovement()
    {
        _regularMovement = true;
    }

    public void disablePathing()
    {
        disableRegularMovement();
        _agent.enabled = false;
        _body.isKinematic = false;
        _moveController.enabled = true;
    }

    public void enablePathing()
    {
        enableRegularMovement();
        _agent.enabled = true;
        _body.isKinematic = true;
        _moveController.enabled = false;
    }

    IEnumerator fallingTimer()
    {
        yield return new WaitForSeconds(0.1f);
        _isFalling = true;
    }

    IEnumerator moveBackTimer()
    {
        yield return new WaitForSeconds(_moveBackTime);
        _regularMovement = true;
    }

    public IEnumerator doneBeingPushed()
    {
        yield return new WaitForSeconds(0.1f);
        for (; _body.velocity.magnitude > 2f;)
        {
            yield return new WaitForFixedUpdate();
        }
        gameObject.layer = LayerMask.NameToLayer("Crawlers");
        enablePathing();
    }
}

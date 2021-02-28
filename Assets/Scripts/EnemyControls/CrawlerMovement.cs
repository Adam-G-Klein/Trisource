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
    private BoxCollider _collider;

    private float _moveBackTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
        _agent = GetComponentInChildren<NavMeshAgent>();
        _const = GetComponent<EntityConst>();
        _agent.speed = _const.speed;
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stayStill)
            moveCrawler();
        else
            if (_regularMovement)
                _agent.destination = transform.position;
    }

    private void moveCrawler()
    {
        if (_regularMovement)
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
        _agent.enabled = true;
        enableRegularMovement();
    }
}

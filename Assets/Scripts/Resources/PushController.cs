using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    public float timeToLive = 1f;

    private float _speed = 10f;
    private float _pushForce = 20f;
    private Vector3 _moveDirection;
    private Rigidbody _body;
    private List<GameObject> collisions = new List<GameObject>();
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
        _moveDirection = transform.TransformDirection(Vector3.forward);
        _body.MovePosition(_body.position + (_moveDirection * _speed * Time.fixedDeltaTime));
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool alreadyPushed = false;
        Vector3 pushDirection;

        if (other.gameObject.tag != "CrawlerEnemy")
        {
            return;
        }

        foreach (GameObject obj in collisions)
        {
            if (obj.GetInstanceID() == other.gameObject.GetInstanceID())
            {
                alreadyPushed = true;
            }
        }

        if (!alreadyPushed)
        {
            collisions.Add(other.gameObject);
            pushDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
            other.gameObject.GetComponent<CrawlerInterface>().forcePushBack(pushDirection, _pushForce);
        }
    }

    public void initPush(float speed)
    {
        _speed = speed;
        _body = GetComponent<Rigidbody>();
        StartCoroutine(timeToLiveTimer());
    }

    public void initPush(float speed, float pushForce)
    {
        initPush(speed);
        _pushForce = pushForce;
    }

    IEnumerator timeToLiveTimer()
    {
        yield return new WaitForSeconds(timeToLive);
        Object.Destroy(this.gameObject);
    }
}

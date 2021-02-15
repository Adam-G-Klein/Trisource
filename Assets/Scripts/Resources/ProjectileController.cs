using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float timeToLive = 3f;
    public GameObject effect;

    private float _damage = 25f;
    private float _speed = 10f;
    private Vector3 _moveDirection;
    private Rigidbody _body;

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
        if (other.gameObject.tag == "CrawlerEnemy")
        {
            other.gameObject.GetComponent<CrawlerInterface>().takeDamage(_damage);
        }
        Instantiate(effect, transform.position, new Quaternion(0, 0, 0, 0));
        Object.Destroy(this.gameObject);
    }

    public void initProjectile(float speed)
    {
        _speed = speed;
        _body = GetComponent<Rigidbody>();
        StartCoroutine(timeToLiveTimer());
    }

    public void initProjectile(float speed, float newDamage)
    {
        initProjectile(speed);
        _damage = newDamage;
    }

    IEnumerator timeToLiveTimer()
    {
        yield return new WaitForSeconds(timeToLive);
        Object.Destroy(this.gameObject);
    }
}

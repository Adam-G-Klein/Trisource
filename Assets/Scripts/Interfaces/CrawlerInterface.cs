using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerInterface : MonoBehaviour
{
    private CrawlerHealth _crawlerHealth;
    private Rigidbody _body;
    private CrawlerMovement _movement;
    // Start is called before the first frame update
    void Start()
    {
        _crawlerHealth = GetComponent<CrawlerHealth>();
        _body = GetComponent<Rigidbody>();
        _movement = GetComponent<CrawlerMovement>();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    public void takeDamage(float damage)
    {
        _crawlerHealth.takeDamage(damage);
    }

    public void forcePushBack(Vector3 direction, float force)
    {
        _movement.disablePathing();
        gameObject.layer = LayerMask.NameToLayer("ForcePushing");
        _body.AddForce(direction * force, ForceMode.VelocityChange);
        StartCoroutine(_movement.doneBeingPushed());
    }
    
}

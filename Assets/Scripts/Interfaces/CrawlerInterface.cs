using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerInterface : MonoBehaviour
{
    private CrawlerHealth _crawlerHealth;
    private GroundMovement _crawlerMovement;
    private Rigidbody _body;
    // Start is called before the first frame update
    void Start()
    {
        _crawlerHealth = GetComponent<CrawlerHealth>();
        _crawlerMovement = GetComponent<GroundMovement>();
        _body = GetComponent<Rigidbody>();
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
        _crawlerMovement.disableNormalMovement();
        _body.AddForce(direction * force, ForceMode.VelocityChange);
        StartCoroutine(_crawlerMovement.resumeNormalMovement());
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;

    private bool _active = false;
    private bool _canShoot = true;
    private float _shootWaitTime = 0.25f;
    private float _speed = 30f;
    private float _damage = 25f;

    private Transform hands;
    private Transform hand;
    // Start is called before the first frame update
    void Start()
    {
        hands = transform.Find("Graphics/Hands");
        hand = transform.Find("Graphics/Hands/Right Hand");
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
            checkProjectile();
    }

    void checkProjectile()
    {
        GameObject newProjectile;
        ProjectileController newProjectileController;
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButton(0)) && _canShoot)
        {
            newProjectile = Instantiate(projectile,
                                        hand.position + (hands.TransformDirection(Vector3.forward) * 0.2f),
                                        hands.rotation) as GameObject;
            newProjectileController = newProjectile.GetComponent<ProjectileController>();
            newProjectileController.initProjectile(_speed, _damage);
            _canShoot = false;
            StartCoroutine(shootTimer());
        }
    }

    public void activate()
    {
        _active = true;
    }

    public void deactivate()
    {
        _active = false;
    }

    public void setSpeed(float speed)
    {
        _speed = speed;
    }

    public void setDamage(float damage)
    {
        _damage = damage;
    }

    IEnumerator shootTimer()
    {
        yield return new WaitForSeconds(_shootWaitTime);
        _canShoot = true;
    }
}

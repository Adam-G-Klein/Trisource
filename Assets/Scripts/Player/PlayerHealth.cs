using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _health;
    private Rigidbody _body;
    private EntityConst _entityConst;

    // Start is called before the first frame update
    void Start()
    {
        _entityConst = GetComponent<EntityConst>();
        _health = _entityConst.maxHealth;
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Player Health: " + _health);
        return;
    }

    public void takeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
        {
            killPlayer();
        }
    }

    private void killPlayer()
    {
        _body.position = new Vector3(0f, 3f, 0f);
        _health = _entityConst.maxHealth;
    }
}

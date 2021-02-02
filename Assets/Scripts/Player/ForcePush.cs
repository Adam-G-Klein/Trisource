using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public GameObject forcePush;

    private float _speed;
    private float _pushForce;
    private bool _canPush = true;
    private bool _active = false;
    private float _pushWaitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
            checkPush();
    }

    void checkPush()
    {
        GameObject newPush;
        PushController newPushController;
        GameObject hands;
        
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButton(0)) && _canPush)
        {
            hands = transform.Find("Graphics/Hands").gameObject;
            newPush = Instantiate(forcePush, hands.transform.position + (hands.transform.TransformDirection(Vector3.forward) * 0.2f),
                                  new Quaternion(hands.transform.rotation.x, transform.rotation.y, hands.transform.rotation.z, transform.rotation.w)) as GameObject;
            newPushController = newPush.GetComponent<PushController>();
            newPushController.initPush(_speed, _pushForce);
            _canPush = false;
            StartCoroutine(pushTimer());
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

    public void setForce(float force)
    {
        _pushForce = force;
    }

    IEnumerator pushTimer()
    {
        yield return new WaitForSeconds(_pushWaitTime);
        _canPush = true;
    }
}

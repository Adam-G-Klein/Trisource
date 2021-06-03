using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public GameObject forcePush;
    public Material defaultHands;
    public Material cooldownHands;

    private float _speed;
    private float _pushForce;
    private bool _canPush = true;
    private bool _active = false;
    private float _pushWaitTime = 3f;

    private SkinnedMeshRenderer rightHandRenderer;
    private SkinnedMeshRenderer leftHandRenderer;

    private GameObject hands;

    private AudioManager _audioManager;

    private HandInterface _handInterface;

    // Start is called before the first frame update
    void Start()
    {
        hands = transform.Find("Graphics/Hands").gameObject;
        rightHandRenderer = transform.Find("Graphics/Hands/PlayerRightHand").gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        leftHandRenderer = transform.Find("Graphics/Hands/PlayerLeftHand").gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _handInterface = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HandInterface>();
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
        
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButton(0)) && _canPush)
        {
            newPush = Instantiate(forcePush, hands.transform.position + (hands.transform.TransformDirection(Vector3.forward) * 0.2f),
                                  new Quaternion(hands.transform.rotation.x, transform.rotation.y, hands.transform.rotation.z, transform.rotation.w)) as GameObject;
            newPushController = newPush.GetComponent<PushController>();
            newPushController.initPush(_speed, _pushForce);
            _audioManager.playForcePush();
            _handInterface.uncurlAndCurlRightHandBlue();
            startCooldown();
        }
    }

    void startCooldown()
    {
        _canPush = false;
        setHands(cooldownHands);
        StartCoroutine(pushTimer());
    }

    void endCooldown()
    {
        _canPush = true;
        if (_active)
            setHands(defaultHands);
    }

    void setHands(Material material)
    {
        Material[] mats = rightHandRenderer.materials;
        mats[0] = material;
        rightHandRenderer.materials = mats;
        mats = leftHandRenderer.materials;
        mats[0] = material;
        leftHandRenderer.materials = mats;
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
        endCooldown();
    }
}

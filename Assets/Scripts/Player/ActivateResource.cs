using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateResource : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material untethered;

    private bool _redActive = false;
    private bool _blueActive = false;
    private bool _yellowActive = false;
    private float _resourceDistance = 200f;
    private bool _canTether = true;

    private TetherVisuals tetherVisuals;
    private PlayerCameraController cameraController;
    private ShootProjectile shootProjectile;
    private ForcePush forcePush;
    private Renderer rightHand;
    private Renderer leftHand;
    private GroundMovement movement;
    private PlayerMovement playerMovement;
    private AudioManager _audioManager;
    private float _baseMoveSpeed;
    private float _baseJumpHeight;
    private bool _hover = true;

    private PlayerInterface _playerInterface;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponent<PlayerCameraController>();
        shootProjectile = GetComponent<ShootProjectile>();
        forcePush = GetComponent<ForcePush>();
        rightHand = transform.Find("Graphics/Hands/Right Hand").gameObject.GetComponent<Renderer>();
        leftHand = transform.Find("Graphics/Hands/Left Hand").gameObject.GetComponent<Renderer>();
        tetherVisuals = GameObject.FindGameObjectWithTag("VisualManager").GetComponentInChildren<TetherVisuals>();
        movement = GetComponent<GroundMovement>();
        _baseMoveSpeed = GetComponent<EntityConst>().speed;
        playerMovement = GetComponent<PlayerMovement>();
        _baseJumpHeight = playerMovement.jumpHeight;
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _playerInterface = GetComponent<PlayerInterface>();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        _hover = true;
        bool hitSomething = false;
        RaycastHit hit = new RaycastHit();
        Vector3 tetherPoint;
        ResourceConst resourceConst;

        if (Input.GetMouseButtonDown(1) && _canTether)
        {
            hitSomething = Physics.Raycast(cameraController.cam.transform.position, cameraController.cam.transform.forward, out hit, _resourceDistance);
            _hover = false;
        }
        else
        {
            hitSomething = Physics.Raycast(cameraController.cam.transform.position, cameraController.cam.transform.forward, out hit, _resourceDistance);
        }
        if (hitSomething && !_hover)
        {
            hitSomething = false;
            switch((hit.collider.gameObject.tag))
            {
                case "Red Resource":
                    resourceConst = hit.collider.gameObject.GetComponent<ResourceConst>();
                    activateRed(resourceConst.projectileSpeed, resourceConst.projectileDamage);
                    tetherPoint = getResourceTetherPoint(hit);
                    tetherVisuals.tether(tetherPoint);
                    break;

                case "Blue Resource":
                    resourceConst = hit.collider.gameObject.GetComponent<ResourceConst>();
                    activateBlue(resourceConst.pushSpeed, resourceConst.pushForce);
                    tetherPoint = getResourceTetherPoint(hit);
                    tetherVisuals.tether(tetherPoint);
                    break;

                case "Yellow Resource":
                    resourceConst = hit.collider.gameObject.GetComponent<ResourceConst>();
                    activateYellow(resourceConst.zoomIncrease, resourceConst.jumpIncrease);
                    tetherPoint = getResourceTetherPoint(hit);
                    tetherVisuals.tether(tetherPoint);
                    break;

                case "Checkpoint":
                    tetherPoint = hit.collider.gameObject.transform.position;
                    tetherVisuals.checkpointTether(tetherPoint);
                    tetherVisuals.setCheckpoint();
                    _playerInterface.checkpoint(hit.collider.gameObject.transform.Find("Respawn Point").transform.position);
                    break;
            }
        }
        else if (hitSomething && _hover)
        {
            hitSomething = false;
            switch ((hit.collider.gameObject.tag))
            {
                case "Red Resource":
                case "Blue Resource":
                case "Yellow Resource":
                    hit.collider.gameObject.GetComponent<HoverOver>().setHover();
                    break;
            }
        }
        return;
    }

    private Vector3 getResourceTetherPoint(RaycastHit hit)
    {
        return hit.collider.gameObject.transform.Find("TetherPoint").position;
    }

    void activateRed(float speed, float damage)
    {
        Debug.Log("activated red");
        _redActive = true;

        deactivateBlue();
        deactivateYellow();
        shootProjectile.activate();
        shootProjectile.setDamage(damage);
        shootProjectile.setSpeed(speed);
        setHands(redMaterial);
        tetherVisuals.setRed();
    }

    void activateBlue(float speed, float force)
    {
        Debug.Log("activated blue");
        _blueActive = true;

        deactivateRed();
        deactivateYellow();
        forcePush.activate();
        forcePush.setSpeed(speed);
        forcePush.setForce(force);
        setHands(blueMaterial);
        tetherVisuals.setBlue();
    }

    void activateYellow(float speedIncrease, float jumpIncrease)
    {
        Debug.Log("activated yellow");

        deactivateBlue();
        deactivateRed();
        if (!_yellowActive)
        {
            movement.setSpeed(_baseMoveSpeed * speedIncrease);
            playerMovement.jumpHeight = _baseJumpHeight * jumpIncrease;
            _audioManager.playYellow();
            _audioManager.stopSteps();
            _audioManager.playSteps(_baseMoveSpeed * speedIncrease);
        }
        _yellowActive = true;
        setHands(yellowMaterial);
        tetherVisuals.setYellow();
    }

    void deactivateRed()
    {
        _redActive = false;
        shootProjectile.deactivate();
    }

    void deactivateBlue()
    {
        _blueActive = false;
        forcePush.deactivate();
    }

    void deactivateYellow()
    {
        float speed;
        if (_yellowActive)
        {
            speed = movement.getSpeed();
            movement.setSpeed(_baseMoveSpeed);
            playerMovement.jumpHeight = _baseJumpHeight;
            _audioManager.stopYellow();
            _audioManager.stopSteps();
            _audioManager.playSteps(_baseMoveSpeed);
        }
        _yellowActive = false;
    }

    public int getActive()
    {
        if (_yellowActive)
            return 1;
        else if (_blueActive)
            return 2;
        else if (_redActive)
            return 3;
        return 0;
    }

    public void deactivateAll()
    {
        deactivateBlue();
        deactivateRed();
        deactivateYellow();
        setHands(untethered);
        tetherVisuals.untether();
    }

    void setHands(Material material)
    {
        rightHand.material = material;
        leftHand.material = material;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateResource : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public GameObject tether;

    private bool _redActive = false;
    private bool _blueActive = false;
    private bool _yellowActive = false;
    private float _resourceDistance = 100f;
    //private float _tetherTime = 0.5f;
    private bool _canTether = true;

    private TetherVisuals tetherVisuals;
    private PlayerCameraController cameraController;
    private ShootProjectile shootProjectile;
    private ForcePush forcePush;
    private Renderer rightHand;
    private Renderer leftHand;
    private GroundMovement movement;
    private float _baseMoveSpeed;

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
        return;
    }

    // Update is called once per frame
    void Update()
    {
        bool hitSomething = false;
        RaycastHit hit = new RaycastHit();
        Vector3 tetherPoint;
        ResourceConst resourceConst;

        if (Input.GetMouseButtonDown(1) && _canTether)
        {
            hitSomething = Physics.Raycast(cameraController.cam.transform.position, cameraController.cam.transform.forward, out hit, _resourceDistance);
        }
        if (hitSomething)
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
                    activateYellow(resourceConst.zoomIncrease);
                    tetherPoint = getResourceTetherPoint(hit);
                    tetherVisuals.tether(tetherPoint);
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

    void activateYellow(float increase)
    {
        Debug.Log("activated yellow");

        deactivateBlue();
        deactivateRed();
        if (!_yellowActive)
        {
            movement.setSpeed(_baseMoveSpeed * increase);
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
        }
        _yellowActive = false;
        setHands(yellowMaterial);
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

    void setHands(Material material)
    {
        rightHand.material = material;
        leftHand.material = material;
    }
}
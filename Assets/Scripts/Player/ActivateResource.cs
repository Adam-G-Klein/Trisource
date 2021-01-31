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
    private float _tetherTime = 0.5f;
    private bool _canTether = true;

    private PlayerCameraController cameraController;
    private ShootProjectile shootProjectile;
    private ForcePush forcePush;
    private Renderer rightHand;
    private Renderer leftHand;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponent<PlayerCameraController>();
        shootProjectile = GetComponent<ShootProjectile>();
        forcePush = GetComponent<ForcePush>();
        rightHand = transform.Find("Graphics/Hands/Right Hand").gameObject.GetComponent<Renderer>();
        leftHand = transform.Find("Graphics/Hands/Left Hand").gameObject.GetComponent<Renderer>();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        bool hitSomething = false;
        RaycastHit hit = new RaycastHit();
        GameObject newTether;

        if (Input.GetMouseButtonDown(1) && _canTether)
        {
            // DO THIS LATER
            // newTether = Instantiate(tether, leftHand.transform.position, )
            hitSomething = Physics.Raycast(cameraController.cam.transform.position, cameraController.cam.transform.forward, out hit, _resourceDistance);
        }
        if (hitSomething)
        {
            hitSomething = false;
            switch((hit.collider.gameObject.tag))
            {
                case "Red Resource":
                    activateRed();
                    break;

                case "Blue Resource":
                    activateBlue();
                    break;

                case "Yellow Resource":
                    activateYellow();
                    break;
            }
        }
        return;
    }

    void activateRed()
    {
        Debug.Log("activated red");
        _redActive = true;

        deactivateBlue();
        deactivateYellow();
        shootProjectile.activate();
        setHands(redMaterial);
    }

    void activateBlue()
    {
        Debug.Log("activated blue");
        _blueActive = true;

        deactivateRed();
        deactivateYellow();
        forcePush.activate();
        setHands(blueMaterial);
    }

    void activateYellow()
    {
        Debug.Log("activated yellow");
        _yellowActive = true;

        deactivateBlue();
        deactivateRed();
        // Something here
        setHands(yellowMaterial);
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
        _yellowActive = false;
        //shootProjectile.deactivate();
    }

    void setHands(Material material)
    {
        rightHand.material = material;
        leftHand.material = material;
    }
}


/*
 * IEnumerator TrailPlacer(Transform trail)
    {
        TrailController trailCont = trail.GetComponent<TrailController>();
        Vector2 startPos = player.position;
        Vector3 newAngs, newScale;
        Vector2 newPos;
        // + 90 due to experiment based implementation
        newAngs = new Vector3(0, 0,mover.FindAngle(swiper.lastDir) + 90);
        //this should be interruptable by another trail being formed
        while (LeanTween.isTweening(mover.ltidMov) && !trailCont.isTrailPlaced())
        {

            newPos = (((Vector2)player.position - startPos) / 2) + startPos;
            newScale = new Vector3(Vector2.Distance(startPos, player.position),
                                    trail.localScale.y, trail.localScale.z);
            //print(string.Format("angs: {0} pos: {1} scale: {2}", newAngs, newPos, newScale));
            trail.eulerAngles = newAngs;
            trail.localScale = newScale;

            trail.position = newPos;
            yield return new WaitForEndOfFrame();
        }
        trail.GetComponent<TrailController>().trailPlaced = true;
        trailPlacing = false;
    }
*/
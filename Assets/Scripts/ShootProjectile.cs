using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float speed = 30f;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        checkProjectile();
    }

    void checkProjectile()
    {
        GameObject newProjectile;
        GameObject hand;
        GameObject hands;
        if (Input.GetMouseButtonDown(0))
        {
            hands = transform.Find("Graphics/Hands").gameObject;
            hand = transform.Find("Graphics/Hands/Right Hand").gameObject;
            newProjectile = Instantiate(projectile, 
                                        hand.transform.position + (hands.transform.TransformDirection(Vector3.forward) * 0.2f), 
                                        new Quaternion(hands.transform.rotation.x, transform.rotation.y, hands.transform.rotation.z, transform.rotation.w)) as GameObject;
            newProjectile.GetComponent<ProjectileController>().setSpeed(speed);
            newProjectile.GetComponent<ProjectileController>().setStartPosition(hand.transform.position);
        }
    }
}

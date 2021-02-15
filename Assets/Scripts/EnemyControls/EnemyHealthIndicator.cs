using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthIndicator : MonoBehaviour
{
    public float lineWidth = 0.2f;
    public Gradient backgroundColor;
    public Gradient healthBarColor;
    public Material lineMaterial;

    private LineRenderer backgroundLine;
    private LineRenderer healthBarLine;
    private GameObject background;
    private GameObject healthBar;
    private Transform startPoint;
    private Transform endPoint;
    private CrawlerHealth crawlerHealth;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.Find("StartPoint");
        endPoint = transform.Find("EndPoint");
        background = transform.Find("background").gameObject;
        healthBar = transform.Find("healthBar").gameObject;
        setupBackground();
        setupHealthBar();
        crawlerHealth = GetComponentInParent<CrawlerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void setupBackground()
    {
        backgroundLine = background.AddComponent<LineRenderer>();
        backgroundLine.positionCount = 2;
        backgroundLine.SetPosition(0, startPoint.position);
        backgroundLine.SetPosition(1, endPoint.position);
        backgroundLine.startWidth = lineWidth;
        backgroundLine.endWidth = lineWidth;
        backgroundLine.material = lineMaterial;
        backgroundLine.colorGradient = backgroundColor;
        backgroundLine.sortingOrder = 1;

    }

    void setupHealthBar()
    {
        healthBarLine = healthBar.AddComponent<LineRenderer>();
        healthBarLine.positionCount = 2;
        healthBarLine.SetPosition(0, startPoint.position);
        healthBarLine.SetPosition(1, endPoint.position);
        healthBarLine.startWidth = lineWidth;
        healthBarLine.endWidth = lineWidth;
        healthBarLine.material = lineMaterial;
        healthBarLine.colorGradient = healthBarColor;
        healthBarLine.sortingOrder = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = crawlerHealth.getHealthPercent();
        Vector3 direction;

        backgroundLine.SetPosition(0, startPoint.position);
        backgroundLine.SetPosition(1, endPoint.position);
        healthBarLine.SetPosition(0, startPoint.position);
        direction = endPoint.position - startPoint.position;
        healthBarLine.SetPosition(1, startPoint.position + (direction * percent));
        direction = player.transform.position - transform.position;
        transform.forward = direction.normalized;
    }
}

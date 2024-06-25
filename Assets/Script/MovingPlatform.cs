    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   
    public float speed;
    Vector3 targetPos;

    MovementController movementController;
    Rigidbody2D rb;
    Vector3 moveDirection;

    Rigidbody2D playerrb;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;
    public float waitDuration;
    
    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }
    private void Start()
    {
        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;       
        DirectionCaculate();
    }   
    public void Update()
    {

        if (Vector2.Distance(transform.position, targetPos) < 0.05f) {
            NextPoint();
        }
        

    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void DirectionCaculate() { 
        moveDirection = (targetPos - transform.position).normalized;    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            playerrb.gravityScale = playerrb.gravityScale * 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
           
            playerrb.gravityScale = playerrb.gravityScale / 50;
        }
    }
    void NextPoint() {
        transform.position = targetPos;
        moveDirection = Vector3.zero;
        if (pointIndex == pointCount - 1) { 
            direction=-1;
        }
        if(pointIndex == 0) { 
            direction = 1;
        }
        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }
    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCaculate() ;
    }
}

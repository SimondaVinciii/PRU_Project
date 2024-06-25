using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        checkPointPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    void Die() {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn( float duration) { 
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        transform.localScale = new Vector3(0,0,0);
        //transform.TransformDirection(new Vector3(0,0,0));
        yield return new WaitForSeconds(duration);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1,1,1);
        playerRb.simulated = true;

    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;

    }
}

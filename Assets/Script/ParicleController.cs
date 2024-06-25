using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParicleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] ParticleSystem movementParticle;
    [Range(1, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playreRb;

    float counter;
    bool isOnGround;

    [Header("Fall")]
    [SerializeField] ParticleSystem fallParticle;
    [Header("Touch")]
    [SerializeField] ParticleSystem touchParticle;

    private void Start()
    {
        touchParticle.transform.parent = null;
    }
    private void Update()
    {
        counter += Time.deltaTime;

        if(Mathf.Abs(playreRb.velocity.x) > occurAfterVelocity && isOnGround)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
           
        }
       
    }
    public void PlayTouchParticle(Vector2 pos) { 
        touchParticle.transform.position = pos;
        touchParticle.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
    
}

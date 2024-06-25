
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rd;
    [SerializeField]int speed;
    float speedMultiplier;
    bool btnPressed;
    [Range(1, 10)]
    [SerializeField]
    
    float acceleration;
    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;
    Vector2 relativeTransform;
    public bool isOnPlatform;

    public Rigidbody2D platformRb;

    public ParicleController particleController;
   
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        UpdateRelativeTransfrom();
    }
    private void FixedUpdate()
    {
        UpdateSpeedMultilier();
        float targetSpeed = speed * speedMultiplier * relativeTransform.x;
        if(isOnPlatform)
        {
            rd.velocity = new Vector2(targetSpeed+ platformRb.velocity.x, rd.velocity.y);
        }
        else
        {
            rd.velocity = new Vector2(targetSpeed, rd.velocity.y);
        }
        
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f , 0.4f),0, wallLayer);
        if (isWallTouch)
        {

           Flip();
        }
    }

    public void Flip() {
        particleController.PlayTouchParticle(wallCheckPoint.position);
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransfrom();

    }
    void UpdateRelativeTransfrom()
    {
        relativeTransform = transform.InverseTransformDirection(Vector2.one);
    }
    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
            speedMultiplier = 1;
        } else if (value.canceled)
        {
            btnPressed= false;
            speedMultiplier = 0;
        }
        
    }
    
        
    void UpdateSpeedMultilier()
    {
        if(btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime * acceleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime * acceleration;
            if(speedMultiplier < 0)
            {
                speedMultiplier = 0;
            }
        }

     }
   


}

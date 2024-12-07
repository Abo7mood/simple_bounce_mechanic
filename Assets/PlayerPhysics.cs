using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerPhysics : MonoBehaviour
{
    public float bounceForce = 60f;
    float dotValue;
    float finalVal;
    public float minForce = 120f;
    bool isDamage;
    [Space]
    [Range(0f, 1f)]
    public float dirParam = .14f;
    Rigidbody2D playerRB;
    Animator anim;
    PlayerState state;


    GameObject collidingObject;

    public GameObject collidingGameObject => collidingObject;

 

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        state = GetComponent<PlayerState>();
        anim = GetComponent<Animator>();
    }
    Vector2 finalForceDir;
    public Vector2 forceDir => finalForceDir;
    private void Update()
    {
        state.currentPlayerState = !isColliding ? PlayerCurrentState.InAir : PlayerCurrentState.Bounced;
        float worldDot = Vector2.Dot(Vector2.up, transform.up);
        if (worldDot < 0f)
        {
            finalForceDir = transform.up * -1f;
        }
        else
        {
            finalForceDir = transform.up;
        }

       


    }
    bool isColliding;
    Collision2D curCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        collidingObject = collision.gameObject;
        curCollision = collision;
        //Find dot between player up dir and normal of collision
        dotValue = Vector2.Dot(collision.contacts[0].normal, transform.up);
        //Make it only positive.
        finalVal = Mathf.Abs(dotValue);
        //Create a final magnitude of force to apply.
        float forceMag = finalVal * bounceForce;
        CalculateAndAddForce(forceMag, finalVal);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collidingObject = collision.gameObject;
        isColliding = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collidingObject = null;
        isColliding = false;


    }
 
    private void CalculateAndAddForce(float forceMag, float dotRes)
    {
        if (dotRes <= dirParam)
        {
            playerRB.AddForce(Vector2.up * minForce);
        }
        playerRB.AddForce(forceMag * finalForceDir);
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //Simple Script to rotate the player basing off horizontal input

    public float rotationSpeed = 10f;

    public float boostRotSpeed = 120f;
    public float rotationCooldown = 2f;
    float xInput = 0f;
    float finalRotation = 0f;
    [HideInInspector] public float boostRotation = 0f;
    Rigidbody2D playerRB;

    PlayerState state;

    bool isboosting;
    private void Start()
    {
        state = GetComponent<PlayerState>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        finalRotation = xInput * rotationSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && !isboosting)
        {
            StartCoroutine(RotationCooldown());
            StartCoroutine(BoostCooldown());
        }


        transform.eulerAngles -= new Vector3(0f, 0f, finalRotation + boostRotation);
    }

    IEnumerator RotationCooldown()
    {
        boostRotation = boostRotSpeed * Time.deltaTime;
        state.currentPlayerState = PlayerCurrentState.Spinned;

        yield return new WaitForSeconds(rotationCooldown);
        boostRotation = 0f;
        state.currentPlayerState = PlayerCurrentState.InAir;
    }
    IEnumerator BoostCooldown()
    {
        isboosting = true;
        yield return new WaitForSeconds(rotationCooldown + .2f);
        isboosting = false;
    }
}


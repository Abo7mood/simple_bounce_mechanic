using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBoost : MonoBehaviour
{
    PlayerState state;
    Rigidbody2D playerRB;
    PlayerPhysics playerPhysics;

    public float boostForce = 120f;
    [Space]
    public float boostCooldown = 10f;
    bool boostDone = false;
    bool CanBoost;
    float curBoostCooldown = 0f;
    float boostAmount = 1;
    private void Start()
    {
        state = GetComponent<PlayerState>();
        playerRB = GetComponent<Rigidbody2D>();
        playerPhysics = GetComponent<PlayerPhysics>();
    }
    private void Update()
    {
        CoolDownCheck();
        if (boostAmount >= 1)
        {
            boostAmount = 1;
            CanBoost = true;
        }
        else
        {
            boostAmount += Time.deltaTime * .1f;
            CanBoost = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !boostDone && CanBoost)
        {

            boostAmount = 0;


            curBoostCooldown = boostCooldown;
            boostDone = true;
            //Add more force in the current direction
            playerRB.AddForce(playerPhysics.forceDir * boostForce);
        }
    }
    void CoolDownCheck()
    {
        if (curBoostCooldown >= .1f)
        {
            curBoostCooldown -= Time.deltaTime;
        }
        else
        {
            boostDone = false;
            curBoostCooldown = 0f;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{

    public void Start()
    {
        SetValues();
    }

    protected void FixedUpdate()
    {
        if (MainManager.Instance.gameON)
        {

            if (IfIsMoving())
            {
                
                LookAt(LookForward());

                Move();
            }
            else
            {
                ShootTimer();

                LookAt(FindEnemy());

                if (TimeToShoot())
                {
                    Shoot();
                }
            }

        }
    }

    public void SetValues()
    {
        hero_body = gameObject.GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * -run_speed, 0f, Input.GetAxis("Horizontal") * Time.deltaTime * run_speed, Space.World);
    }


    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "WinWall":
                if (NoEnemyLeft())
                {
                    MainManager.Instance.Win();
                }
                break;
            case "Enemy":
                hp = hp - 5;
                break;
        }
    }
    
    public Vector3 LookForward()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(0.0f, moveHorizontal + moveVertical, 0.0f);

        return movement;
    }

    public override Vector3 FindEnemy()
    {

        Enemie closestEnemy = null;

        float distanceToClosestEnemy = Mathf.Infinity;
        Enemie[] allEnemies = FindObjectsOfType<Enemie>();

        foreach (Enemie currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        
        try
        {
            return closestEnemy.transform.position;
        }

        catch (System.NullReferenceException)
        {
            return gameObject.transform.position;
        }

    }



    public bool IfIsMoving()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            is_moving = true;
        }
        else
        {
            is_moving = false;
        }
        return is_moving;
    }

    public override void Damage()
    {
        base.Damage();
        MainManager.Instance.SetText("Player health: "+hp.ToString());
    }

    public bool NoEnemyLeft()
    {
        Enemie[] allEnemies = FindObjectsOfType<Enemie>();

        if (allEnemies.Length <=0)
        {
            return true;
        }
        return false;

    }


}

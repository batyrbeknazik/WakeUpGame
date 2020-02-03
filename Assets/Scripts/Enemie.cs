using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : Character
{
    [Header("Enemy Values")]
    public float distance;
    public float stopping_distance;

    [Header("Target")]
    public Transform target;


    public void Start()
    {
        SetValues();
    }


    protected void FixedUpdate()
    {

        if (MainManager.Instance.gameON)
        {
            LookAt(FindEnemy());


            Move();
            
        }
    }

    public override void Move()
    {

        if (EnemieIsNear())
        {
            if(KeepDistance())
            {
                FollowEnemie();
            }
            else
            {
                ShootTimer();

                if (TimeToShoot())
                {
                    Shoot();
                }
            }
        }
    }


    public bool EnemieIsNear()
    {
        return Vector2.Distance(transform.position, target.position) < distance;
    }

    public bool KeepDistance()
    {
        return Vector2.Distance(transform.position, target.position) > stopping_distance;
    }

    public void FollowEnemie()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, run_speed * Time.deltaTime); 
    }

    public void SetValues()
    {
        hero_body = gameObject.GetComponent<Rigidbody>();
    }


    public override Vector3 FindEnemy()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        return target.transform.position;
       
    }

   




}

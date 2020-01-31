using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : Character
{
    public float distance;
    public float stopping_distance;

    public Transform target;

    public void Start()
    {
        SetValues();
    }


    //protected void FixedUpdate()
    //{
    //    if (MainManager.Instance.gameON)
    //    {

    //        if (IfIsMoving())
    //        {
    //            Move();
    //        }
    //        else
    //        {
    //            Shoot();
    //        }
    //    }
    //}

    public override void Move()
    {
        if (EnemieIsNear())
        {
            if(KeepDistance())
            {
                FollowEnemie();
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
        transform.position = Vector3.MoveTowards(transform.position, target.position, run_speed * Time.deltaTime); 
    }

    public void SetValues()
    {
        distance = 25f;
        run_speed = 12f;
        stopping_distance = 10f;
        damage_amount = 20f;
        hp = 100;

        timer = 0;
        shoot_speed = 2f;

        hero_body = gameObject.GetComponent<Rigidbody>();

    }
    public override void Shoot()
    {
        //throw new System.NotImplementedException();
        Debug.Log("boof");
    }
    public override bool IfIsMoving()
    {
        //throw new System.NotImplementedException();
        if (transform.hasChanged)
        {
            is_moving = true;
            transform.hasChanged = false;
        }
        else
        {
            is_moving = false;
        }
        return is_moving;
    }

    public override Vector3 FindEnemy()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        return target.position;
    }


}

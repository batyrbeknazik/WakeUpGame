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


    protected void FixedUpdate()
    {
        if (MainManager.Instance.gameON)
        {
            
            LookAt(FindEnemy());

            shoot_timer += Time.deltaTime;

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

                if (ShootTimer())
                {
                    Shoot();
                    shoot_timer = 0f;
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
    public override void Shoot()
    {
        RaycastHit info;

        if (Physics.Raycast(transform.position, transform.forward, out info))
        {

            Hero enemie = info.transform.GetComponent<Hero>();

            if (enemie != null)
            {
                enemie.Damage();
            }

            Debug.Log(info.transform.name);
        }
    }
    

    public override Vector3 FindEnemy()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        return target.transform.position;
       
    }

    

   


}

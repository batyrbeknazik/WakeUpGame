using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public Enemie closestEnemy;
    
    public void Start()
    {
        SetValues();
    }

    protected void FixedUpdate()
    {
        shoot_timer += Time.deltaTime;

        if (MainManager.Instance.gameON)
        {
            LookAt(FindEnemy());

            if (IfIsMoving())
            {
                Move();
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
        if (other.gameObject.name == "WinWall")
        {
            MainManager.Instance.Win();
        }
        else if (other.gameObject.tag=="enemy")
        {
            hp = hp - 5;
        }
    }



    public override Vector3 FindEnemy()
    {
        
        float distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;
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



    public override void Shoot()
    {
        RaycastHit info;

        if (Physics.Raycast(transform.position, transform.forward, out info))
        {
            
            Enemie enemie = info.transform.GetComponent<Enemie>();

            if(enemie!=null)
            {
                enemie.Damage();
            }
            
            Debug.Log(info.transform.name);
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




}

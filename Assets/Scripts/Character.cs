using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody hero_body;

    public float run_speed;
    public float hp;
    public float shoot_speed;
    public float damage_amount;

    public bool is_moving;
    public float timer;


    protected void FixedUpdate()
    {
        timer += Time.deltaTime;

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
                    timer = 0f;
                }
            }

        }
    }

    public abstract void Move();

    public abstract void Shoot();

    public abstract bool IfIsMoving();

    public virtual void LookAt(Vector3 direction)
    {
        transform.LookAt(direction);
    }

    public void Damage()
    {
        hp = hp - damage_amount;
        CheckHealth();
        Debug.Log(hp);  
    }

    public void CheckHealth()
    {
        if (hp<=0)
        {
            Destroy(gameObject);
        }
    }
    public abstract Vector3 FindEnemy();

    public bool ShootTimer()
    {
        return timer > shoot_speed;
    }

}

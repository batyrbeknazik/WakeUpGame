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
    public float shoot_timer;


   

    public abstract void Move();

    public abstract void Shoot();

    public virtual void LookAt(Vector3 direction)
    {
        //direction.y = gameObject.transform.position.y;
        transform.LookAt(direction);
    }

    public virtual void Damage()
    {
        hp = hp - damage_amount;
        CheckHealth();
        Debug.Log(hp+" "+gameObject.name);  
    }

    public void CheckHealth()
    {
        if (hp<=0)
        {
            switch (gameObject.tag)
            {
                case "Player":
                    MainManager.Instance.Win();
                    break;
                case "Enemy":
                    MainManager.Instance.GiveReward(20);
                    break;
            }
            Destroy(gameObject);
        }
    }
    public abstract Vector3 FindEnemy();

    public virtual bool ShootTimer()
    {
        return shoot_timer > shoot_speed;
    }
    

}

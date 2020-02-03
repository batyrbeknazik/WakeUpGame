using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody hero_body;
    [Header("Core Values")]
    public float run_speed;
    public float hp;
    public float shoot_speed;
    public float damage_amount;

    protected bool is_moving;
    protected float shoot_timer;

    


    public abstract void Move();

    public virtual void Shoot()
    {
        RaycastHit info;

        if (Physics.Raycast(transform.position, transform.forward, out info))
        {
            
            Character hero = null;
            switch (info.collider.gameObject.tag)
            { 
                case "Player":
                    hero = info.transform.GetComponent<Hero>();
                    break;
                case "Enemy":
                    hero = info.transform.GetComponent<Enemie>();
                    break;
            }
            if (hero != null)
            {
                hero.Damage();
            }
            Debug.Log(info.transform.name);
        }
    }

    public virtual void LookAt(Vector3 direction)
    {
        //transform.LookAt(direction);
        float rotating_speed = 10 * Time.deltaTime;
        Vector3 targetDir = direction - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotating_speed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public virtual void Damage()
    {
        hp = hp - damage_amount;
        CheckHealth();
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

    public virtual bool TimeToShoot()
    {
        if (shoot_timer > shoot_speed)
        {
            shoot_timer = 0f;

            return true;
        }
        
        return false;
    }
    public virtual void ShootTimer()
    {
        shoot_timer += Time.deltaTime;
    }

}

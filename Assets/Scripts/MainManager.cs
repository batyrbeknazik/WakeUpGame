using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public bool gameON;
    protected int money;

    [Header("Enemies Prefabs")]
    public GameObject[] enemiesPrefabs;
    [Header("Enemies Parents")]
    public GameObject infantry_parent;
    public GameObject artillery_parent;
    [Header("Enemies Info")]
    public GameObject[] current_enemies;
    public int enemies_amount;
    [Header("UI")]
    public Text main_text;



    private static MainManager _instance;

    public static MainManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MainManager>();
            }
            return _instance;
        }
    }


    public void Start()
    {
        StartCoroutine(StartGame());
        
    }

    public virtual void Win()
    {
        SetText("WIN");
        gameON = false;
    }

    public void SpawnEnemies()
    {
        for (int i=0;i< enemies_amount; i++)
        {
            int type = Random.Range(0, 2);
            GameObject enemy = Instantiate(enemiesPrefabs[type], GenerateRandomLocation(), Quaternion.identity);
            current_enemies[i] = enemy;
            switch (type)
            {
                case 0:
                    enemy.transform.parent = infantry_parent.transform;
                    break;
                case 1:
                    enemy.transform.parent = artillery_parent.transform;
                    break;

            }
        }
        
    }
    public Vector3 GenerateRandomLocation()
    {
        int z = Random.Range(-2, -46);
        int x = Random.Range(-21, 7);
        Vector3 location = new Vector3(x,8,z);
        return location;
    }

    public IEnumerator StartGame()
    {
        int timer = 3;
        
        while (timer>0)
        {
            yield return new WaitForSeconds(1f);
            SetText(timer.ToString());
            timer--;
        }
        
        gameON = true;
        SpawnEnemies();
    }

    public void GiveReward(int amount)
    {
        money = money + amount;
        print(money + " money");

    }
    public void SetText(string text)
    {
        main_text.text = text;
    }
}

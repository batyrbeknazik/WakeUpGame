using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public bool gameON;

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
        gameON = true;
    }

    public virtual void Win()
    {
        gameON = false;
    }
    
}

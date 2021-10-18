using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 checkpoint;
    public int level;
    public Difficulty difficulty;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        difficulty.setDifficulty(level);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDifficulty(int level)
    {
        this.level = level;
        difficulty.setDifficulty(this.level);
    }
}

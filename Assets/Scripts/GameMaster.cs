using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 checkpoint;
    public int level;
    public bool showRestaurantMessage;
    public bool showAccidentMessage;
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
        showRestaurantMessage = true;
        showAccidentMessage = true;
    }

    public void setDifficulty(int level)
    {
        this.level = level;
        difficulty.setDifficulty(this.level);
    }

    public void setShowRestaurantPrompt(bool value)
    {
        this.showRestaurantMessage = value;
    }

    public void setShowAccidentPrompt(bool value)
    {
        this.showAccidentMessage = value;
    }


}

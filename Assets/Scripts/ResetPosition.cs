using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPosition : MonoBehaviour
{
    private GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Send player back to the checkpoint
        transform.position = gm.checkpoint;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Reload the scene if the player hits another car
        if(other.CompareTag("Traffic"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}

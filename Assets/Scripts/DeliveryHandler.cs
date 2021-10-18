using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryHandler : MonoBehaviour
{
    public GameObject restaurantPrompt;
    public GameObject buildingPrompt;
    public GameObject player;
    public GameObject foodPrefab;

    public DeliverFood foodScript;
    public Difficulty difficultyScript;

    public Text addressValue;
    public Text scoreValue;
    public Text timeValue;

    public int deliveries = 0;
    public float timeTotal;

    private string address = "";
    private bool isAccepted = false;
    private bool canCount = false;
    private float timeLeft;

    private GameMaster gameMaster;



    // Detect a building
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Restaurant") && !isAccepted)
        {
		    Debug.Log(other.name);
            restaurantPrompt.SetActive(true);

        }
        else if (other.CompareTag("Building") && isAccepted)
        {
            Debug.Log(other.name);
            buildingPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Restaurant"))
        {
            // Start the time if the order is accepted
            if (isAccepted)
            {
                timeLeft = timeTotal;
                canCount = true;
            }
            restaurantPrompt.SetActive(false);
        }
        else if (other.CompareTag("Building"))
        {
            buildingPrompt.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Accept the order
            if (other.CompareTag("Restaurant") && !isAccepted)
            {
                // Set a random address to deliver the food
                address = Random.Range(1,43).ToString();
                addressValue.text = address;

                isAccepted = true;
                restaurantPrompt.SetActive(false);
                Debug.Log(address);
            }
            else if (other.CompareTag("Building") && isAccepted)
            {
                isAccepted = false;
                buildingPrompt.SetActive(false);

                // Set the address in DeliverFood so that it knows where to go
                foodScript.address = other.name.ToString();

                // Successful delivery
                if (other.name.ToString() == string.Format("Building{0}", address))
                {
                    // Increase score
                    int scoreInt = int.Parse(scoreValue.text);
                    scoreInt++;

                    // Relay information to other scripts
                    difficultyScript.setDifficulty(scoreInt);
                    gameMaster.level = scoreInt;
                    foodScript.isSuccess = true;

                    // Update HUD
                    scoreValue.text = scoreInt.ToString();
                    addressValue.text = "x";
                    timeValue.text = "x";

                    canCount = false;
                    Debug.Log("Correct");
                }
                
                // Failed delivery
                else
                {
                

                    // Relay information to other scripts
                    foodScript.isSuccess = false;

                    // Reset HUD
                    scoreValue.text = "0";
                    addressValue.text = "x";
                    timeValue.text = "x";

                    canCount = false;
                    Debug.Log("Wrong");
                }

                // Make the food object at the player's position
                GameObject food = Instantiate(foodPrefab) as GameObject;
                food.transform.position = player.transform.position;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0.0f && canCount)
        {
			timeLeft -= Time.deltaTime;
			timeValue.text = timeLeft.ToString("F");
		}
        else if (timeLeft <= 0.0f)
        {
			canCount = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            int scoreInt = int.Parse(scoreValue.text);
                    scoreInt++;
                    difficultyScript.setDifficulty(scoreInt);
                    gameMaster.level = scoreInt;
                    scoreValue.text = scoreInt.ToString();
                    canCount = false;

                    addressValue.text = "x";
                    timeValue.text = "x";
                    foodScript.isSuccess = true;
                    Debug.Log("Correct");
        }
    }
}

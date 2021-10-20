using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryHandler : MonoBehaviour
{
    public GameObject player;

    public GameObject restaurantPrompt;
    public GameObject buildingPrompt;
    public GameObject messageSuccess;
    public GameObject messageFailure;
    public GameObject foodPrefab;


    public Text addressValue;
    public Text scoreValue;
    public Text timeValue;

    public float timeTotal;

    public Difficulty difficultyScript;
    public DeliverFood foodScript;

    private GameMaster gameMaster;
    private Collider2D which;

    private TweenPrompt tweenRestaurant;
    private TweenPrompt tweenBuilding;
    private TweenPrompt HUDaddress;
    private TweenPrompt HUDscore;
    private TweenPrompt HUDtime;
    private TweenPrompt tweenSuccess;
    private TweenPrompt tweenFailure;

    private string address = "";
    private bool isAccepted = false;
    private bool canCount = false;
    private bool atBuilding = false;
    private float timeLeft;


    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        tweenRestaurant = restaurantPrompt.GetComponent<TweenPrompt>();
        tweenBuilding = buildingPrompt.GetComponent<TweenPrompt>();
        HUDaddress = addressValue.GetComponent<TweenPrompt>();
        HUDscore = scoreValue.GetComponent<TweenPrompt>();
        HUDtime = timeValue.GetComponent<TweenPrompt>();
        tweenSuccess = messageSuccess.GetComponent<TweenPrompt>();
        tweenFailure = messageFailure.GetComponent<TweenPrompt>();
    }

    // Detect a building
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Restaurant") && !isAccepted)
        {
		    Debug.Log(other.name);
            restaurantPrompt.SetActive(true);
            tweenRestaurant.tweenIn();

        }
        else if (other.CompareTag("Building") && isAccepted)
        {
            Debug.Log(other.name);
            buildingPrompt.SetActive(true);
            tweenBuilding.tweenIn();
        }

        atBuilding = true;
        which = other;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Restaurant"))
        {
            
            
            tweenRestaurant.tweenOut();
            StartCoroutine(disableWait(restaurantPrompt, 1.5f, false));
        }
        else if (other.CompareTag("Building"))
        {
            tweenBuilding.tweenOut();
            StartCoroutine(disableWait(buildingPrompt, 1.5f, false));

        }

        atBuilding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start timer
        if (timeLeft > 0.0f && canCount)
        {
			timeLeft -= Time.deltaTime;
			timeValue.text = timeLeft.ToString("F");
		}
        else if (timeLeft <= 0.0f && canCount)
        {
            isAccepted = false;
            tweenBuilding.tweenOut();
            StartCoroutine(disableWait(buildingPrompt, 1.5f, false));


            deliveryFail();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            messageSuccess.SetActive(true);
            tweenSuccess.tweenIn();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            tweenSuccess.tweenOut();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            messageFailure.SetActive(true);
            tweenFailure.tweenIn();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            tweenFailure.tweenOut();
        }

        // Order management
        if (Input.GetKeyDown(KeyCode.Space) && atBuilding)
        {
            // Accept the order
            if (which.CompareTag("Restaurant") && !isAccepted)
            {
                // Set a random address to deliver the food
                address = Random.Range(1,43).ToString();
                HUDaddress.tweenIn();
                addressValue.text = address;

                // Change roadblocks
                if (gameMaster.level >= 2)
                    difficultyScript.introduceRoadBlocks();

                isAccepted = true;
                tweenRestaurant.tweenOut();
                StartCoroutine(disableWait(restaurantPrompt, 1.5f, false));
                
                // Start the time if the order is accepted
                if (isAccepted)
                {
                    timeLeft = timeTotal;
                    canCount = true;
                }
                HUDtime.tweenIn();


                Debug.Log(address);
            }
            else if (which.CompareTag("Building") && isAccepted)
            {
                isAccepted = false;
                tweenBuilding.tweenOut();
                StartCoroutine(disableWait(buildingPrompt, 1.5f, false));

                // Successful delivery
                if (which.name.ToString() == string.Format("Building{0}", address))
                    StartCoroutine(deliverySuccess());
                
                // Failed delivery
                else
                    StartCoroutine(deliveryFail());

            }
        }

        // The following key presses are not expected from the player
        // Test difficulty
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int scoreInt = int.Parse(scoreValue.text);
                    scoreInt++;
                    difficultyScript.setDifficulty(scoreInt);
                    gameMaster.level = scoreInt;
                    HUDscore.tweenIn();
                    scoreValue.text = scoreInt.ToString();
                    canCount = false;

                    HUDaddress.tweenIn();
                    addressValue.text = "x";
                    HUDtime.tweenIn();
                    timeValue.text = "x";
                    foodScript.isSuccess = true;
                    Debug.Log("Correct");
        }

        // Test success explosion
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foodScript.followBuilding = false;
            foodScript.isSuccess = true;
            GameObject food = Instantiate(foodPrefab) as GameObject;
            food.transform.position = player.transform.position;
        }

        // Test failure explosion
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foodScript.followBuilding = false;
            foodScript.isSuccess = false;
            GameObject food = Instantiate(foodPrefab) as GameObject;
            food.transform.position = player.transform.position;

        }
    }

    IEnumerator deliverySuccess()
    {
        // Set the address in DeliverFood so that it knows where to go
        foodScript.address = which.name.ToString();
        foodScript.followBuilding = true;
        foodScript.isSuccess = true;
        
        // Make the food object at the player's position
        GameObject food = Instantiate(foodPrefab) as GameObject;
        food.transform.position = player.transform.position;

        // Wait for the food to reach the door
        yield return new WaitForSeconds(2);

        // Show success message briefly
        messageSuccess.SetActive(true);
        tweenSuccess.tweenIn();
        StartCoroutine(disableWait(messageSuccess, 3.0f, true));

        // Increase score
        int scoreInt = int.Parse(scoreValue.text);
        scoreInt++;

        // Increase difficulty
        difficultyScript.setDifficulty(scoreInt);
        if (gameMaster.level < scoreInt)
            gameMaster.level = scoreInt;

        // Update HUD
        HUDscore.tweenIn();
        scoreValue.text = scoreInt.ToString();
        HUDaddress.tweenIn();
        addressValue.text = "x";
        HUDtime.tweenIn();
        timeValue.text = "x";

        // Stop the counter
        canCount = false;
        Debug.Log("Delivery Succesful");
    }

    IEnumerator deliveryFail()
    {
        // Set the address in DeliverFood so that it knows where to go
        foodScript.address = which.name.ToString();
        foodScript.followBuilding = true;
        foodScript.isSuccess = false;
        
        // Make the food object at the player's position
        GameObject food = Instantiate(foodPrefab) as GameObject;
        food.transform.position = player.transform.position;

        // Wait for the food to reach the door
        yield return new WaitForSeconds(2);

        // Show failure message briefly
        messageFailure.SetActive(true);
        tweenFailure.tweenIn();
        StartCoroutine(disableWait(messageFailure, 3.0f, true));


        // Reset HUD
        HUDscore.tweenIn();
        scoreValue.text = "0";
        HUDaddress.tweenIn();
        addressValue.text = "x";
        HUDtime.tweenIn();
        timeValue.text = "x";

        // Stop the counter
        canCount = false;
        Debug.Log("Delivery Failed");
    }

    IEnumerator disableWait(GameObject prompt, float time, bool tOut)
    {
        yield return new WaitForSeconds(time);

        if (tOut)
        {
            tweenFailure.tweenOut();
            yield return new WaitForSeconds(0.2f);
        }

        prompt.SetActive(false);
    }

}

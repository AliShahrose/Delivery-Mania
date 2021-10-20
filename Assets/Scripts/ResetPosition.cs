using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPosition : MonoBehaviour
{
    public GameObject messageAccident;
    public GameObject blast;
    public GameObject blastParticles;
    public GameObject messageDeliverySuccess;
    public GameObject messageDeliveryFailure;
    public GameObject messageRestaurant;
    public GameObject restaurantPrompt;
    public GameObject buildingPrompt;
    public GameObject deliveryInfo;

    private GameObject playerSprite;
    private GameMaster gm;
    private TweenTransition transition;
    private TweenPrompt tweenAccident;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transition = GetComponentInParent<TweenTransition>();
        tweenAccident = messageAccident.GetComponent<TweenPrompt>();
        playerSprite = GameObject.Find("Sprite");



        // Send player back to the checkpoint
        transform.position = gm.checkpoint;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Reload the scene if the player hits another car
        if(other.CompareTag("Traffic"))
        {
            //Destroy both cars
            Instantiate(blast, new Vector3(transform.position.x - 1.0f, transform.position.y + 1.0f, 0), Quaternion.identity);
            Instantiate(blastParticles, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.3f, 0), Quaternion.identity);
            other.gameObject.SetActive(false);
            playerSprite.SetActive(false);

            // Fix this horrible mess
            messageDeliverySuccess.SetActive(false);
            messageDeliveryFailure.SetActive(false);
            messageRestaurant.SetActive(false);
            restaurantPrompt.SetActive(false);
            buildingPrompt.SetActive(false);
            deliveryInfo.SetActive(false);

            if (gm.showAccidentMessage)
            {
                messageAccident.SetActive(true);
                tweenAccident.tweenIn();
                gm.setShowAccidentPrompt(false);
                StartCoroutine(waitBeforeReload(5.0f));
            }
            else
                StartCoroutine(waitBeforeReload(3.0f));
        }
    }

    IEnumerator waitBeforeReload(float time)
    {
        yield return new WaitForSeconds(time);

        transition.makeTransition();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(blast, new Vector3(transform.position.x - 1.0f, transform.position.y + 1.0f, 0), Quaternion.identity);
            Instantiate(blastParticles, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.3f, 0), Quaternion.identity);
            playerSprite.SetActive(false);
            StartCoroutine(waitBeforeReload(5.0f));
        }
    }

}

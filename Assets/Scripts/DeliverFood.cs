using UnityEngine;

public class DeliverFood : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] foodList;

    public GameObject building;
    public GameObject successParticles;
    public GameObject failureParticles;

    public string address = "";
    public bool isSuccess;
    public float moveSpeed;
    public float rotationSpeed;

    private float rotZ;

    void Start()
    {
        building = GameObject.Find(address + "Door");

        // Select a random food item
        spriteRenderer.sprite = foodList[Random.Range(0,5)];
        
    }

    void Update()
    {
        // Move the food from player to the door while rotating
        rotZ += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,0, rotZ);
        transform.position = Vector2.MoveTowards(transform.position, building.transform.position, moveSpeed * Time.deltaTime);
        
        // Door reached
        if(Vector2.Distance(transform.position, building.transform.position) < 0.1f)
        {
            // Show approriate particles depending on success of failure
            if(isSuccess)
            {
                Instantiate(successParticles, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(failureParticles, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
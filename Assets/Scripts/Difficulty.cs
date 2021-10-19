using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    // public DeliveryHandler deliveryHandler;
    public GameObject traffic;
    public GameObject[] roadBlocks;

    private GameMaster gameMaster;




    public void setDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                break;

            case 1:
                introduceTraffic(level);
                break;

            case 2:
                introduceTraffic(level);
                introduceRoadBlocks();
                break;

            default:
                introduceTraffic(2);
                introduceRoadBlocks();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // deliveryHandler = FindObjectOfType<DeliveryHandler>();
        traffic.SetActive(false);
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        setDifficulty(gameMaster.level);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            setDifficulty(1);
        }
    }

    void introduceTraffic(int level)
    {
        traffic.SetActive(true);

        switch (level)
        {
            case 1:
                traffic.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                traffic.transform.GetChild(0).gameObject.SetActive(true);
                traffic.transform.GetChild(1).gameObject.SetActive(true);
                break;
            default:
            break;
        }
    }

    public void introduceRoadBlocks()
    {
        // int even = Random.Range(0,(24 - 0) / 2) * 2;
                // int odd = Random.Range(0,(24 - 0) / 2) * 2 + 1;
                // if(Random.Range(0, 100) > 50)
                // {
                //     foreach(GameObject block in roadBlocks)
                //     {
                //         int b = int.Parse(block.name.Substring(block.name.Length - 3));
                //         if(b % 2 == 0 )
                //         {
                //             int e = int.Parse(block.name.Substring(block.name.Length - 1));
                //             if(e % 2 != 0)
                //                 block.SetActive(true);
                //             else
                //             block.SetActive(false);

                //         }
                        
                //     }
                //     // foreach(GameObject block in roadBlocks)
                //     // {
                //     //     int b = int.Parse(block.name.Substring(block.name.Length - 1));
                //     //     if(b % 2 == 0 )
                //     //         block.SetActive(true);
                //     //     else
                //     //         block.SetActive(false);
                //     // }

                //     // roadBlocks[even].SetActive(true);
                //     // roadBlocks[odd].SetActive(false);
                // }
                // else
                // {
                //     foreach(GameObject block in roadBlocks)
                //     {
                //         int b = int.Parse(block.name.Substring(block.name.Length - 1));
                //         if(b % 2 != 0 )
                //             block.SetActive(true);
                //         else
                //             block.SetActive(false);
                //     }
                //     // roadBlocks[even].SetActive(false);
                //     // roadBlocks[odd].SetActive(true);
                // }
                // foreach(GameObject block in roadBlocks)
                // {
                //     int rand = Random.Range(0,2);
                //     int b = int.Parse(block.name.Substring(1,2));
                //     if(b % 2 == 0 )
                //     {
                //         // int e = int.Parse(block.name.Substring(block.name.Length - 1));
                //         // if(e % 2 != 0)
                //         //     block.SetActive(true);
                //         // else
                //         //     block.SetActive(false);

                //             block.SetActive(false);
                //     }
                //     else
                //     {
                //         if(block.name.EndsWith(Random.Range(0,2).ToString()))
                //             block.SetActive(true);
                //         else
                //             block.SetActive(false);
                //         // int e = int.Parse(block.name.Substring(block.name.Length - 1));
                //         // if(e % 2 == rand)
                //         //     block.SetActive(true);
                //         // else
                //         //     block.SetActive(false);
                //     }
                    
                // }

        // Randomly select which type of street to block
        // Should not be consective streets
        bool blockEven;
        if (Random.Range(0, 2) % 2 == 0)
            blockEven = true;
        else
            blockEven = false;

        foreach(GameObject street in roadBlocks)
        {
            if(blockEven)
            {
                // Remove all blocks so that in next iteration there is nothing in the way
                street.transform.GetChild(0).gameObject.SetActive(false);
                street.transform.GetChild(1).gameObject.SetActive(false);

                // Determine if the street matches the one we want to block
                if (int.Parse(street.name.Substring(street.name.Length - 2)) % 2 == 0)
                    // Only block one end of the street to still keep it accessible
                    street.transform.GetChild(Random.Range(0,2)).gameObject.SetActive(true);
            }
            else
            {
                street.transform.GetChild(0).gameObject.SetActive(false);
                street.transform.GetChild(1).gameObject.SetActive(false);
                
                if (int.Parse(street.name.Substring(street.name.Length - 2)) % 2 != 0)
                    street.transform.GetChild(Random.Range(0,2)).gameObject.SetActive(true);
            }
        }
    }
}

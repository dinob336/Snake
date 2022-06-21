using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject foodPrefab;
    // Start is called before the first frame update
    void Start()
    {
         RandomPosition();
    }

    // Update is called once per frame

    public void RandomPosition()
    {
        int xPos = Random.Range(-9, 9);
        int zPos = Random.Range(-9, 9);

        transform.position = new Vector3(xPos, 0, zPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(foodPrefab);
            RandomPosition();
        }
       
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public GameObject player;
    public Vector3 playerPos;
    public Vector3 direction;

    public List<Transform> segments;
    public Transform segmentPrefab;

    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();
        segments.Add(transform);

        direction = Vector3.forward;
     }
    private void Update()
    {
        Movement();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        for(int i = segments.Count -1; i >0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
                0,
            Mathf.Round(transform.position.z + direction.z)
            );
        
    }

    private void Movement()
    {
        playerPos = transform.position;
        if (Input.GetKeyDown(KeyCode.UpArrow)) { direction = Vector3.forward; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { direction = Vector3.back; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { direction = Vector3.left; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { direction = Vector3.right; }
    }
    
    

    private void IncreaseSize()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }



    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        IncreaseSize();
        Time.fixedDeltaTime -= 0.005f;
        Debug.Log(Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bound" || collision.gameObject.tag == "SnakeSegment")
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }

}

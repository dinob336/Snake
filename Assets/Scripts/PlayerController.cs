using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    
    public GameObject player;
    public Vector3 playerPos;
    public Vector3 direction;

    public List<Transform> segments;
    public Transform segmentPrefab;

    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject mainMenuButton;
    
    public TMP_Text scoreText;
    private int score;

   
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
        segments = new List<Transform>();
        segments.Add(transform);

        direction = Vector3.forward;
        gameOver = false;
        score = 0;
        
        
    }
    private void Update()
    {
        Movement();
        scoreText.text = $"Score : {score}";
        

        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.3f;
            gameOver = false;
        }
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
        if (Input.GetKeyDown(KeyCode.UpArrow)) { direction = Vector3.forward; transform.rotation = Quaternion.Euler(0, 0, 0); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { direction = Vector3.back; transform.rotation = Quaternion.Euler(0, 180, 0); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { direction = Vector3.left; transform.rotation = Quaternion.Euler(0, -90, 0); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { direction = Vector3.right; transform.rotation = Quaternion.Euler(0, 90, 0); }
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
        score++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bound" || collision.gameObject.tag == "SnakeSegment")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        mainMenuButton.SetActive(true);

    }
}

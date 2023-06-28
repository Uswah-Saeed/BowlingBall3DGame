using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float sideMoveSpeed = 10f;
    private float duringThrowMoveSpeed = 2f;
    public float forwardMoveSpeed = 100000f;
    private Rigidbody rb;
    public bool ballThrown = false;
    [SerializeField]
    private Transform locToReach;
    private GameManager _gameManager;
    public static bool _secondThrow = false;
    private Animator cameraAnimator;
    private GameObject cameraObject;
    private TextMeshProUGUI feedback;
    public AudioManager audioManager;

    [SerializeField]
    private GameObject pinParent;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        cameraAnimator = cameraObject.GetComponent<Animator>();
        feedback= GameObject.FindGameObjectWithTag("Feedback").GetComponent<TextMeshProUGUI>();
        feedback.text = "";
        audioManager = FindObjectOfType<AudioManager>();


    }
    private void Update()
    {
        if (!ballThrown)
        {
            MoveBall();
            Debug.Log("ball move state");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("camera move");
                cameraAnimator.SetBool("cameraMove", true);
                spawnBall();
                
            }   
        }
        else
        {

            MoveBallDuringThrow();
        }
        if (transform.position.z > 18f)
        {
            cameraAnimator.SetBool("cameraMove", false);
            StartCoroutine(Feedback());
            //feedback.text = "";
        }

    }
    public void spawnBall()
    {

        Debug.Log("ball spawned");
        ballThrown = true;
        
        rb.AddForce(Vector3.forward * Time.deltaTime * forwardMoveSpeed);
        

        StartCoroutine("InstantiateBall");

    }
    public void MoveBall()
    {
        audioManager.PlayMySound("rolling");
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 position = transform.position;
        position += new Vector3(moveHorizontal, moveVertical, 0f) * Time.deltaTime * sideMoveSpeed;
        position.x = Mathf.Clamp(position.x, -7f, 7f);
        position.y = Mathf.Clamp(position.y, 1.0f, 2.6f);
        transform.position = position;
    }
    public void MoveBallDuringThrow()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        position += new Vector3(moveHorizontal,0f, 0f) * Time.deltaTime * duringThrowMoveSpeed;
        position.x = Mathf.Clamp(position.x, -7f, 7f);
        transform.position = position;
    }
    
   
    IEnumerator InstantiateBall()
    {
        //Debug.Log("in coroutine");
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
        _gameManager.InstantiateBall();
       

    }
    IEnumerator Feedback()
    {
        yield return new WaitForSeconds(3f);
        GenerateFeedback();
        yield return new WaitForSeconds(2f);
        feedback.GetComponent<Animator>().SetBool("textAppear", false);
    }
    void GenerateFeedback()
    {
      
        int point = Score.GetInstance().GetScore();
        Debug.Log("here are tootallee points : " + point);
        if(point > 9)
        {
            audioManager.PlayMySound("cheers");
            if (PinRestPos.counter % 2 == 0)
            {
                feedback.text = "Spare!";
            }
            else { feedback.text = "Strike!"; }
            
        }
        else if(point >=6 && point <= 9)
        {
            feedback.text = "Great!";
        }
        else if(point >=1 && point <= 5) 
        {
            feedback.text = "Practice Practice Practice!!";
        }
        feedback.GetComponent<Animator>().SetBool("textAppear", true);
    }
   
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinRestPos : MonoBehaviour
{
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private Rigidbody[] pinRigidbodies;
    public static int counter = 0;

    // Start is called before the first frame update
    void Awake()
    {

        int pinCount = transform.childCount;
        initialPositions = new Vector3[pinCount];
        initialRotations = new Quaternion[pinCount];
        pinRigidbodies = new Rigidbody[pinCount];
        for (int i = 0; i < pinCount; i++)
        {
            Transform pin = transform.GetChild(i);
            initialPositions[i] = pin.position;
            initialRotations[i] = pin.rotation;
            pinRigidbodies[i] = pin.GetComponent<Rigidbody>();
        }
    }
    public void ResetPins()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            Transform pin = transform.GetChild(i);
            pin.position = initialPositions[i];
            pin.rotation = initialRotations[i];
            pinRigidbodies[i].velocity = Vector3.zero;
            pinRigidbodies[i].angularVelocity = Vector3.zero;
            pin.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (counter > 0 && counter % 2 == 0)
        {        
            StartCoroutine(reInstatiatePins());
        }
        else if(Score.GetInstance().GetScore() == 10)
         {
            
            StartCoroutine(reInstatiatePins());
   
        }

    }
    IEnumerator reInstatiatePins()
    {
        yield return new WaitForSeconds(3.5f);
        ResetPins();
        Score.GetInstance().reset_scoreboard();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player");
            counter++;
            Debug.Log("this is counter: " + counter);
        }
    }

}

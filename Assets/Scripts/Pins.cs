using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pins : MonoBehaviour
{
    [SerializeField]
    private Transform pin;
    private float threshold = 0.6f;
    private GameObject parentObject;
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void CheckPinStatus()
    {
        if(pin.up.y < threshold)
        {
            audioManager.PlayMySound("knockedDown");
            Score.GetInstance().AddScore(1);
            parentObject = gameObject.transform.parent.gameObject;
            parentObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "pins")
        {

            Debug.Log("hit " + other);
            CheckPinStatus();

        }
    }
}

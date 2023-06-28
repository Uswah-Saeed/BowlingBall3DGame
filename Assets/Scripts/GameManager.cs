using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform ballLocation;
    [SerializeField]
    private GameObject pinParent;
    [SerializeField]
    private GameObject pinPrefab;
   

    void Awake()
    {
        InstantiateBall(); 
    }

    public void InstantiateBall()
    {
        if (ball != null && ballLocation != null)
        {
            Instantiate(ball, ballLocation.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("error in loading ball and location");
        }
    }
 
    


}

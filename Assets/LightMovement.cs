using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, 4, 0);
    GameObject RocketObj;
    Vector3 RocketPosition; 
    // Start is called before the first frame update
    void Start()
    {
        RocketObj = GameObject.Find("Rocket");
        RocketPosition = RocketObj.transform.position;
        transform.position = RocketPosition + offset;
    }

    // Update is called once per frame
    void Update()
    {
        RocketPosition = RocketObj.transform.position;
        transform.position = RocketPosition + offset;
    }
}

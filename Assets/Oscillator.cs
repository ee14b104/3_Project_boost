using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
     Vector3 movementVector = new Vector3(0,8.5f, 0);
    [Range(-1,1)] [SerializeField]  float movementFactor;
    Vector3 startingPos;
    [SerializeField] float phase = 0;
     float period = 3f;
    // Start is called before the first frame update
    void Start()
    {

        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        movementFactor = Mathf.Sin(2 * Mathf.PI * cycles / period + phase*2*Mathf.PI/360);
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
        
    }
}

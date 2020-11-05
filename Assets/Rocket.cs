using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    float thrust = 10;
    AudioSource m_MyAudioSource;
    bool fuel = false;
    bool moveRight = false;
    bool moveLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    private void FixedUpdate()
    {
        if (fuel)
        {
            rigidbody.AddRelativeForce(Vector3.up * thrust);

        }
        if (moveRight)
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 100);
        }
        if (moveLeft)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 100);

        }
    }
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fuel = true;
            if ( m_MyAudioSource.isPlaying == false)
            {
                m_MyAudioSource.Play();
            }
        }
        else
        {
            fuel = false;
            m_MyAudioSource.Stop();
        }
        if(Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else
        {
            moveLeft = false;
            moveRight = false;
        }
    }
}

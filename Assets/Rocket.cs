using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    public float thrust = 10;
    AudioSource m_MyAudioSource;
    bool fuel = false;
    bool moveRight = false;
    bool moveLeft = false;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            ProcessInput();
        }
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision collision)

    {
        if (state != State.Alive) { return; }
        if (collision.gameObject.tag == "Finish")
        {
            state = State.Transcending;
            Invoke("LoadNextLevel", 1f);
        }
        else if(collision.gameObject.tag == "friendly")
        {
            
        }
        else
        {
            state = State.Dying;
            Invoke("LoadFirstLevel", 1f);
        }
    }
    private void FixedUpdate()
    {
        if (fuel)
        {
            rigidbody.AddRelativeForce(Vector3.up * thrust);

        }
        rigidbody.freezeRotation = true;
        if (moveRight)
        {
            transform.Rotate(-Vector3.forward * Time.fixedDeltaTime * 30);
        }
        if (moveLeft)
        {
            transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 30);

        }
        rigidbody.freezeRotation = false;

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

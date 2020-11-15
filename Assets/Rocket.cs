using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    public float thrust = 10;
    
    bool fuel = false;
    bool moveRight = false;
    bool moveLeft = false;
    public AudioClip mainEngine;
    public AudioClip death;
    public AudioClip sucess;
    public ParticleSystem mainEngineParticle;
    public ParticleSystem deathParticle;
    public ParticleSystem sucessParticle;
    public float levelLoadDelay = 1f;
    public Vector3 RocketPosition;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        RocketPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        RocketPosition = transform.position;
        if (state == State.Alive)
        {
            ProcessInput();
        }
    }
    private void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
            audioSource.Stop();
            sucessParticle.Play();
            audioSource.PlayOneShot(sucess);
            Invoke("LoadNextLevel", levelLoadDelay);
        }
        else if(collision.gameObject.tag == "friendly")
        {
            
        }
        else
        {
            state = State.Dying;
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticle.Play();
            
            Invoke("LoadFirstLevel", levelLoadDelay);
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
            if ( audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(mainEngineParticle.isStopped)
            {
                mainEngineParticle.Play();
            }
        }
        else
        {
            fuel = false;
            audioSource.Stop();
            mainEngineParticle.Stop();
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

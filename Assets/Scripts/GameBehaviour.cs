using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    [SerializeField] private float rcsThrust = 80f;
    [SerializeField] private float mainThrust = 10f;
    
    private void Start()
    {
        // Getting Components from the Unity Editor
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        // Manage Rocket Movement
        ProcessInput();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // Collision Checker when the object hit the "Dead-Zones" for Game Lost and "Win-Zones" for Game Win.
        switch (other.gameObject.tag)
        {
            case "Dead-Zone":
                print("You Lost.");
                break;
            case "Win-Zone":
                print("You WON!");
                break;
            default:
                print("Still Playing.");
                break;
        }
    }

    private void ProcessInput()
    {
        // Add the Movement support
        Thrust();
        // Add the Left-Right Movement supports
        Rotate();
    }

    private void Rotate()
    {
        var frameRotationSpeed = rcsThrust * Time.deltaTime;
        
        _rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft(frameRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
           RotateRight(frameRotationSpeed);
        }

        _rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartAudio();
            AddForce();
        }
        else
        {
            StopAudio();
        }
    }

    private void StartAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void AddForce()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust);
    }

    private void StopAudio()
    {
        _audioSource.Stop();
    }

    private void RotateLeft(float frameRotationSpeed)
    {
        transform.Rotate(Vector3.forward * frameRotationSpeed);
    }

    private void RotateRight(float frameRotationSpeed)
    {
        transform.Rotate(Vector3.back * frameRotationSpeed);
    }
}
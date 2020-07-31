using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    [SerializeField] private float rcsThrust = 80f;
    [SerializeField] private float mainThrust = 10f;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        ProcessInput();
    }


    private void OnCollisionEnter(Collision other)
    {
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
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        var frameRotationSpeed = rcsThrust * Time.deltaTime;
        _rigidbody.freezeRotation = true;
        
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.Rotate(Vector3.forward * frameRotationSpeed);

        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * frameRotationSpeed);
        }
        
        _rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            } 
           
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
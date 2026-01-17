using UnityEngine;
using System.Collections;

using Unity.VisualScripting;
public class FoxController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float gravityModifier = 15f;
    [SerializeField] private bool isOnGround = true;
    public bool isGameOver = false;
    private Animator _playerAnim;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _dirtParticle;

    private AudioSource _playerAudio;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _crashSound;

    [SerializeField] int _health = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerAudio = GetComponent<AudioSource>();

        _playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void OnJump()
    {
        if (isOnGround && !isGameOver)
        {
            _playerAnim.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            _dirtParticle.Stop();
            _playerAudio.PlayOneShot(_jumpSound, 1.0f);
        }
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            _dirtParticle.Play();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {  
            
            if (_health < 1)
            {
                isGameOver = true;
                _playerAnim.SetBool("Death_b", true);

                _explosionParticle.Play();
                _dirtParticle.Stop();
                _playerAudio.PlayOneShot(_crashSound, 1.0f);
                Time.timeScale = 0f;
            }
            else if (_health >= 1)
            {
                HealthMinus(collision);
            
            
                _playerAnim.SetTrigger("Death_trig");
                _playerAudio.PlayOneShot(_crashSound, 1.0f);
                _dirtParticle.Stop();
                _explosionParticle.Play();
                StartCoroutine(ResumeAfterDelay(5f));
            }

        }
    }
    private IEnumerator ResumeAfterDelay(float delay)
    {
        Time.timeScale = 0f; 
        yield return new WaitForSecondsRealtime(delay); 
        Time.timeScale = 1f;
        _playerAnim.SetTrigger("Run_trig");
        _dirtParticle.Play();
        _explosionParticle.Stop();
    }
    private void HealthMinus(Collision collision)
        {
        _health--;
        Destroy(collision.gameObject);
    }
}

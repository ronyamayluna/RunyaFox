using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
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
    [SerializeField] private AudioClip _attackSound;

    [SerializeField] int _health = 3;
    private bool hasCollided = false; 

    public GameObject coinPrefab;
    [SerializeField] private Animator _birdAnim;
    //[SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoremanager;
    [SerializeField] GameObject bird;

    void Start()
    {
        _playerAudio = GetComponent<AudioSource>();
        _playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
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

    public void OnAttack()
    {
        if (!isGameOver)
        {
            _dirtParticle.Stop();
            _explosionParticle.Play();
            _playerAnim.SetTrigger("Attack_trig");
            _playerAudio.PlayOneShot(_attackSound, 1.0f);

            // Проверяем наличие птицы в радиусе атаки
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f); // Радиус атаки = 2.0f
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Bird"))
                {
                    Destroy(hitCollider.gameObject); // Уничтожаем птицу
                    Debug.Log("Птица уничтожена!");
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            _dirtParticle.Play();
        }

        if ((collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Bird")) && !hasCollided)
        {
            hasCollided = true; 

            _health--; 
            if (_health < 1)
            {
                
                isGameOver = true;
                _playerAnim.SetBool("Death_b", true);

                _explosionParticle.Play();
                _dirtParticle.Stop();
                _playerAudio.PlayOneShot(_crashSound, 1.0f);
                
            }
            else
            {
                
                _playerAnim.SetTrigger("Death_trig");
                _playerAudio.PlayOneShot(_crashSound, 1.0f);
                _dirtParticle.Stop();
                _explosionParticle.Play();
                StartCoroutine(ResumeAfterDelay(1f));
            }

          
            StartCoroutine(ResetCollisionFlag());
        }
    }

    private IEnumerator ResetCollisionFlag()
    {
        yield return new WaitForSeconds(0.5f); 
        hasCollided = false;
    }
    private void TimeScale()
        {
        
        StartCoroutine(ResumeAfterDelay(1f));
        
    }
    private IEnumerator ResumeAfterDelay(float delay)
    {
        
        yield return new WaitForSecondsRealtime(delay);
        
        _playerAnim.SetTrigger("Run_trig");
        _dirtParticle.Play();
        _explosionParticle.Stop();
    }
    private IEnumerator GameOverDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            scoremanager.AddScore();
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Bird") && !isGameOver)
        {
            if (_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // Проверяем, атакует ли игрок
            {
                Destroy(other.gameObject); // Уничтожаем птицу
                _playerAudio.PlayOneShot(_attackSound, 1.0f);
            }
            else
            {
                _health--;
                if (_health < 1)
                {
                    isGameOver = true;
                    _playerAnim.SetBool("Death_b", true);

                    _explosionParticle.Play();
                    _dirtParticle.Stop();
                    _playerAudio.PlayOneShot(_crashSound, 1.0f);
                }
                else
                                   {
                    _playerAnim.SetTrigger("Death_trig");
                    _playerAudio.PlayOneShot(_crashSound, 1.0f);
                    _dirtParticle.Stop();
                    _explosionParticle.Play();
                    StartCoroutine(ResumeAfterDelay(1f));
                }
            }
        }
    }
    private void BirdDestroying()
        {
        if (isGameOver)
        {
            Destroy(bird);
        }
    }

 
}

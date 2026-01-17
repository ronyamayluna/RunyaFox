using UnityEngine;
using System.Collections;


public class BirdFly : MonoBehaviour
{
    private float speed = 5f;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    [SerializeField] GameObject birdPrefab;
    [SerializeField] private AudioClip _chkchkSound;
    [SerializeField] private PlayerController _playerControllerScript;
    private bool isBirdSpawning = false; // Флаг для контроля запуска корутины

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();

        // Проверяем, назначен ли PlayerController в инспекторе
        if (_playerControllerScript == null)
        {
            Debug.LogError("PlayerController не назначен в BirdFly. Назначьте его в инспекторе.");
        }

        // Проверяем, назначен ли birdPrefab в инспекторе
        if (birdPrefab == null)
        {
            Debug.LogError("birdPrefab не назначен в BirdFly. Назначьте его в инспекторе.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Убедимся, что корутина запускается только один раз
        if (!isBirdSpawning)
        {
            isBirdSpawning = true;
            StartCoroutine(SpawnBirdEvery20Seconds());
        }
    }

    private IEnumerator SpawnBirdEvery20Seconds()
    {
        if (!_playerControllerScript.isGameOver)
            {
            while (true)
            {
                if (_playerControllerScript != null && !_playerControllerScript.isGameOver)
                {
                    _playerAudio.PlayOneShot(_chkchkSound, 1.0f);
                    float x = 0f;
                    float z = 0f;
                    float y = 0;
                    Vector3 pos = new Vector3(x, y, z);
                    Instantiate(birdPrefab, pos, Quaternion.identity);
                }
                yield return new WaitForSeconds(20f);
            }
        }           
    }
}

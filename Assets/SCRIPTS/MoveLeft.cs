using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PlayerController _playerControllerScript;
    float leftBound = -8f;
    // Update is called once per frame

    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void Update()
    {
        if (!_playerControllerScript.isGameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Coin"))
        {
            Destroy(gameObject);
        }
    }
}



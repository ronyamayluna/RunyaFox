using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _obstaclePrefab;
    [SerializeField] GameObject _secondobstaclePrefab;
    Vector3 _spawnPos = new Vector3(25, 0, 0);
    float _startDelay = 2f;
    float _repeatRate = 2f;
    private PlayerController _playerControllerScript;

    private void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
    }

    void SpawnObstacle()
    {
        if (!_playerControllerScript.isGameOver)
        {
            // Выбираем случайный префаб
            GameObject prefabToSpawn = Random.Range(0, 2) == 0 ? _obstaclePrefab : _secondobstaclePrefab;

            // Спавним выбранный префаб
            Instantiate(prefabToSpawn, _spawnPos, prefabToSpawn.transform.rotation);
        }
    }

}

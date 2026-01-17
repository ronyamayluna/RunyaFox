using UnityEngine;
using System.Collections;

public class MoneyTake : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 1;
    public static int totalCoins = 0;
    [SerializeField] private PlayerController _playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCoinsEvery3Seconds());
    }

    private IEnumerator SpawnCoinsEvery3Seconds()
    {
            while (!_playerControllerScript.isGameOver)
            {
                for (int i = 0; i < coinCount; i++)
                {
                    float x = Random.Range(10f, 15f);
                    float z = Random.Range(0f, 0f);
                    float y = Random.Range(0.8f, 2f);
                    Vector3 pos = new Vector3(x, y, z);
                    Instantiate(coinPrefab, pos, Quaternion.identity);
                }
                yield return new WaitForSeconds(3f);
            }
    }
}

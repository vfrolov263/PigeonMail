using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerClouds : MonoBehaviour
{
    [Range(30.0f, 6000.0f)]
    [SerializeField] private float _lifeTime = 30.0f;
    [Range(0.1f, 100.0f)]
    [SerializeField] private float _spawnTime = 2.0f;

    [SerializeField] private GameObject[] _clouds;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => CreateCloud(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 50,
            maxSize: 200);
    }

    private GameObject CreateCloud()
    {
        GameObject cloud = Instantiate(_clouds[Random.Range(0, _clouds.Length)]);
        cloud.AddComponent<MoveCloud>();
        return cloud;
    }

    private void ActionOnGet(GameObject cloud)
    {
        cloud.transform.position = new Vector3(300, 120 + Random.Range(-25, 25), Random.Range(-250, 250));
        cloud.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        cloud.SetActive(true);
        StartCoroutine(LifeTime(cloud));
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            _pool.Get();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator LifeTime(GameObject cloud)
    {
        yield return new WaitForSeconds(_lifeTime);
        _pool.Release(cloud);
    }
}

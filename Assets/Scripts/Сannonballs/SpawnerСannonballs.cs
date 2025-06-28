using UnityEngine;
using System.Collections;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class SpawnerСannonballs : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] public float SpawnTime = 2.0f;
    [SerializeField] private float _lifeTime = 10.0f;

    [SerializeField] private float _force = 100.0f;

    [SerializeField] private GameObject _player;

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
        GameObject ball = Instantiate(_ball);
        //ball.AddComponent<MoveCloud>();
        return ball;
    }

    private void ActionOnGet(GameObject ball)
    {
        //ball.transform.position = new Vector3(_player.transform.position.x + Random.Range(-30.0f, 30.0f), _player.transform.position.y + Random.Range(0.0f, 30.0f), _player.transform.position.z + Random.Range(-30.0f, 30.0f));
        Vector3 generation = _player.transform.position + _player.transform.forward * 50.0f;
        //ball.transform.position = _player.transform.position + _player.transform.forward * 20.0f;
        ball.transform.position = new Vector3(generation.x + Random.Range(-30.0f, 30.0f), generation.y + Random.Range(0.0f, 30.0f), generation.z + Random.Range(-30.0f, 30.0f));

        ball.SetActive(true);

        Vector3 start = ball.transform.position;
        Vector3 end = _player.transform.position;// + _player.transform.forward * 10.0f;

        Vector3 vector = end - start;

        ball.GetComponent<Rigidbody>().AddForce(vector * _force);
        StartCoroutine(LifeTime(ball));
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
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    IEnumerator LifeTime(GameObject cloud)
    {
        yield return new WaitForSeconds(_lifeTime);
        _pool.Release(cloud);
    }

}

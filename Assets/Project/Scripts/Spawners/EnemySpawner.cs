using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] BulletSpawner _enemyBulletSpawner;
	[SerializeField] private Enemy _prefab;
	[SerializeField, Range(0, 0.45f)] private float _ySpawnOffset = 0.15f;
	[SerializeField] private float _xSpawnOffset = 2f;
	[SerializeField] private Vector2 _spawnDelay = new Vector2(2f, 2.5f);

	private ObjectPool<Enemy> _pool;
	private Coroutine _spawnCoroutine = null;

	public event Action EnemyDied;

	private void Awake()
	{
		_pool = new ObjectPool<Enemy>(_prefab, transform);
	}

	private void OnEnable()
	{
		if (_spawnCoroutine == null)
		{
			_spawnCoroutine = StartCoroutine(SpawnRoutine());
		}
	}

	private void OnDisable()
	{
		if (_spawnCoroutine != null)
		{
			StopCoroutine(_spawnCoroutine);
			_spawnCoroutine = null;
		}
	}

	private IEnumerator SpawnRoutine()
	{
		while (enabled)
		{
			Spawn();
			yield return new WaitForSeconds(UnityEngine.Random.Range(_spawnDelay.x, _spawnDelay.y));
		}
	}

	private void Spawn()
	{
		Vector3 leftBottomCorner = _camera.ViewportToWorldPoint(new Vector3(0, _ySpawnOffset, _camera.transform.position.z));
		Vector3 rightTopCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1 - _ySpawnOffset, _camera.transform.position.z));

		float spawnX = rightTopCorner.x + _xSpawnOffset;
		float spawnY = UnityEngine.Random.Range(leftBottomCorner.y, rightTopCorner.y);

		Enemy enemy = _pool.Get(new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
		enemy.Init(_enemyBulletSpawner);
		enemy.Died += OnDied;
		enemy.ReadyToRelease += Unsubscribe;
	}

	private void OnDied()
	{
		EnemyDied?.Invoke();
	}

	private void Unsubscribe(PooledObject obj)
	{
		if (obj is Enemy enemy)
		{
			enemy.Died -= OnDied;
			enemy.ReadyToRelease -= Unsubscribe;
		}
	}
}

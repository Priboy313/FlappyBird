using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	[SerializeField] private Bullet _prefab;
	private ObjectPool<Bullet> _pool;

	private void Awake()
	{
		_pool = new ObjectPool<Bullet>(_prefab, transform);
	}

	public void Spawn(Vector3 position, Quaternion rotation, float speed)
	{
		Bullet bullet = _pool.Get(position, rotation);
		bullet.Init(speed);
	}

}

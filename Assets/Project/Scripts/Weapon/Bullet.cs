using UnityEngine;

[RequireComponent (typeof(DamageDealer))]
public class Bullet : PooledObject
{
	[SerializeField] private float _maxLifeTime = 5f;
	private float _lifeTime;
	private float _speed = 4f;
	private DamageDealer _dealer;

	private void Awake()
	{
		_dealer = GetComponent<DamageDealer>();
	}

	private void OnEnable()
	{
		_lifeTime = 0;
		_dealer.Hit += ReturnToPool;
	}

	private void OnDisable()
	{
		_dealer.Hit -= ReturnToPool;
	}

	public void Init(float speed)
	{
		_speed = speed;
	}

	private void Update()
	{
		transform.Translate(Vector3.right * _speed * Time.deltaTime);

		_lifeTime += Time.deltaTime;

		if (_lifeTime > _maxLifeTime)
		{
			ReturnToPool();
		}
	}
}

using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Health))]
[RequireComponent(typeof(Weapon))]
public class Enemy : PooledObject
{
	[SerializeField] float _shootDelay = 3f;
	[SerializeField] float _moveSpeed = 2f;

	private Health _health;
	private Weapon _weapon;

	private Coroutine _shootCoroutine = null;

	public event Action Died;

	private void Awake()
	{
		_weapon = GetComponent<Weapon> ();
		_health = GetComponent<Health> ();
	}

	public void Init(BulletSpawner spawner)
	{
		_weapon.Init(spawner);
	}

	private void OnEnable()
	{
		_health.Died += OnDied;

		if (_shootCoroutine == null)
		{
			_shootCoroutine = StartCoroutine(ShootRoutine());
		}
	}

	private void OnDisable()
	{
		_health.Died -= OnDied;
		
		if (_shootCoroutine != null)
		{
			StopCoroutine(_shootCoroutine);
			_shootCoroutine = null;
		}
	}

	private void Update()
	{
		transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
	}

	private IEnumerator ShootRoutine()
	{
		WaitForSeconds wait = new WaitForSeconds(_shootDelay);

		while (enabled)
		{
			yield return wait;
			Shoot();
		}
	}

	private void Shoot()
	{
		_weapon.Shoot();
	}

	private void OnDied()
	{
		Died?.Invoke();
		ReturnToPool();
	}
}

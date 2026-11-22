using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
	[SerializeField] private float _maxHealth = 1f;
	private float _health = 1f;

	public event Action Died;

	private void OnEnable()
	{
		_health = _maxHealth;
	}

	public void TakeDamage(float damage) {
		if (_health > 0)
		{
			_health -= damage;

			if (_health <= 0)
			{
				Died?.Invoke();
			}
		}
	}
}

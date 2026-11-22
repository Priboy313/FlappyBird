using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
	[SerializeField] private float _damage = 9999f;

	public event Action Hit;

	private void OnValidate()
	{
		GetComponentInChildren<Collider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out IDamageable damageable))
		{
			damageable.TakeDamage(_damage);
		}

		Hit?.Invoke();
	}
}

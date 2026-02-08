using System;
using UnityEngine;
using Systems.Gameplay.Interacable;

namespace Systems.Gameplay.Bird
{
	[RequireComponent(typeof(Bird))]
	public class BirdCollisionHandler : MonoBehaviour
	{
		[SerializeField] private BirdCollider _collider;

		public event Action<IInteractable> CollisionDetected;

		private void Awake()
		{
			if (_collider == null)
			{
				Debug.LogError("Collider not set!");
				enabled = false;
				return;
			}
		}

		private void OnEnable()
		{
			if (_collider != null)
			{
				_collider.CollisionDetected += OnCollisionDetected;
			}
		}

		private void OnDisable()
		{
			if (_collider != null)
			{
				_collider.CollisionDetected -= OnCollisionDetected;
			}
		}

		private void OnCollisionDetected(IInteractable interactable)
		{
			CollisionDetected?.Invoke(interactable);
		}
	}
}

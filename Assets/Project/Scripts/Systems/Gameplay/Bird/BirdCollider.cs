using System;
using UnityEngine;
using Systems.Gameplay.Interacable;


namespace Systems.Gameplay.Bird
{

	[RequireComponent(typeof(CircleCollider2D))]
	public class BirdCollider : MonoBehaviour
	{
		public event Action<IInteractable> CollisionDetected;

		private void Awake()
		{
			GetComponent<CircleCollider2D>().isTrigger = true;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out IInteractable interactable))
			{
				CollisionDetected?.Invoke(interactable);
			}
		}
	}
}

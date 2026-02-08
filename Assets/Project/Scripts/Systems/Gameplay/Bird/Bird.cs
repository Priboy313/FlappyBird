using System;

using UnityEngine;

using Systems.Gameplay.Interacable;

namespace Systems.Gameplay.Bird {

	[RequireComponent(typeof(BirdMover))]
	[RequireComponent(typeof(ScoreCounter))]
	[RequireComponent(typeof(BirdCollisionHandler))]
	public class Bird : MonoBehaviour
	{
		private BirdMover _birdMover;
		private ScoreCounter _scoreCounter;
		private BirdCollisionHandler _collisionHandler;

		public event Action GameOver;

		private void Awake()
		{
			_scoreCounter = GetComponent<ScoreCounter>();
			_birdMover = GetComponent<BirdMover>();
			_collisionHandler = GetComponent<BirdCollisionHandler>();
		}

		private void OnEnable()
		{
			_collisionHandler.CollisionDetected += ProcessCollision;
		}

		private void OnDisable()
		{
			_collisionHandler.CollisionDetected -= ProcessCollision;
		}

		private void ProcessCollision(IInteractable interactable)
		{
			if (interactable is Ground)
			{
				GameOver?.Invoke();
				Debug.Log("GameOver!");
			}
		}

		public void Reset()
		{
			_scoreCounter.Reset();
			_birdMover.Reset();
		}
	}
}

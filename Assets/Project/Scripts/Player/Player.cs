using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Weapon))]
public class Player : MonoBehaviour
{
	[SerializeField] private InputReader _input;

	private Health _health;
	private PlayerMover _playerMover;
	private Weapon _weapon;


	public event Action GameOver;

	private void Awake()
	{
		_health = GetComponent<Health>();
		_playerMover = GetComponent<PlayerMover>();
		_weapon = GetComponent<Weapon>();
	}

	private void OnEnable()
	{
		_health.Died += Died;
		
		if (_input != null)
		{
			_input.JumpPressed += _playerMover.Jump;
			_input.ShootPressed += _weapon.Shoot;
		}
	}

	private void OnDisable()
	{
		_health.Died -= Died;

		if (_input != null)
		{
			_input.JumpPressed -= _playerMover.Jump;
			_input.ShootPressed -= _weapon.Shoot;
		}
	}

	private void Died()
	{
		GameOver?.Invoke();
	}
}

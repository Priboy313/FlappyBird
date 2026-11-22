using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private EnemySpawner _enemySpawner;

	private int _score = 0;

	private void OnEnable()
	{
		_enemySpawner.EnemyDied += OnScoreChange;
	}

	private void OnDisable()
	{
		_enemySpawner.EnemyDied -= OnScoreChange;
	}

	private void Start()
	{
		WriteScore(_score);
	}

	private void WriteScore(int score)
	{
		_scoreText.text = $"{score}";
	}

	private void OnScoreChange()
	{
		_score += 1;
		WriteScore(_score);
	}
}

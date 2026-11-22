using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ScriptExporter
{
	// Названия папок, которые нужно игнорировать (без учета регистра).
	// Если потом добавишь TextMeshPro или плагины — сможешь просто дописать их сюда.
	private static readonly string[] ExcludedFolders = { "Editor", "Test", "Tests" };

	[MenuItem("Tools/Export All Scripts")]
	public static void ExportScripts()
	{
		// По умолчанию открываем корень проекта (рядом с папкой Assets)
		string projectRoot = Directory.GetParent(Application.dataPath).FullName;

		// Сохраняем в .txt, чтобы Unity не пытался скомпилировать этот файл как код
		string savePath = EditorUtility.SaveFilePanel(
			"Сохранить все скрипты в один файл",
			projectRoot,
			"AllScripts.txt",
			"txt"
		);

		// Если нажали "Отмена"
		if (string.IsNullOrEmpty(savePath))
			return;

		string normalizedAssetsPath = Path.GetFullPath(Application.dataPath)
			.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

		string[] allFiles = Directory.GetFiles(normalizedAssetsPath, "*.cs", SearchOption.AllDirectories);
		Array.Sort(allFiles, StringComparer.OrdinalIgnoreCase);

		StringBuilder sb = new StringBuilder();
		int exportedCount = 0;

		foreach (string fullPath in allFiles)
		{
			string normalizedFullPath = Path.GetFullPath(fullPath);
			string relativePath = "Assets" + normalizedFullPath
				.Substring(normalizedAssetsPath.Length)
				.Replace('\\', '/');

			if (IsInExcludedFolder(relativePath))
				continue;

			string content = File.ReadAllText(normalizedFullPath, Encoding.UTF8);

			sb.AppendLine($"// Файл: {relativePath}");
			sb.AppendLine(content);
			sb.AppendLine();
			sb.AppendLine();

			exportedCount++;
		}

		File.WriteAllText(savePath, sb.ToString(), Encoding.UTF8);
		Debug.Log($"Готово! Собрано скриптов: {exportedCount}. Файл сохранён: {savePath}");

		// Если файл сохранили всё-таки внутрь Assets — обновляем редактор
		if (Path.GetFullPath(savePath).StartsWith(normalizedAssetsPath, StringComparison.OrdinalIgnoreCase))
		{
			AssetDatabase.Refresh();
		}
	}

	private static bool IsInExcludedFolder(string relativePath)
	{
		string[] parts = relativePath.Split('/');

		// Проверяем только имена папок (до последнего слэша), чтобы случайно 
		// не пропустить скрипт с названием вроде "TestPlayer.cs"
		for (int i = 0; i < parts.Length - 1; i++)
		{
			foreach (string excluded in ExcludedFolders)
			{
				if (string.Equals(parts[i], excluded, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
		}

		return false;
	}
}
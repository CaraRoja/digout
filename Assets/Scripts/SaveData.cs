using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string saveFileName;        // Nome do arquivo de salvamento com slot
    public DateTime saveTime;          // Hor�rio em que o jogo foi salvo
    public float completionPercentage; // Porcentagem de conclus�o do jogo
    public List<float> levelCompletionTimes; // Tempos para completar cada n�vel
    public List<int> levelAttempts;    // N�mero de tentativas por n�vel
    public List<int> scores;           // Pontua��o por n�vel
    public List<int> itemsCollected;   // Itens coletados por n�vel
    public List<int> enemiesDefeated;  // Inimigos derrotados por n�vel
    public int timesMeditated;         // Vezes que o jogador meditou
    public int problemsSolved;         // Vezes que o jogador resolveu problemas
    public List<string> chosenPaths;   // Caminhos escolhidos nos n�veis
    public float totalPlayTime;        // Tempo total de jogo

    public SaveData()
    {
        saveFileName = "DefaultSave_{0}.json";
        ResetData();
    }

    public void ResetData()
    {
        saveTime = DateTime.Now;
        completionPercentage = 0.0f;
        levelCompletionTimes = new List<float>();
        levelAttempts = new List<int>();
        scores = new List<int>();
        itemsCollected = new List<int>();
        enemiesDefeated = new List<int>();
        chosenPaths = new List<string>();
        timesMeditated = 0;
        problemsSolved = 0;
        totalPlayTime = 0.0f;
    }

    public void AddLevelData(float completionTime, int attempts, int score, int items, int enemies, string path)
    {
        levelCompletionTimes.Add(completionTime);
        levelAttempts.Add(attempts);
        scores.Add(score);
        itemsCollected.Add(items);
        enemiesDefeated.Add(enemies);
        chosenPaths.Add(path);
        completionPercentage = (levelCompletionTimes.Count / (float)LevelManager.Instance.levels.Length) * 100;
    }

    public void Save(int slot)
    {
        saveTime = DateTime.Now;
        string json = JsonUtility.ToJson(this);
        string path = Application.persistentDataPath + "/" + string.Format(saveFileName, slot);
        System.IO.File.WriteAllText(path, json);
    }

    public static SaveData Load(int slot)
    {
        string path = Application.persistentDataPath + "/" + string.Format("DefaultSave_{0}.json", slot);
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return new SaveData();
    }
}

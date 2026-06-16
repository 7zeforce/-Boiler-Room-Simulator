using UnityEngine;

public class ResultUpdate : MonoBehaviour
{
    [SerializeField] private GameCanvas _gameCanvas;

    private void OnEnable() => _gameCanvas.OnWin += UpdateResult;

    private void OnDestroy() => _gameCanvas.OnWin -= UpdateResult;

    private void UpdateResult(float time, float point, ComplexityGame complexity, string name)
    {
        CheckingForPassingANewAlarm(name);
        Settings.Instance.Results.Add(new Result 
        {
            Complexity = complexity, 
            PassageTime = time,
            Point = point, 
            NameAlarm = name
        });
    }

    private void CheckingForPassingANewAlarm(string nameAlarm)
    {
        if (Settings.Instance.Results == null) { Settings.Instance.AlarmNumber++; return; }
        for (int i = 0; i < Settings.Instance.Results.Count; i++)
        {
            if (Settings.Instance.Results[i].NameAlarm == nameAlarm) { return; }
        }
        Settings.Instance.AlarmNumber++;
    }
}

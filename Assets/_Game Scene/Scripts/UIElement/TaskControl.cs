using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskControl : MonoBehaviour
{
    public string[] Tasks { get => _tasks; set => _tasks = value; }
    public List<TMP_Text> TextsTask { get => _textsTask; set => _textsTask = value; }

    [SerializeField] private List<TMP_Text> _textsTask;
    [SerializeField] private RectTransform _panelTask;
    [SerializeField] private float _heightText;

    private string[] _tasks;

    private void Start() => Task();

    private void Task()
    {
        int index = 1;
        if (_textsTask.Count != _tasks.Length)
        {
            int max = Mathf.Max(_textsTask.Count, _tasks.Length);
            if (max > _tasks.Length)
            {
                for (int i = _tasks.Length + 1; i < _textsTask.Count; i++)
                {
                    _textsTask[i].gameObject.SetActive(false);
                }
                HeightUpdate(_textsTask.Count - _tasks.Length);
            }
        }
        for (int i = 0; i < _tasks.Length; i++)
        {
            _textsTask[i].text += $"{index}. {_tasks[i]}\n";
            index++;
        }
    }

    private void HeightUpdate(int difference) => _panelTask.position = new Vector2(_panelTask.position.x, _panelTask.position.y - _heightText * difference);
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayResult : MonoBehaviour
{
    public string NameAlarmResult { get => _nameAlarmResult; set => _nameAlarmResult = value; }

    public string NameAlalrm { get => _nameAlarm; set => _nameAlarm = value; }

    public List<TextResult> PanelComplexity { get => _panelComplexity; }

    [SerializeField] private ResultControl _control;

    [Header("Левел панели")]
    [SerializeField] private string _nameAlarmResult;
    [SerializeField] private string _nameAlarm;

    [Header("тексты")]
    [SerializeField] private string[] _text;
    [SerializeField] private TMP_Text _complexityText;
    [SerializeField] private TMP_Text _textName;

    [Header("Список панелей сложности")]
    [SerializeField] private List<TextResult> _panelComplexity;

    private int _complexityIndex = 0;

    private void Start() => _textName.text = _nameAlarm;

    public void UpdateText(float time, float point, ComplexityGame complexity)
    {
        int index = (int)complexity;
        _panelComplexity[index].TimeResult = time;
        _panelComplexity[index].PointResult = point;
        if (complexity != ComplexityGame.Easy)
        {
            _panelComplexity[index].Time.text = string.Format("{0:0}" + ".сек", time);
            _panelComplexity[index].Point.text = $"{point}/100";
        }
        else if(complexity == ComplexityGame.Easy)
        {
            _panelComplexity[index].Time.text = "-";
            _panelComplexity[index].Point.text = $"{point}/100";
        }
    }

    public void ButtonLeftOrRight(string Direction)
    {
       
        switch (Direction)
        {
            case "Right":
                _complexityIndex++;
                UpdateComplexity();
                break;
            case "Left":
                _complexityIndex--;               
                UpdateComplexity();
                break;
        }  
    }

    private void UpdateComplexity()
    {
        if (_complexityIndex >= _text.Length) { _complexityIndex = _text.Length - 1; }
        else if (_complexityIndex <= 0) {  _complexityIndex = 0; }
        _complexityText.text = _text[_complexityIndex];
        for (int i = 0; i < _panelComplexity.Count; i++)
        {
            _panelComplexity[i].gameObject.SetActive(_complexityIndex == i? true : false);
        }
    }
}

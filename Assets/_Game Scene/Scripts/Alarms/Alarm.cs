using TMPro;
using UnityEngine;

public abstract class Alarm : MonoBehaviour
{
    public string Name { get => _name; }

    [SerializeField] private string _name;
    [SerializeField] private string[] _tasks;
    [SerializeField] private string[] _correctAnswers;
    [SerializeField] private InteractionInformation[] _sequencing;
    private InteractionInformation _interactionInfoSelect;
    private int _subsequenceInteraction = 0;
    private bool[] _rightAnswers;
    private int _correctAnswersIndex = 0;
    private bool _correctObject = false;
    private bool _buttonActionOn = false;
    private int _checkClicks = 0;
    private int _badButtons = 0;
    private AlarmCanvas _alarmCanvas;

    protected virtual void Start()
    {
        _alarmCanvas = AlarmCanvas.Instance;
        GameCanvas.UpdateInteractionInformation += UpdateInteractionSelect;
        GameCanvas.BackGame += ViktoryChek;
        _alarmCanvas.OnButtonClick += ButtonClick;
        _alarmCanvas.OnButtonClickActionObject += ButtonActionClick;
        _alarmCanvas.TaskControl.Tasks = _tasks;
        _alarmCanvas.Texts[0].text = _name;
        _rightAnswers = new bool[_tasks.Length];
        for (int i = 0; i < _rightAnswers.Length; i++)
        {
            _rightAnswers[i] = false;
        }
        _alarmCanvas.Texts[2].gameObject.SetActive(false);
        _alarmCanvas.Texts[1].gameObject.SetActive(false);
        _alarmCanvas.AlarmName = _name;
        _alarmCanvas.CorrectAnswers = _correctAnswers[_correctAnswersIndex];
    }

    protected virtual void OnDestroy()
    {
        GameCanvas.UpdateInteractionInformation -= UpdateInteractionSelect;
        GameCanvas.BackGame -= ViktoryChek;
        _alarmCanvas.OnButtonClick -= ButtonClick;
        _alarmCanvas.OnButtonClickActionObject += ButtonActionClick;
    }

    protected virtual void UpdateInteractionSelect(InteractionInformation interactionSelect)
    {
        _interactionInfoSelect = interactionSelect;
        CorrectObject();
    }

    protected virtual void CorrectObject()
    {
        _correctObject =
        _interactionInfoSelect.name == _sequencing[_subsequenceInteraction].name ? true : false;
    }

    protected virtual void ButtonClick(int nomerButton)
    {
        _alarmCanvas.Texts[3].text = _interactionInfoSelect.NameButtons[nomerButton];
        _alarmCanvas.Texts[1].gameObject.SetActive(true);
        _alarmCanvas.Texts[1].text = _interactionInfoSelect.ObjectState[nomerButton];
        switch (_correctObject)
        {
            case true:
                CehekingCorrectButton(nomerButton);
                break;
            case false:
                if (_interactionInfoSelect.ObjectNotInfluencing) { return; }
                _alarmCanvas.Defeat("�� ������� � ������������������");
                break;

        }
    }

    protected virtual void InteractionInfoTextColorUpdate(bool value)
    {
        if (_alarmCanvas.Gamecnavas.ComplexitySelect.ComplexityGame == ComplexityGame.Difficult)
        {
            return;
        }
        _alarmCanvas.Texts[1].GetComponent<TMP_Text>().color = value ? Color.green : Color.red;
    }

    protected virtual void ButtonActionClick()
    {
        _buttonActionOn = true;
        _alarmCanvas.Texts[2].gameObject.SetActive(false);
        _alarmCanvas.Texts[1].text = _interactionInfoSelect.Changestate;
    }

    protected virtual void ViktoryChek()
    {
        _checkClicks = 0;
        _alarmCanvas.Texts[2].gameObject.SetActive(false);
        _alarmCanvas.Texts[1].gameObject.SetActive(false);
        _alarmCanvas.Texts[1].GetComponent<TMP_Text>().color = Color.white;
        if (_subsequenceInteraction != _rightAnswers.Length)
        {
            return;
        }
        float numberOfCorrectAnswers = 0;
        for (int i = 0; i < _rightAnswers.Length; i++)
        {
            if (_rightAnswers[i])
            {
                numberOfCorrectAnswers++;
            }
        }
        if (_badButtons < _rightAnswers.Length)
        {
            _alarmCanvas.Victori(numberOfCorrectAnswers / _rightAnswers.Length * 100);
        }
        else if (_badButtons >= _rightAnswers.Length)
        {
            _alarmCanvas.Defeat("�� �� �������� �� �� ���� ������");
        }
    }

    protected virtual void TaskTextColorUpdate(int subsequenceInteraction, bool interactionSelect)
    {
        _alarmCanvas.TaskControl.TextsTask[subsequenceInteraction - 1].GetComponent<TMP_Text>().color = interactionSelect ? Color.green : Color.red;
    }

    protected virtual void CehekingCorrectButton(int nomerButton)
    {
        if (nomerButton == _interactionInfoSelect.CorrectButtonNumber)
        {
            if (_interactionInfoSelect.ActionObject != null)
            {
                _alarmCanvas.Texts[2].gameObject.SetActive(true);
                _alarmCanvas.Texts[2].text = _interactionInfoSelect.ActionObject;
            }
            InteractionInfoTextColorUpdate(true);
            _checkClicks++;
            _alarmCanvas.Texts[1].text = _interactionInfoSelect.ObjectState[nomerButton];
            if (_checkClicks == 1)
            {
                _rightAnswers[_subsequenceInteraction] = true;
                _subsequenceInteraction++;
                TaskTextColorUpdate(_subsequenceInteraction, true);
            }
        }
        else
        {
            _checkClicks++;
            _badButtons++;
            InteractionInfoTextColorUpdate(false);
            if (_checkClicks == 1)
            {
                _rightAnswers[_subsequenceInteraction] = false;
                _subsequenceInteraction++;
                TaskTextColorUpdate(_subsequenceInteraction, false);
            }
        }
        _correctAnswersIndex++;
        if (_correctAnswersIndex > _correctAnswers.Length - 1) { return; }
        _alarmCanvas.CorrectAnswers = _correctAnswers[_correctAnswersIndex];
    }
}

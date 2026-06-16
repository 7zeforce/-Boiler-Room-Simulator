using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static event Action SynchronizationOptions;
    public static event Action<bool> OnControl;
    public static event Action<InteractionInformation> UpdateInteractionInformation;
    public static event Action BackGame;
    public event Action<float, float, ComplexityGame, string> OnWin;

    public static GameCanvas Instance;

    public Complexity ComplexitySelect { get => _complexitySelect; }

    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Complexity[] _difficultys;
    //0-PlayerInterface
    //1-TextObject
    //2-Loos
    //3-Options
    //4-Task
    //5-Interaction
    //6-FClick
    //7-Aim
    //8-Win
    //9-Timer
    [SerializeField] private TMP_Text _objectText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _objectNameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _pointText;
    [SerializeField] private TMP_Text _explanationText;
    [SerializeField] private Image _imageObject;
    [SerializeField] private AudioSource _buttonClick;
    [SerializeField] private AlarmCanvas _alarmCanvas;
    private float _timer;
    private float _corectPoint;
    private Complexity _complexitySelect;
    private string _filePath = "";

    private void Awake() => Instance = this;

    private void Start()
    {
        _panels[0].SetActive(true);
        for (int i = 1; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
        _panels[9].SetActive(true);
        _panels[7].SetActive(true);
        DisplayObject.OnNameObject += TextUpdateOn;
        PlayerRayCast.OnInteractionObject += InteractionObject;
        PlayerRayCast.OnPanel += OnPanelClickF;
        Pause.OnPause += OnCanvas;
        Settings.Instance.UpdateVolume += UpdateSoundButtonCLick;
        foreach (var difficulty in _difficultys)
        {
            if (difficulty.ComplexityGame == Settings.Instance.Complexity)
            {
                _complexitySelect = difficulty;
            }
        }
        if (_complexitySelect.Time != 0)
        {
            _timer = _complexitySelect.Time;
            OnTimer();
        }
        else
        {
            _panels[9].SetActive(false);
        }
        _panels[4].SetActive(_complexitySelect.DisplayingTask);
        UpdateSoundButtonCLick(Settings.Instance.VolumeSounds);
    }

    private void OnDestroy()
    {
        DisplayObject.OnNameObject -= TextUpdateOn;
        PlayerRayCast.OnInteractionObject -= InteractionObject;
        PlayerRayCast.OnPanel -= OnPanelClickF;
        Settings.Instance.UpdateVolume -= UpdateSoundButtonCLick;
        Pause.OnPause -= OnCanvas;
    }

    private void OnApplicationFocus(bool focus) => Time.timeScale = focus ? 1 : 0;

    public void SetFilePath(string path)
    {
        if (path != "")
        {
            _filePath = path + ".pdf";
        }
        else
        {
            _filePath = "";
        }
    }

    public void ButtonANew()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonBackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading Menu");
    }
    
    public void ButtonBackGame()
    {
        _panels[5].SetActive(false);
        _panels[4].SetActive(_complexitySelect.DisplayingTask);
        _panels[7].SetActive(true);
        if (_panels[4].activeSelf) _panels[1].SetActive(true);
        OnControl?.Invoke(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        BackGame?.Invoke();
    }

    public void OnPanelLoos(string textExplanation)
    {
        _panels[0].SetActive(false);
        _panels[2].SetActive(true);
        _explanationText.text = textExplanation;
        SynchronizationOptions?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void OnPanelWin(float pointAttitude)
    {
        _panels[0].SetActive(false);
        _panels[8].SetActive(true);
        SynchronizationOptions?.Invoke();
        float time = _complexitySelect.Time - _timer;
        _corectPoint = MathF.Round(pointAttitude);
        OnWin?.Invoke(time, _corectPoint, _complexitySelect.ComplexityGame, Settings.Instance.AlarmSelected.name);
        if (_panels[9].activeSelf ==  false)
        {
            _timeText.gameObject.SetActive(false);
            _pointText.text = string.Format("���� ���� -" + "{0:0}" + "/100", _corectPoint);
        }
        else
        {
            _timeText.text = string.Format("���� ����� ����������� - " + "{0:0}" + "�", time);
            _pointText.text = string.Format("���� ���� -" + "{0:0}" + "/100", _corectPoint);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void ButtonClickSound() => _buttonClick.Play();

    public void ButtonReadPDF()
    {
        if (_filePath != "")
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _filePath);
            System.Diagnostics.Process.Start(filePath);
        }
    }

    public void ButttonOptions(bool value) => _panels[3].SetActive(value);

    private IEnumerator TimerGame()
    {
        while (_timer > 0)
        {
            yield return new WaitForSeconds(0.1f);
            _timer -= 0.1f;
            _timerText.text = string.Format("{0:0}", _timer);
        }
        OnPanelLoos("����� �����");
    }

    private void TextUpdateOn(string text, bool value)
    {
        _objectText.text = text;
        if (Time.timeScale == 0) return;
        _panels[1].SetActive(value);
    }

    private void InteractionObject(string name, string description, Sprite imageObject, InteractionInformation interactionInfoSelection)
    {
        UpdateInteractionInformation?.Invoke(interactionInfoSelection);
        OnControl?.Invoke(false);
        _panels[1].SetActive(false);
        _panels[7].SetActive(false);
        _panels[4].SetActive(false);
        _panels[5].SetActive(true);
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        _objectNameText.text = name;
        _descriptionText.text = description;
        _imageObject.sprite = imageObject;
    }

    private void UpdateSoundButtonCLick(float value) => _buttonClick.volume = value / 100;

    private void OnPanelClickF(bool value) => _panels[6].SetActive(value);

    private void OnCanvas(bool value) => _panels[0].SetActive(value);

    private void OnTimer() => StartCoroutine(TimerGame());
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Toggle _music;
    [SerializeField] private Button[] _buttonsNumberAlarm;
    [SerializeField] private Button[] _buttonsComplexity;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject[] _alarms;
    [SerializeField] private AudioSource[] _audioSourses;
    //0-StartPanel
    //1-LevelsPanel
    //2-PanelComplexsity
    //3-ResultPanel
    //4-OptionsPanel
    //5-QuitPanel
    [SerializeField] private DisplayResult[] _displayResults;

    private void Start()
    {
        Settings.Instance.UpdateVolume += SoundsVolumeUpdate;

        for (int i = 0; i < _buttonsNumberAlarm.Length; i++)
        {
            Alarm alarm = _alarms[i].GetComponent<Alarm>();
            _buttonsNumberAlarm[i].GetComponentInChildren<TMP_Text>().text = alarm.Name;
            _displayResults[i].NameAlalrm = alarm.Name;
            _displayResults[i].NameAlarmResult = alarm.gameObject.name;
        } 

        if (Settings.Instance.AlarmNumber < _buttonsNumberAlarm.Length)
        {
            for (int i = Settings.Instance.AlarmNumber; i < _buttonsNumberAlarm.Length; i++)
            {
                _buttonsNumberAlarm[i].interactable = false;
            }
        }
        
        _panels[0].SetActive(true);
        for (int i = 1; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
        SoundsVolumeUpdate(Settings.Instance.VolumeSounds);
        _music.isOn = Settings.Instance.MusicState;
    }

    private void OnDestroy() => Settings.Instance.UpdateVolume -= SoundsVolumeUpdate;

    public void ButtonLevels()
    {
        _panels[0].SetActive(false);
        _panels[1].SetActive(true);
    }

    public void ButtonAlarm(int number)
    {
        Complexitiesbuttons(number);
        _panels[2].SetActive(true);
        Settings.Instance.AlarmSelected = _alarms[number - 1];
    }

    public void ButtonComplexsity(int value)
    { 
        Settings.Instance.Complexity = (ComplexityGame)value;
        if (value == ((int)ComplexityGame.Difficult))
        {
            Settings.Instance.AlarmSelected = null;
            StartGame();
        }
        else
        {
            StartGame();
        }
        
    }

    public void ButtonBeakMenu()
    {
        _panels[0].SetActive(true);
        for (int i = 1; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
    }

    public void OnOfMusic() => Settings.Instance.MusicState = _music.isOn;

    public void BeakAlarms() => _panels[6].SetActive(false);

    public void ButtonOptions() => _panels[4].SetActive(true);

    public void ButtonResult() => _panels[3].SetActive(true);

    public void BeakLevels() => _panels[2].SetActive(false);

    public void ButtonQuit() => _panels[5].SetActive(true);

    public void ButtonYes() => Settings.Instance.QuitGame();

    public void ButtonSound() => _audioSourses[1].Play();

    private void StartGame()
    {
        SceneManager.LoadScene("Loading Game");
        _panels[0].SetActive(false);
    }

    private void Complexitiesbuttons(int correctNumberAlarm)
    {
        for (int i = 1; i < _buttonsComplexity.Length; i++)
        {
            TextButtonComplexity(i, false);
        }
        if (Settings.Instance.Results == null) { return; }
        for(int i = 0; i < Settings.Instance.Results.Count; i++)
        {
            if (Settings.Instance.Results[i].NameAlarm == _alarms[correctNumberAlarm - 1].name)
            {
                if(Settings.Instance.Results[i].Complexity == ComplexityGame.Difficult){ break; }
                TextButtonComplexity((int)Settings.Instance.Results[i].Complexity + 1, true);
            }
        }
    }

    private void TextButtonComplexity(int i,bool OnOf)
    {
        _buttonsComplexity[i].interactable = OnOf;
        switch (OnOf)
        {
            case true:
                _buttonsComplexity[i].GetComponentInChildren<TMP_Text>().alpha = 1f;
                break;
            case false:
                _buttonsComplexity[i].GetComponentInChildren<TMP_Text>().alpha = 0.5f;
                break;
        }
    }

    private void SoundsVolumeUpdate(float value)
    {
        foreach (var sound in _audioSourses)
        {
            sound.volume = value / 100;
        }
    }
}
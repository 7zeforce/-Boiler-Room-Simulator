using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static event Action<bool> OnPause;

    [SerializeField] private GameObject[] _panels;
    //0-Pause
    //1-Options
    //2-Quit?

    private void Start()
    {
        foreach (var panel in _panels)
        {
            panel.SetActive(false); 
        }
        StartCoroutine(StartPause());
    }

    public void ButtonBackMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading Menu");
    }

    public void ButtonContinue() => Continue();

    public void ButtonsOptions(bool value) => _panels[1].SetActive(value);

    public void ButtonsYesOrNot(bool value) => _panels[2].SetActive(value);

    public void ButtonQuit() => Settings.Instance.QuitGame();

    private IEnumerator StartPause()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        _panels[0].SetActive(true);
        OnPause?.Invoke(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Escape));
    }

    private void Continue()
    {
        Time.timeScale = 1;
        _panels[0].SetActive(false);
        OnPause?.Invoke(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(StartPause());
    }
}

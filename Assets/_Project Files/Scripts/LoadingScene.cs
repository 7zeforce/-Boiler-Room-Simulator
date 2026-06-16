using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;
    [SerializeField] private Text _loadingText;
    private AsyncOperation _operation;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    private IEnumerator AsyncLoad()
    {
        string nameScene = SceneManager.GetActiveScene().name == "Loading Game" ? $"Level {1}" : "Menu";
        _operation = SceneManager.LoadSceneAsync(nameScene);

        while (!_operation.isDone)
        {
            float progress = _operation.progress / 0.9f;
            _loadingImage.fillAmount = progress;
            _loadingText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}
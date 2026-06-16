using UnityEngine;

public enum ComplexityGame {Easy, Medium, Difficult}
[CreateAssetMenu(fileName = "Complexity", menuName = "Complexity", order = 1)]
public class Complexity : ScriptableObject
{
    public float Time { get { return _time; } }

    public bool DisplayingTask { get { return _displayingTask; } }

    public ComplexityGame ComplexityGame { get { return _complexityGame; } }

    [SerializeField] private ComplexityGame _complexityGame;
    [SerializeField] private bool _displayingTask;
    [SerializeField] private float _time;
}

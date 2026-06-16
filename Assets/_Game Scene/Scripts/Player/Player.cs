using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Animator Animator { get => _animator; }

    [SerializeField] private Transform _camera;
    [SerializeField] protected Animator _animator;

    private void Awake() => Instance = this;

    private void Start()
    {
        if (Settings.Instance.Complexity != ComplexityGame.Difficult)
        {
            _camera.gameObject.AddComponent<DisplayObject>();
        }
    }
}
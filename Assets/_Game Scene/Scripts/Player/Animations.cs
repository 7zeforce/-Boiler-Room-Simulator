using UnityEngine;

public class Animations : Player
{
    private void Start() => AlarmCanvas.Instance.OnInteraction += InteractionAniamtion;

    private void OnDestroy() => AlarmCanvas.Instance.OnInteraction -= InteractionAniamtion;

    private void LateUpdate() => Animation();

    private void Animation()
    {
        _animator.SetFloat("y", Input.GetAxis("Vertical"));
        _animator.SetFloat("x", Input.GetAxis("Horizontal"));
    }

    private void InteractionAniamtion(bool value)
    {
        if (!value) { return; }
        _animator.SetTrigger("Interaction");
    }
}
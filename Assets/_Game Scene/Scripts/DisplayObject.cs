using System;

public class DisplayObject : PlayerRayCast
{
    public static event Action<string, bool> OnNameObject;

    private DescriptionObject _nameObjectOld;
    private bool _onRayCast = false;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!_raycasting) return;
        if (_onRayCast)
        {
            _nameObjectOld.OutlineOn(false);
        }
        if (_descriptionObject != null)
        {
            _nameObjectOld = _descriptionObject;
            OnNameObject?.Invoke(_nameObjectOld.GetNameObject(),true);
            GameCanvas.Instance.SetFilePath(_nameObjectOld.FilePath);
            _nameObjectOld.OutlineOn(true);
            _onRayCast = true;
        }
        else
        {
            if (!_onRayCast)
            {
                return;
            }
            OnNameObject?.Invoke("", false);
            _nameObjectOld.OutlineOn(false);
            _nameObjectOld = null;
            _onRayCast = false;
        }
    }
}

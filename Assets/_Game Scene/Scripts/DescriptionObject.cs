using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class DescriptionObject : MonoBehaviour
{
    public string FilePath => _interactionInfoSelection.FilePath; 
    public string _nameObject;

    [SerializeField] private InteractionInformation _interactionInfoSelection;
    [SerializeField] private string _descriptionObject;
    [SerializeField] private Sprite _imageObject;
    private Outline _outline;

    private void Awake()
    {
        _outline = gameObject.GetComponent<Outline>();
        _outline.OutlineWidth = 5;
    }

    private void Start() => OutlineOn(false);

    public string GetNameObject() => _nameObject;

    public string GetDescription() => _descriptionObject;

    public Sprite GetImage() => _imageObject;

    public InteractionInformation GetInteractionInfoSelection()
    {
        if( _interactionInfoSelection == null)
        {
            return null;
        }
        else
        {
            return _interactionInfoSelection;
        }
    } 

    public void OutlineOn(bool value) => _outline.enabled = value;
}
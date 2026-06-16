using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInfoSelection", menuName = "infoObjectInteraction", order = 1)]
public class InteractionInformation : ScriptableObject
{
    public string FilePath => _filePath;
    public string[] NameButtons { get { return _nameButtons; } set { _nameButtons = value; } }
    public string[] ObjectState { get { return _objectState; } set { _objectState = value; } }
    public int CorrectButtonNumber { get { return _correctButtonNumber; } set { _correctButtonNumber = value; } }
    public string ActionObject {  get { return _actionObject; } }
    public string Changestate { get { return _changestate; } }
    public bool ObjectNotInfluencing { get { return _objectNotInfluencing; } }

    [SerializeField] private string[] _nameButtons;
    [SerializeField] private string[] _objectState;
    [SerializeField] private string _actionObject;
    [SerializeField] private string _changestate;
    [SerializeField] private bool _objectNotInfluencing;
    [SerializeField] private string _filePath;

    private int _correctButtonNumber;
}

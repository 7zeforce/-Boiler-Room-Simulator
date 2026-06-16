using UnityEngine;

public class AlarmSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] _alarms;
    //0-LowPressureGVS
    //1-HightTemperatureBoiler
    private int _randomIndex;

    private void Awake()
    {
        if(Settings.Instance.AlarmSelected == null)
        {
            _randomIndex = Random.Range(0, _alarms.Length);
            Settings.Instance.AlarmSelected = _alarms[_randomIndex];
            Instantiate(_alarms[_randomIndex]);
        }
        else
        {
            Instantiate(Settings.Instance.AlarmSelected);
        }
    }
}

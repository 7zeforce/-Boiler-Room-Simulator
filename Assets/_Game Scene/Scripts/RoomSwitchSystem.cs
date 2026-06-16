using UnityEngine;

public class RoomSwitchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _roomTextures;
    [SerializeField] private GameObject _roomDontTextures;

    //private void Start()
    //{
    //    ComplexityGame complexuty = Settings.Instance.Complexity;
    //    Switch(complexuty == ComplexityGame.Easy, complexuty != ComplexityGame.Easy);
    //}

    //private void Switch(bool easy, bool dontEasy)
    //{
    //    _roomDontTextures.SetActive(easy);
    //    _roomTextures.SetActive(dontEasy);
    //}
}
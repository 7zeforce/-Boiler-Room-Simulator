using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float _sensetivity;
    private bool _rotation = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        _sensetivity = Settings.Instance.Sensitivity;
        Settings.Instance.UpdateSensitivity += SensetivityUpdate;
        GameCanvas.OnControl += CameraRotationProhibition;
    }

    private void OnDestroy() 
    {
        GameCanvas.OnControl += CameraRotationProhibition;
        Settings.Instance.UpdateSensitivity -= SensetivityUpdate;
    }
    
    private void Update()
    {
        if(!_rotation) return;
        float angleY = _player.gameObject.transform.rotation.eulerAngles.y;
        angleY += _sensetivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        _player.gameObject.transform.rotation = Quaternion.Euler(0, angleY, 0);

        float angleX = gameObject.transform.rotation.eulerAngles.x;
        angleX -= _sensetivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        angleX = angleX < 180? Mathf.Clamp(angleX, -1, 90) : Mathf.Clamp(angleX, 270, 360);

        gameObject.transform.rotation = Quaternion.Euler(angleX,
            _player.gameObject.transform.rotation.eulerAngles.y,
            gameObject.transform.rotation.eulerAngles.z);
    }

    private void CameraRotationProhibition(bool value) => _rotation = value;

    private void SensetivityUpdate(float value) => _sensetivity = value;
}
using UnityEngine;

namespace JasonRPG
{
    public class CameraController : MonoBehaviour
    {
        private float _tilt;

        private void Update()
        {
            float mouseRotation = Input.GetAxis("Mouse Y");
            _tilt = Mathf.Clamp(_tilt - mouseRotation, -15f, 15f);
            transform.localRotation = Quaternion.Euler(_tilt, 0f, 0f);
        }
    }
}

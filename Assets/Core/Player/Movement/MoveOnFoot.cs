using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Core
{
    [Serializable]
    public class MoveOnFoot : IMoveOnFoot
    {
        private float refRotation;
        private Transform _transform;
        private Camera mainCam;
        [Inject] private ICameraHandler _camera;
        public void Move(Transform transform, Vector3 inputDir, float speed)
        {
            if (mainCam == null)
            {
                mainCam = _camera.GetCam();
            }
            if (_transform == null)
            {
                _transform = transform;
            }
            float smoothTurnTime = 0.1f;
            
            if (inputDir.magnitude > 0)
            {
                float yawCam = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + mainCam.transform.eulerAngles.y; 
                float rotataion = Mathf.SmoothDampAngle(transform.eulerAngles.y, yawCam, ref refRotation, smoothTurnTime);
                transform.rotation = Quaternion.Euler(0, rotataion, 0); 
                transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
            }
        }                                                                                           
        
        public void Jump(Rigidbody rigidbody, float jumpForce)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public bool GroundCheck(Transform groundCheck, LayerMask layerGround)
        {
            RaycastHit hit;
            bool isGround = Physics.Raycast(groundCheck.position, Vector3.down, out hit,0.25f ,layerGround);
            
            if (isGround)
            {
                Vector3 onSlopDir = Vector3.ProjectOnPlane(_transform.forward, hit.normal);
                Debug.DrawRay(groundCheck.position,  onSlopDir, Color.green);
            }
            
            return isGround;
        }

        public void InitTransform(Transform transform)
        {
            _transform = transform;
        }
    }

    public interface IMoveOnFoot
    {
        public void Move(Transform transform, Vector3 inputDir, float speed);
        public void Jump(Rigidbody rigidbody, float jumpForce);
        public bool GroundCheck(Transform groundCheck, LayerMask layerGround);

        public void InitTransform(Transform transform);
    }
}
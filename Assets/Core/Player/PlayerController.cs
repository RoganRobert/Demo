using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core
{
    public class PlayerController : MonoBehaviour
    {
        public bool isGround;
        public LayerMask groundMask;
        public Transform groundCheck;
        public MovementState movementState;
        public Vector3 inputDir;
        public float speed;
        public float sprintSpeed;
        
        
        public float jumpForce;
        public Rigidbody rigidbody;

        [SerializeField]
        private PhotonView view;
        
        
        public Transform lookAtPoint;
        public Animator rootAnimator;
        public Animator modelAnim;
        
        #region Dependencies Injection

        [Inject] private IAnimationHandler _animationHandler;
        [Inject] private ICameraHandler _cameraHandler;
        [Inject] private IInputHandler _inputHandler;
        [Inject] private IMoveOnFoot _moveOnFoot;
        #endregion

        public Transform modelTrans;

        
        private void Start()
        {
            modelAnim = transform.GetChild(0).GetComponent<Animator>();
            rootAnimator.avatar = _animationHandler.GetAnimAvatar(modelAnim);
            rootAnimator.runtimeAnimatorController = _animationHandler.GetAnimController(modelAnim);
            _moveOnFoot.InitTransform(transform);
            _animationHandler.GetRootAnim(rootAnimator);
            
            if (view.IsMine)
            {
                _cameraHandler.SetCamFollow(lookAtPoint);
            }
        }
        
        
        public void FixedUpdate()
        {
            if (!view.IsMine) return;
            
            if (movementState == MovementState.OnFoot)
            {
                FootMovement();
            }
        }

       
        private void Update()
        {
            if (!view.IsMine) return;
            isGround = _moveOnFoot.GroundCheck(groundCheck, groundMask);
            inputDir = _inputHandler.GetInput();
            if (_inputHandler.GetJumpInput() && movementState == MovementState.OnFoot)
            {
                if (isGround)
                {
                    _moveOnFoot.Jump(rigidbody, jumpForce);
                }
                
            }
        }
        public void FootMovement()
        {
            if (_inputHandler.IsSprinting())
            { 
                _moveOnFoot.Move(this.transform, inputDir, sprintSpeed); 
                _animationHandler.SetParam(AnimationParam.Speed, $"{sprintSpeed * Mathf.RoundToInt(inputDir.magnitude)}");
            }
            else
            {
                _moveOnFoot.Move(this.transform, inputDir, speed); 
                _animationHandler.SetParam(AnimationParam.Speed, $"{speed *  Mathf.RoundToInt(inputDir.magnitude)}");
            }
            
        }
        public void InitModel()
        {
            
        }
        
    }
    
    public enum MovementState
    {
        OnFoot,
        OnVehicle
    }
}
using UnityEngine;
using UnityEngine.InputSystem;


namespace natsumon
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerView : MonoBehaviour
    {
        CharacterController characterController;
        PlayerInput playerInput;
        Animator animator;

        // 移動速度
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 4f;
        private float rotationSpeed = 70f;

        bool isRunning = false;
        Vector2 input = Vector2.zero;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var velocity = Vector3.zero;

            Vector3 playerDirection = Vector3.Normalize(new Vector3(input.x, 0, input.y));

            // 方向キーの入力があったら、その向きにキャラクターを回転させる
            if (input.magnitude != 0)
            {
                Quaternion characterTargetRotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
            }

            // 移動速度の計算
            float speedByStick = input.magnitude;
            float moveSpeed = walkSpeed * speedByStick;

            if (isRunning)
            {
                moveSpeed *= runSpeed;
            }
            velocity = transform.rotation * playerDirection;

            // アニメーションの速度設定
            animator.SetFloat("Speed", moveSpeed);

            // キャラクターの移動
            characterController.Move(velocity * Time.deltaTime);

        }

        public void OnMove(InputValue value)
        {
            input = value.Get<Vector2>();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            // ボタンが押された瞬間かつ着地している時だけ処理
            // if (!context.performed || !_characterController.isGrounded) return;

            // // 鉛直上向きに速度を与える
            // _verticalVelocity = _jumpSpeed;
        }

        public void OnSprint(InputValue value)
        {
            isRunning = value.isPressed;
        }
    }
}

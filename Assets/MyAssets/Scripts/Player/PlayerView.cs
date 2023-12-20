using UnityEngine;
using UnityEngine.InputSystem;


namespace natsumon
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerFollower playerFollower;
        CharacterController characterController;
        PlayerInput playerInput;
        Animator animator;

        // 移動速度
        private float moveSpeed = 0f;
        private float walkSpeedRatio = 0.01f;
        private float sprintSpeedUpRatio = 0.02f;

        bool isRunning = false;
        Vector2 input = Vector2.zero;
        Vector3 cameraDirection;

        // 重力
        private bool isGroundedPrev;
        private float initSpeed = 2f;
        private float gravity = 5f;
        private float verticalVelocity;

        // ジャンプ
        private float jumpPower = 3f;
        private bool isJumping = false;


        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();

            playerInput.enabled = true;
        }

        void Update()
        {

            Vector3 inputDirection = new Vector3(input.x, 0, input.y);

            // 重力の計算
            calcGravity();

            // キャラクターの向き
            // 入力されたZ軸方向とPlayerFollowerの正面方向、入力されたX軸方向とplayerFollowerの前後方向を正規化した値
            // 重力をY軸に入れる
            Vector3 nextDirection = (playerFollower.transform.forward * inputDirection.z + playerFollower.transform.right * inputDirection.x).normalized;

            // 現在の位置に角度を加算してキャラクターの角度をDirectionの方向へ変える
            var nextPos = transform.position + nextDirection;
            this.transform.LookAt(nextPos);

            // 移動速度計算
            calcMoveSpeed(nextDirection);

            if (!characterController.isGrounded)
            {
                moveSpeed = 2f;
            }

            // キャラクターの移動
            characterController.Move((nextDirection + new Vector3(0, -verticalVelocity, 0)) * Time.deltaTime * moveSpeed);

            // カメラ位置更新
            playerFollower.UpdatePlayerFollower(this.transform.position, cameraDirection);

        }

        private void calcGravity()
        {
            var isGrounded = characterController.isGrounded;
            // 接地直前はスピードを緩める
            if (isGrounded && !isGroundedPrev)
            {
                verticalVelocity = initSpeed;
            }
            else if (!isGrounded) // 接地していなかったら重力の計算をする(落下速度 ＝ 初速度 + 重力加速度 * 時間)
            {
                verticalVelocity += gravity * Time.deltaTime;
                if (gravity < verticalVelocity)
                {
                    verticalVelocity = gravity;
                }
            }
            else if (isJumping) // ジャンプ時
            {
                verticalVelocity = -jumpPower;
                isJumping = false;
            }

            isGroundedPrev = isGrounded;
        }
        private void calcMoveSpeed(Vector3 nextDirection)
        {
            // 移動速度計算
            // 走っている時と、動いているけど走っていない時、何も入力していない時とで分ける
            if (nextDirection.magnitude > 0.1f)
            {
                if (isRunning)
                {
                    moveSpeed = moveSpeed >= 8 ? 8 : moveSpeed + sprintSpeedUpRatio;
                }
                else
                {
                    moveSpeed = moveSpeed >= 2 ? moveSpeed - walkSpeedRatio : moveSpeed + walkSpeedRatio;
                }
            }
            else
            {
                // 入力がない時はスピードを0にする
                moveSpeed = 0f;
            }
            animator.SetFloat("Speed", moveSpeed);
        }

        // InputActionに設定しているActionsのコールバックたち
        public void OnMove(InputValue value)
        {
            input = value.Get<Vector2>();
        }

        public void OnSprint(InputValue value)
        {
            isRunning = value.isPressed;
        }

        public void OnJump(InputValue value)
        {
            if (characterController.isGrounded)
                isJumping = value.isPressed;
        }

        public void OnRotateCamera(InputValue value)
        {
            // 1が右、０が入力なし、−１が左入力
            var yAxis = value.Get<float>();
            cameraDirection = new Vector3(0f, yAxis, 0f);
        }
    }
}

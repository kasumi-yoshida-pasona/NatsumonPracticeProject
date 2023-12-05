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
        private float walkSpeed = 2f;
        private float sprintSpeedUpRatio = 4f;
        private float rotationSpeed = 720f;

        bool isRunning = false;
        Vector2 input = Vector2.zero;
        Vector3 cameraDirection;


        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();

            playerInput.enabled = true;
        }

        void Update()
        {
            Vector3 inputDirection = new Vector3(input.x, 0, input.y);
            // 方向キーの入力がなかったらReturn
            // if (inputDirection == Vector3.zero) return;

            // キャラクターの向き
            // 入力されたZ軸方向とPlayerFollowerの正面方向、入力されたX軸方向とplayerFollowerの前後方向を正規化した値
            Vector3 nextDirection = (playerFollower.transform.forward * inputDirection.z + playerFollower.transform.right * inputDirection.x).normalized;

            // 現在の位置に角度を加算してキャラクターの角度をDirectionの方向へ変える
            var nextPos = transform.position + nextDirection;
            this.transform.LookAt(nextPos);

            // 移動速度
            float moveSpeed = walkSpeed;
            if (isRunning)
                moveSpeed *= sprintSpeedUpRatio;

            // キャラクターの移動
            characterController.Move(nextDirection * Time.deltaTime * moveSpeed);

            // カメラ位置更新
            playerFollower.UpdatePlayerFollower(this.transform.position, cameraDirection);



            // // アニメーションの速度設定
            // animator.SetFloat("MoveSpeed", moveSpeed);
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

        public void OnRotateCamera(InputValue value)
        {
            // 1が右、０が入力なし、−１が左入力
            var yAxis = value.Get<float>();
            cameraDirection = new Vector3(0f, yAxis, 0f);
        }
    }
}

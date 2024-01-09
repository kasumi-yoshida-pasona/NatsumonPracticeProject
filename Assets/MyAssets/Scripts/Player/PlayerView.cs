using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using System;
using Unity.VisualScripting;


namespace natsumon
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerFollower playerFollower;
        CharacterController characterController;
        PlayerInput playerInput;
        Animator animator;

        // 移動速度
        private Vector3 inputDirection;
        private bool isMoveInput = false;
        private float moveSpeed = 0f;

        bool isSprint = false;
        Vector2 input = Vector2.zero;
        Vector3 cameraDirection;

        // 重力
        private bool isGroundedPrev;
        private float verticalVelocity;

        // ジャンプ
        private bool isInitJump = false;

        // 壁登り判定
        private ReactiveProperty<bool> isClimbing = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsClimbing { get { return isClimbing; } }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponentInChildren<Animator>();

            playerInput.enabled = true;
        }

        void Update()
        {

            // 壁登り判定
            Ray lowerRay = new Ray(this.transform.position, this.transform.forward);
            Ray upperRay = new Ray(this.transform.position + new Vector3(0, 1, 0), this.transform.forward);
            isClimbing.Value = Physics.Raycast(lowerRay, 1.4f) && Physics.Raycast(upperRay, 1.2f);

            // デバッグ用。後で消す
            Debug.DrawRay(upperRay.origin, upperRay.direction * 1.2f, Color.red, 1f);
            Debug.DrawRay(lowerRay.origin, lowerRay.direction * 0.4f, Color.red, 1f);

            if (isClimbing.Value)
            {
                climbing();
            }
            else
            {
                movingOnGround();
            }

            // カメラ位置更新
            playerFollower.UpdatePlayerFollower(this.transform.position, cameraDirection);
        }

        // 壁登り時のキャラクターの移動
        public void climbing()
        {
            float moveForward = 0f;
            // 角度調整
            // 頭側のRaycastが足側のRaycastと光線の長さ違うときキャラクターを前方に傾ける
            Ray upperRay = new Ray(this.transform.position + new Vector3(0, 1, 0), this.transform.forward);
            if (!Physics.Raycast(upperRay, 0.4f))
            {
                // 頭部からの距離の計算、0.1ずつ追加していく
                float rayDistance = 0.4f;
                while (!Physics.Raycast(upperRay, rayDistance))
                {
                    rayDistance += 0.1f;
                }
                moveForward = rayDistance;

                // 傾ける位置取得
                RaycastHit hit;
                Physics.Raycast(upperRay, out hit);

                // getAngleで基準点とターゲットの角度
                Vector3 startPos = this.transform.position + this.transform.forward * 0.4f;
                Vector3 targetDir = hit.point - startPos;
                float angle = Vector3.Angle(targetDir, this.transform.up);

                this.transform.Rotate(angle, 0, 0);
                this.transform.position = this.transform.position + this.transform.forward * rayDistance;
            }
            // 入力情報の取得
            inputDirection = new Vector3(input.x, input.y, 0);

            // 入力値によるキャラクターの次の動きの場所
            // 入力されたZ軸方向とPlayerFollowerの正面方向、入力されたX軸方向とplayerFollowerの前後方向を正規化した値
            Vector3 nextDirection = (playerFollower.transform.up * inputDirection.y + playerFollower.transform.right * inputDirection.x + playerFollower.transform.forward).normalized;

            // Sprint状態かどうかでスピード変更
            float climbingVelocity = isSprint ? 2f : 1f;

            // キャラクターの移動
            characterController.Move(nextDirection * Time.deltaTime * climbingVelocity);
        }

        // 接地時のキャラクターの移動
        public void movingOnGround()
        {
            // 入力がある時と入力がないけどmoveSpeedが0fじゃない時に動き続ける
            // inputDirectionを更新するのは動いている時かmoveSpeed＝0fのとき
            if (isMoveInput || moveSpeed == 0f)
                inputDirection = new Vector3(input.x, 0, input.y);

            // キャラクターの向き
            // 入力されたZ軸方向とPlayerFollowerの正面方向、入力されたX軸方向とplayerFollowerの前後方向を正規化した値
            Vector3 nextDirection = (playerFollower.transform.forward * inputDirection.z + playerFollower.transform.right * inputDirection.x).normalized;

            // 現在の位置に角度を加算してキャラクターの角度をDirectionの方向へ変える
            Vector3 nextPos = this.transform.position + nextDirection;
            this.transform.LookAt(nextPos);

            // 重力の計算
            calcGravity();

            // 地面についていれば移動の計算をする
            if (characterController.isGrounded)
            {
                // 移動速度計算
                calcMoveSpeed();
                // キャラクターの移動
                characterController.Move((nextDirection + new Vector3(0, verticalVelocity, 0)) * Time.deltaTime * moveSpeed);
            }
            else
            {
                // キャラクターの移動
                characterController.Move((nextDirection + new Vector3(0, verticalVelocity, 0)) * Time.deltaTime * 2f);
            }
        }

        private void calcGravity()
        {
            float jumpPower = 3f;
            float gravity = 5f;
            var isGrounded = characterController.isGrounded;
            // 接地直前にIsJumpingをfalseにする
            if (isGrounded && !isGroundedPrev)
            {
                animator.SetBool("IsJumping", false);
            }
            else if (isInitJump) // ジャンプ入力時
            {
                verticalVelocity = jumpPower;
                isInitJump = false;
                animator.SetBool("IsJumping", true);
            }
            else if (!isGrounded) // 接地していなかったら重力の計算をする(落下速度 ＝ 初速度 + 重力加速度 * 時間)
            {
                verticalVelocity -= gravity * Time.deltaTime;
                if (gravity <= verticalVelocity)
                    verticalVelocity = gravity;
            }
            isGroundedPrev = isGrounded;
        }
        private void calcMoveSpeed()
        {

            float walkSpeedRatio = 0.01f;
            float sprintSpeedUpRatio = 0.02f;
            // 移動速度計算
            // 走っている時と、動いているけど走っていない時、何も入力していない時とで分ける
            if (isMoveInput)
            {
                if (isSprint)
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
                moveSpeed = moveSpeed <= 0f ? 0f : moveSpeed - sprintSpeedUpRatio * 2;
            }
            // 速度をAnimatorへ
            animator.SetFloat("Speed", moveSpeed);
        }

        // InputActionに設定しているActionsのコールバックたち
        public void OnMove(InputValue value)
        {
            input = value.Get<Vector2>();
            isMoveInput = input.magnitude == 0 ? false : true;
        }

        public void OnSprint(InputValue value)
        {
            isSprint = value.isPressed;
        }

        public void OnJump(InputValue value)
        {
            if (characterController.isGrounded)
                isInitJump = value.isPressed;
        }

        public void OnRotateCamera(InputValue value)
        {
            // 1が右、０が入力なし、−１が左入力
            var yAxis = value.Get<float>();
            cameraDirection = new Vector3(0f, yAxis, 0f);
        }
    }
}

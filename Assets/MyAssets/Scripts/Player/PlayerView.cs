using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace natsumon
{
    public class PlayerView : MonoBehaviour
    {
        CharacterController characterController;
        PlayerInput playerInput;
        Animator animator;

        // 移動速度
        private float walkSpeed = 2f;
        private float sprintSpeedUpRatio = 4f;
        private float rotationSpeed = 720f;

        bool isRunning = false;
        Vector2 input = Vector2.zero;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            var velocity = Vector3.zero;


            // 方向キーの入力があったら、その向きにキャラクターを回転させる
            if (input.magnitude != 0)
            {

                Vector3 playerDirection = Vector3.Normalize(new Vector3(input.x, 0, input.y));
                Quaternion characterTargetRotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
            }

            // 移動速度の計算
            float speedByStick = input.magnitude;
            float moveSpeed = walkSpeed * speedByStick;

            if (isRunning)
            {
                moveSpeed *= sprintSpeedUpRatio;
            }
            velocity = transform.forward * moveSpeed;

            // アニメーションの速度設定
            animator.SetFloat("MoveSpeed", moveSpeed);

            // キャラクターの移動
            characterController.Move(velocity * Time.deltaTime);

            Debug.Log("isRunnging : " + isRunning.ToString());
        }

        public void OnMove(InputValue value)
        {
            input = value.Get<Vector2>();
        }
        public void OnSprint(InputValue value)
        {
            isRunning = value.isPressed;
        }
    }
}

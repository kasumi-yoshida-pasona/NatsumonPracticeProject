using UnityEngine;


namespace natsumon
{
    public class PlayerFollower : MonoBehaviour
    {
        float angle = 0.2f;
        public void UpdatePlayerFollower(Vector3 characterPosition, Vector3 axis)
        {
            // カメラのtransform.position更新
            this.transform.position = characterPosition;

            // 左右の入力がされている間は、入力された方向に角度変更
            this.transform.Rotate(axis, angle);
        }

    }
}

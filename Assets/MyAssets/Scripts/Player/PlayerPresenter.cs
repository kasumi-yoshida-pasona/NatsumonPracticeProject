using UniRx;
using UnityEngine;


namespace natsumon
{
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        private PlayerModel playerModel;

        private void Awake()
        {
            playerModel = new PlayerModel();
        }

        private void Start()
        {
            // 壁登りか接地かを受け取ったらModelへ格納
            playerView.IsClimbing.Subscribe(isClimbing =>
            {
                playerModel.StoreIsClimbing(isClimbing);
            }).AddTo(this);

            // 壁登りか接地かをModelから受け取り、条件分岐してViewへ反映
            playerModel.IsClimbing.Subscribe(isClimbing =>
            {
                if (isClimbing)
                {
                    // 壁登り時の動作を呼び出す
                    Debug.Log($"壁登り時の動作を呼び出す");
                }
                else
                {
                    // 接地時の動作を呼び出す
                    Debug.Log($"接地時の動作を呼び出す");
                }
            }).AddTo(this);
        }

        private void OnDestroy()
        {
            playerModel.Dispose();
        }
    }
}

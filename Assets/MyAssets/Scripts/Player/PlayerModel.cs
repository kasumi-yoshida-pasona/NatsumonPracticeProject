using UniRx;


namespace natsumon
{
    public class PlayerModel
    {
        // 体力ゲージ
        // private ReactiveProperty<float> healthGauge = new ReactiveProperty<float>();
        // public IReadOnlyReactiveProperty<float> HealthGauge { get { return healthGauge; } }
        // 壁登り中かどうか
        private ReactiveProperty<bool> isClimbing = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsClimbing { get { return isClimbing; } }
        // 移動中かどうか
        // private ReactiveProperty<bool> isMoving = new ReactiveProperty<bool>();
        // public IReadOnlyReactiveProperty<bool> IsMoving { get { return isMoving; } }


        // 壁登り状態か接地状態か格納
        public void StoreIsClimbing(bool isClimbingWall)
        {
            isClimbing.Value = isClimbingWall;

        }

        public void Dispose()
        {
            // healthGauge.Dispose();
            isClimbing.Dispose();
            // isMoving.Dispose();
        }
    }
}

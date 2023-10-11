using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // 子Presenter
        [SerializeField] private TitleButtonPresenter titleButtonPresenter;
        [SerializeField] private DialogPresenter dialogPresenter;

        void Start()
        {
            titleButtonPresenter.startBtnPressed().Subscribe(_ => {
            // // ローディングシーンへ移動
            //     onStartBtnPressed?.Invoke();
            });

            titleButtonPresenter.finishBtnPressed().Subscribe(_ => {
                dialogPresenter.SetOnFinishBtnPressed();
            });

        }

        private Action onStartBtnPressed = null;
        public void SetOnStartBtnPressed(Action a)
        {
            onStartBtnPressed = a;
        }

    }

}

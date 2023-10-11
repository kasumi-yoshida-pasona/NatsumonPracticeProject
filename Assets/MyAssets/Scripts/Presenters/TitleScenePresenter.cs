using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // 子Presenter
        [SerializeField] private ButtonPresenter buttonPresenter;
        [SerializeField] private DialogPresenter dialogPresenter;

        void Start()
        {
            buttonPresenter.startBtnPressed().Subscribe(_ => {
            // // ローディングシーンへ移動
            //     onStartBtnPressed?.Invoke();
            });

            buttonPresenter.finishBtnPressed().Subscribe(_ => {
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

using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class LoadingPresenter : MonoBehaviour
    {
        // 親のCanvas
            [SerializeField] Canvas parent;
            [SerializeField] GameObject loadingPrefab;
            private LoadingModel loadingModel;

            // 親Presenterに通知するためのSubject
            private Subject<Unit> initTitleSceneButton = new Subject<Unit>();
            public IObservable<Unit> InitTitleSceneButton() => initTitleSceneButton;

            private GameObject obj;


            private void Awake()
            {
                loadingModel = new LoadingModel();
            }

            void OnDestroy()
            {
                loadingModel.Dispose();
            }

            public void StartLoading()
            {
                obj = Instantiate(loadingPrefab, null);
                // var dialogView = obj.GetComponent<DialogView>();
                // var dialogButtonPresenter = obj.GetComponent<DialogButtonPresenter>();

                // // ゲーム終了確認ダイアログ表示
                // dialogView.ShowDialog(parent, obj);
                // // ダイアログが表示されたことをModelに通知
                // dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);

                // // キャンセルボタン押下
                // dialogButtonPresenter.DialogDestroyed().Subscribe(_ =>
                // {
                //     // ダイアログを壊してタイトルのボタンを初期化
                //     initTitleSceneButton.OnNext(Unit.Default);
                //     Destroy(obj);
                // }).AddTo(obj);

            }
    }
}

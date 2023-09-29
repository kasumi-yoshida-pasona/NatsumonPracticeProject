using natsumon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private ButtonView startBtnView;
        [SerializeField] private ButtonView finishBtnView;

        // Model
        [SerializeField] private ButtonModel buttonModel;
        [SerializeField] private DialogModel dialogModel;

        [SerializeField] private Canvas parent;
        // 表示するダイアログ
        [SerializeField] private DialogView dialog;

        void Awake()
        {
            buttonModel = new ButtonModel();
            dialogModel = new DialogModel();
        }

        void Start()
        {
            // ボタンが選択されたときの処理
            StoreSelectedBtnToModel(startBtnView);
            StoreSelectedBtnToModel(finishBtnView);

            // ボタンが押下されたときの処理
            StorePushedBtnToModel(startBtnView.TargetBtn);
            StorePushedBtnToModel(finishBtnView.TargetBtn);

            // Modelで押下されたボタン情報が変更されたらすべてのボタンをdisabledにする
            buttonModel.PushedBtn
                .Subscribe(PushedBtn => {
                    if (startBtnView.TargetBtn == PushedBtn)
                    {
                        // ローディングシーンへ移動
                    } else if (finishBtnView.TargetBtn == PushedBtn)
                    {
                        // // ゲーム終了確認ダイアログ表示
                        // dialog.ShowDialog(parent);
                        // ダイアログが表示されたことをModelに通知
                        dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
                    }
                }).AddTo(this);

            // dialogModel.StoreShowDialog.Subscribe()
        }

        // Modelに選択されたボタン格納
        private void StoreSelectedBtnToModel(ButtonView Btn)
        {
            Btn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    buttonModel.StoreSelectedBtn(targetBtn);
                })
                .AddTo(this);
        }

        // Modelに押下されたボタン格納
        private void StorePushedBtnToModel(Button Btn)
        {
            Btn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonModel.StorePushedBtn(Btn);
                })
                .AddTo(this);
        }
    }

}

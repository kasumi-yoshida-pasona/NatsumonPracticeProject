using natsumon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        [SerializeField] private TitleScenePresenter titleScenePresenter;
        [SerializeField] private Canvas parent;
        // 表示するダイアログ
        [SerializeField] private DialogView dialog;
        private DialogModel dialogModel;

        private void Awake() {
            dialogModel = new DialogModel();
        }

        private void Start() {
            // titleScenePresenterで登録しているSetOnFinishBtnPressedに引数を渡している
            titleScenePresenter.SetOnFinishBtnPressed(() => {
                        // // ゲーム終了確認ダイアログ表示
                        dialog.ShowDialog(parent);
                        // ダイアログが表示されたことをModelに通知
                        dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
            });
        }
    }

}

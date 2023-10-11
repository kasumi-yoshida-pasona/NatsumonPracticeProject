using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] private Canvas parent;
        // 表示するダイアログ
        [SerializeField] private DialogView dialog;
        private DialogModel dialogModel;

        private void Awake() {
            dialogModel = new DialogModel();
        }

        void OnDestroy()
        {
            dialogModel.Dispose();
        }

        public void SetOnFinishBtnPressed()
        {
            // // ゲーム終了確認ダイアログ表示
            dialog.ShowDialog(parent);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
        }
    }

}

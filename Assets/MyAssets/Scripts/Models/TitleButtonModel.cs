using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TitleScene.Models
{
    public class TitleButtonModel
    {

        /// <summary>
        /// ボタンの状態
        /// status: select / unselect = null / processing
        /// </summary>
        public ReactiveProperty<Button> SelectedBtn = new ReactiveProperty<Button>();

        /// <summary>
        /// ボタンの処理
        /// ボタン押下時に処理中へステータスを変更する
        /// 変更したらPresenterを通してViewへ処理中の情報を渡す
        /// </summary>
        public void ChangeBtnStatusToProcessing(string Btn)
        {
            Debug.Log(Btn);
        }


        /// <summary>
        /// ボタンの処理
        /// ボタン押下時に処理中へステータスを変更する
        /// 変更したらPresenterを通してViewへ処理中の情報を渡す
        /// </summary>
        public void ChangeBtnStatusToSelect(Button Btn)
        {
            SelectedBtn.Value = Btn;

            // Presenterを通してViewに
        }
    }
}

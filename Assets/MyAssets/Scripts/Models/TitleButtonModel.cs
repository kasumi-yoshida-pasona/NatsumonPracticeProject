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
        /// 選択されたボタン
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
        /// ボタン選択時の処理
        /// </summary>
        public void ChangeBtnStatusToSelect(Button Btn)
        {
            SelectedBtn.Value = Btn;
        }
    }
}

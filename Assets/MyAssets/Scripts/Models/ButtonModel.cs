using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace natsumon
{
    public class ButtonModel
    {
        // 選択されたボタン
        public ReactiveProperty<Button> SelectedBtn = new ReactiveProperty<Button>();

        // 押下されたボタン
        public ReactiveProperty<Button> PushedBtn = new ReactiveProperty<Button>();

        // 押下したボタン情報を格納
        public void StorePushedBtn(Button Btn)
        {
            PushedBtn.Value = Btn;
        }

        // 選択したボタン情報を格納
        public void StoreSelectedBtn(Button Btn)
        {
            SelectedBtn.Value = Btn;
        }
    }
}

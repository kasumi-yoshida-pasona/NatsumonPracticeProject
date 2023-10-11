using UnityEngine.UI;
using UniRx;
using System;

namespace natsumon
{
    public class ButtonModel : IDisposable
    {
        // 選択されたボタン
        private ReactiveProperty<Button> selectedBtn = new ReactiveProperty<Button>();
        public IReadOnlyReactiveProperty<Button> SelectedBtn { get {return selectedBtn;}}

        // 押下されたボタン
        private ReactiveProperty<Button> pushedBtn = new ReactiveProperty<Button>();
        public IReadOnlyReactiveProperty<Button> PushedBtn { get {return pushedBtn;}}


        // 押下したボタン情報を格納
        public void StorePushedBtn(Button Btn)
        {
            pushedBtn.Value = Btn;
        }

        // 選択したボタン情報を格納
        public void StoreSelectedBtn(Button Btn)
        {
            selectedBtn.Value = Btn;
        }

        public void Dispose()
        {
            selectedBtn.Dispose();
            pushedBtn.Dispose();
        }

    }
}

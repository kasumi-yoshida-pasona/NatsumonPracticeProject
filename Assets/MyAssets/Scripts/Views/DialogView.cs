using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace natsumon
{
    public class DialogView : MonoBehaviour
    {
        public void ShowDialog(Canvas parent)
        {
            var _dialog = Instantiate(this);
            _dialog.transform.SetParent(parent.transform, false);
        }
    }
}

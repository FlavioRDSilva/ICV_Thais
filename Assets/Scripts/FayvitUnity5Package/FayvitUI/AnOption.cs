using UnityEngine;
using UnityEngine.UI;

namespace FayvitUI
{
    public class AnOption : MonoBehaviour
    {
        [SerializeField] private Image spriteDoItem;

        public Image SpriteDoItem { get { return spriteDoItem; } set { spriteDoItem = value; } }

        protected System.Action<int> ThisAction { get; set; }

        public virtual void InvokeAction()
        {
            if(ThisAction!=null)
                ThisAction(transform.GetSiblingIndex() - 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.UI.Structure
{
    public class BasePanel : MonoBehaviour
    {
        protected bool isRemove;
        protected new string name;

        public virtual void OpenPanel(string name)
        {
            this.name = name;
            gameObject.SetActive(true);
        }

        public virtual void ClosePanel(string name)
        {
            isRemove = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Demo.Optimizes
{
    /// <summary>
    /// Lambda表达式的gc问题
    /// </summary>
    public class GCLambda
    {
        #region 演示用变量与方法
        private int _memberCount = 0;
        private static int _staticCount = 0;
        private void IncrementMemberCount()
        {
            _memberCount++;
        }
        // static method
        private static void IncrementStaticCount()
        {
            _staticCount++;
        }

        private void InvokeActionMethod(System.Action action)
        {
            action.Invoke();
        }
        #endregion

        //调用委托
        private void Method01()
        {
            /**
           * 这里将演示使用lambda表达式时，哪些操作会触发GC.Alloc
           * **/

            //使用成员变量与局部变量时会触发GC
            InvokeActionMethod(() => { _memberCount++; });//会触发gc
            int count = 0;
            InvokeActionMethod(() => { count++; });//会触发gc

            //使用静态变量时可以避免GC
            InvokeActionMethod(() => { _staticCount++; });//不会触发gc


            //使用匿名委托，成员方法，静态方法都会触发gc
            InvokeActionMethod(() => { IncrementMemberCount(); });//会触发gc
            InvokeActionMethod(IncrementMemberCount);//会触发gc
            InvokeActionMethod(IncrementStaticCount);//会触发gc

            //匿名委托的静态方法不会触发gc
            InvokeActionMethod(() => { IncrementStaticCount(); });//不会触发gc

            /**
             * Action仅在第一次是新的，但它在内部缓存以避免第二次GC.Alloc
             * 然而，从代码安全性和可读性的角度来看，将所有变量和方法都设置为静态是不太容易接受的。在需要快速的代码中，
             * 对于每帧或不确定时间触发的事件，不使用lambda表达式的设计更安全，而不是使用大量静态来消除GC.Alloc
             * 
             * **/
        }
     


    }
}

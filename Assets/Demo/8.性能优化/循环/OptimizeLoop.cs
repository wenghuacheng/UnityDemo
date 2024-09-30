using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Optimizes
{
    /// <summary>
    /// 循环优化
    /// </summary>
    public class OptimizeLoop
    {

        private void Method01()
        {
            List<int> list = new List<int>(128);

            /**
             * 优化1：如果Count在循环中没有改变，那么可以通过在循环之前缓存属性访问来减少属性访问的负载
             * **/
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                var val = list[i];
            }

            /**
             * 优化2：大数据量操作列表时linq比普通方式慢19倍
             * 但是linq编写方便。unity中有避免使用LINQ的情况，可以参考
             * **/
            foreach (var i in list)
            {
                if (i % 2 == 0)
                {
                    var _ = i * i;
                }
            }

            var query = list.Where(i => i % 2 == 0).Select(i => i * i);
            foreach (var i in query)
            {
            }
        }
    }
}

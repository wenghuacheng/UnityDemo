using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Patterns.Composites
{
    public interface IExecutor
    {
        void Do();
    }

    public class BuildAxe : IExecutor
    {
        public void Do()
        {
            Debug.WriteLine("制作斧头");
        }
    }

    public class ConsumeMaterial : IExecutor
    {
        public void Do()
        {
            Debug.WriteLine("消耗材料");
        }
    }

    public class AddInventory : IExecutor
    {
        public void Do()
        {
            Debug.WriteLine("添加库存");
        }
    }

    //将子项进行组合
    public class ExecutorComposite : IExecutor
    {
        private List<IExecutor> executors = new List<IExecutor>() { new BuildAxe(), new ConsumeMaterial(), new AddInventory() };
        public void Do()
        {
            foreach (var item in executors)
            {
                item.Do();

            }
        }

    }
}
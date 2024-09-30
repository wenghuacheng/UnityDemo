using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Optimizes
{
    /// <summary>
    /// 泛型委托
    /// </summary>
    public class GCLambdaGeneric
    {

        #region 泛型委托

        /**
         * 如果struct没有实现IEquatable接口，却被指定为T，那么它将被强制转换为带有参数Equals的object，从而导致装箱
         * **/

        //错误演示。没有指定where导致装箱
        public readonly struct GenericStructError<T> : IEquatable<T>
        {
            private readonly T _value;
            public GenericStructError(T value)
            {
                _value = value;
            }
            public bool Equals(T other)
            {
                var result = _value.Equals(other);
                return result;
            }
        }

        //正确演示。通过使用where子句(泛型类型约束)将T可以接受的类型限制为那些实现了IEquatable的类型，可以防止这种意外的装箱
        public readonly struct GenericStruct<T> : IEquatable<T> where T : IEquatable<T>
        {
            private readonly T _value;
            public GenericStruct(T value)
            {
                _value = value;
            }
            public bool Equals(T other)
            {
                var result = _value.Equals(other);
                return result;
            }
        }

        #endregion


        /**
         * 其他优化说明：
         * 使用struct可以避免GC，但是struct传输会造成复制（值类型），如果对象很大使用class的引用方式传输会比赋值一个大的struct性能好很多
         */
    }
}

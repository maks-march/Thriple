using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Thriple.Objects
{
    public enum BlockType
    {
        Idle,
        Red,
        Green,
        Blue,
        Yellow,
        White
    }

    public static class EnumExpression
    {
        public static int GetEnumMax(this Enum _enum)
        {
            var type = _enum.GetType();
            var count = type.GetFields().Count() - 1;
            return count;
        }
    }
}

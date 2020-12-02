using System;

namespace HealthBridgeClinical.Common.Utils
{
    public class Validate
    {
        // instance

        public void Throw<T>(Func<T> _exception) where T : Exception
        {
            if (this == _throw)
            {
                throw _exception();
            }
        }

        // static

        private static readonly Validate _throw = new Validate();
        private static readonly Validate _dontThrow = new Validate();

        public static Validate If(Func<bool> _if)
        {
            return _if() ? _throw : _dontThrow;
        }

        public static Validate If(bool _if)
        {
            return _if ? _throw : _dontThrow;
        }

        public static Validate IfNullOrWhitespace(object value)
        {
            bool isNullOrWhiteSpace()
            {
                if (value is string valueString)
                {
                    return string.IsNullOrWhiteSpace(valueString);
                }
                else
                {
                    return (value == null || value == DBNull.Value);
                }
            }

            return isNullOrWhiteSpace() ? _throw : _dontThrow;
        }
    }
}

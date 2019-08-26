using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing
{
    public class SequenceGeneratorGlobal
    {
        private static object _syncRoot = new object();
        private Decimal _sequence;
        private readonly Decimal _minValue;
        private readonly Decimal _maxValue;
        private readonly string _prefix;
        private readonly string _suffix;
        private static SequenceGeneratorGlobal _instance;

        public static SequenceGeneratorGlobal GetInstance()
        {
            lock (SequenceGeneratorGlobal._syncRoot)
            {
                if (SequenceGeneratorGlobal._instance == null)
                    SequenceGeneratorGlobal._instance = new SequenceGeneratorGlobal(Decimal.One, new Decimal(9999999999L), (string)null, (string)null);
                return SequenceGeneratorGlobal._instance;
            }
        }

        private SequenceGeneratorGlobal(
          Decimal minValue,
          Decimal maxValue,
          string prefix,
          string suffix)
        {
            this._sequence = minValue;
            this._minValue = minValue;
            this._maxValue = maxValue;
            this._prefix = prefix;
            this._suffix = suffix;
        }

        public Decimal Sequence
        {
            get
            {
                return this._sequence;
            }
        }

        public Decimal MinValue
        {
            get
            {
                return this._minValue;
            }
        }

        public Decimal MaxValue
        {
            get
            {
                return this._maxValue;
            }
        }

        public Decimal Next()
        {
            Decimal num = this._sequence++ % this.MaxValue;
            if (!(this._sequence > this._maxValue))
                return num;
            this._sequence = this._minValue;
            return num;
        }
    }
}

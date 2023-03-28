using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineJukebox.Wave
{
    internal class WaveHeaderProperty
    {
        public readonly int StartPos;
        private readonly int EndPos;
        private readonly byte[] Value;

        public WaveHeaderProperty(int startPos, int endPos, byte[] value)
        {
            StartPos = startPos;
            EndPos = endPos;
            Value = value;
        }

        public int GetStartPos() { return StartPos; }
        public int GetEndPos() { return EndPos; }
        public byte[] GetValue() { return Value; }
    }
}

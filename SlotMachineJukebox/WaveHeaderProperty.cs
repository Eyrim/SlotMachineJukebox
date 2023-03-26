using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineJukebox
{
    internal class WaveHeaderProperty
    {
        public readonly int StartPos;
        private readonly int EndPos;
        private readonly byte[] Value;

        public WaveHeaderProperty(int startPos, int endPos, byte[] value)
        {
            this.StartPos = startPos;
            this.EndPos = endPos;
            this.Value = value;
        }

        public int GetStartPos() { return this.StartPos; }
        public int GetEndPos() { return this.EndPos; }
        public byte[] GetValue() { return this.Value; }
    }
}

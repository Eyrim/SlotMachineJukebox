using SlotMachineJukebox.Util;
using SlotMachineJukebox.Generation;

using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Multimedia;
using SlotMachineJukebox.Wave;

namespace SlotMachineJukebox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Testing \/\/\/ 

            /*WaveHeader a = new WaveHeader(44100, 4, 4, 4);
            a.ToBytes();*/

            Pattern pattern = NoteGenerator.CreateRandomPattern("", 4, 4, Octave.Get(4), 1000000);

            MidiFile randomMidi = pattern.ToFile(TempoMap.Create(new TicksPerQuarterNoteTimeDivision(4), Tempo.FromBeatsPerMinute(400)));

            randomMidi.Write(new FileStream("testRandom.mid", FileMode.OpenOrCreate));
        }
    }
}
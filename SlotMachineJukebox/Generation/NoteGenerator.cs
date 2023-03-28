using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.MusicTheory;
using System;

namespace SlotMachineJukebox.Generation
{
    internal class NoteGenerator
    {
        public static Pattern CreateRandomPattern(string key, int numerator, int denominator, Octave octave, int numberOfNotes)
        {
            PatternBuilder builder = new PatternBuilder();
            

            for (int i = 0; i < numberOfNotes; i++)
            {
                builder.Note(GetRandomNote(GetRandomOctave()));   
            }

            return builder.Build();
        }

        private static Octave GetRandomOctave()
        {
            Random random = new Random();

            return Octave.Get(random.Next(2, 9));
        }

        private static Note GetRandomNote(Octave octave)
        {
            octave.A.Transpose(Interval.GetUp((SevenBitNumber) 1));

            Random random = new Random();
            int rand = random.Next(1, 13);

            switch (rand)
            {
                case 1:
                    return octave.A;

                case 2:
                    return octave.ASharp;

                case 3:
                    return octave.B;

                case 4:
                    return octave.C;

                case 5:
                    return octave.CSharp;

                case 6:
                    return octave.D;

                case 7:
                    return octave.DSharp;

                case 8:
                    return octave.E;

                case 9:
                    return octave.F;

                case 10:
                    return octave.FSharp;

                case 11:
                    return octave.G;

                case 12:
                    return octave.GSharp;
            }

            return null;
        }
    }
}

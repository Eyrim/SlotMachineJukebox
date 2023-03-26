using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachineJukebox
{
    /// <summary>
    /// Represents the header of a WAVE file given this documentation
    /// http://soundfile.sapp.org/doc/WaveFormat/
    /// https://docs.fileformat.com/audio/wav/
    /// </summary>
    internal class WaveHeader
    {
        private Dictionary<string, WaveHeaderProperty> HeaderProperties = new Dictionary<string, WaveHeaderProperty>();
        private int SampleRate;
        private int BitDepth;
        private int Channels;
        private int NumSamples;

        
        public WaveHeader(int sampleRate, int bitDepth, int channels, int numSamples)
        {
            this.SampleRate = sampleRate;
            this.BitDepth = bitDepth;
            this.Channels = channels;
            this.NumSamples = numSamples;

            int fileSize;
            int byteRate = CalculateByteRate(sampleRate, bitDepth, channels);
            int blockAlign = CalculateBlockAlign(channels, bitDepth);
            int dataSize = CalculateDataSize(numSamples, channels, bitDepth);

            // ChunkID
            HeaderProperties.Add("ChunkId", new WaveHeaderProperty(1, 4, GetBytes("RIFF")));
            // ChunkSize (written after file creation)
            HeaderProperties.Add("ChunkSize", null);
            // Format
            HeaderProperties.Add("Format", new WaveHeaderProperty(9, 12, GetBytes("WAVE")));
            // SubChunk1ID (format chunk marker)
            HeaderProperties.Add("SubChunk1ID", new WaveHeaderProperty(13, 16, GetBytes(new char[] {'0', 'f', 'm', 't'})));
            // SubChunk1Size (size of the format chunk)
            HeaderProperties.Add("SubChunk1Size", new WaveHeaderProperty(17, 20, GetBytes(16)));
            // Audio Format
            HeaderProperties.Add("AudioFormat", new WaveHeaderProperty(21, 22, GetBytes(1)));
            // NumChannels
            HeaderProperties.Add("NumChannels", new WaveHeaderProperty(23, 24, GetBytes(channels)));
            // SampleRate
            HeaderProperties.Add("SampleRate", new WaveHeaderProperty(25, 28, GetBytes(sampleRate)));
            // ByteRate
            HeaderProperties.Add("ByteRate", new WaveHeaderProperty(29, 32, GetBytes(byteRate)));
            // BlockAlign
            HeaderProperties.Add("BlockAlign", new WaveHeaderProperty(33, 34, GetBytes(blockAlign)));
            // BitsPerSample
            HeaderProperties.Add("BitsPerSample", new WaveHeaderProperty(35, 36, GetBytes(bitDepth)));
            // SubChunk2ID
            HeaderProperties.Add("SubChunk2ID", new WaveHeaderProperty(37, 40, GetBytes(new char[] {'d', 'a', 't', 'a'})));
            // SubChunk2Size (written after file creation) [Number of bytes in the data]
            HeaderProperties.Add("SubChunk2Size", null);
        }

        public void SetFileSize()
        {
            int subChunk1Size = 16;
            int subChunk2Size = CalculateDataSize(this.NumSamples, this.Channels, this.BitDepth);
            int fileSize = CalculateFileSize(subChunk1Size, subChunk2Size);
            
            HeaderProperties["ChunkSize"] = new WaveHeaderProperty(5, 8, BitConverter.GetBytes(fileSize));
        }

        public void SetDataSize()
        {
            int dataSize = CalculateDataSize(this.NumSamples, this.Channels, this.BitDepth);

            HeaderProperties["SubChunk2Size"] = new WaveHeaderProperty(41, 44, GetBytes(dataSize));
            
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[44];
            WaveHeaderProperty currentProperty;
            int count = 0;

            foreach (KeyValuePair<string, WaveHeaderProperty> property in HeaderProperties)
            {
                currentProperty = property.Value;

                if (currentProperty == null)
                {
                    throw new NullReferenceException($"Header Property for {property.Key} was null.");
                }

                for (int i = currentProperty.GetStartPos() - 1; i < currentProperty.GetEndPos(); i++)
                {
                    bytes[i] = currentProperty.GetValue()[count];

                    count++;
                }

                count = 0;
            }

            return bytes;
        }

        private static int CalculateFileSize(int subChunk1Size, int subChunk2Size)
        {
            return 4 + (9 + subChunk1Size) + (8 + subChunk2Size);
        }

        private static int CalculateByteRate(int sampleRate, int bitDepth, int numChannels)
        {
            return (sampleRate * bitDepth * numChannels) / 8;
        }

        private static int CalculateBlockAlign(int numChannels, int bitDepth)
        {
            return (numChannels * bitDepth) / 8;
        }

        private static int CalculateDataSize(int numSamples, int numChannels, int bitDepth)
        {
            return (numSamples * numChannels * bitDepth) / 8;
        }

        private static byte[] GetBytes(int toConvert)
        {
            return BitConverter.GetBytes(toConvert);
        }

        private byte[] GetBytes(char[] toConvert)
        {
            return Encoding.ASCII.GetBytes(toConvert);
        }

        private byte[] GetBytes(string toConvert)
        {
            return Encoding.ASCII.GetBytes(toConvert);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace SlotMachineJukebox.Util
{
    /// <summary>
    /// Class used to interact with files
    /// </summary>
    internal class FileHandling
    {
        /// <summary>
        /// Reads a given file to a string[]
        /// </summary>
        /// <param name="path">The location of the file as a file path</param>
        /// <returns>A string array where each element is a line in the file</returns>
        /// <exception cref="FileNotFoundException">If the file doesn't exist or no permission</exception>
        public static String[] ReadFileToList(string path)
        {
            // If the file doesn't exist don't try to read it
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"No file found at: {path}. Check the path, and permissions");
            }

            List<string> fileData = new List<string>();
            string? currentLine = "";

            using (StreamReader sr = new StreamReader(path))
            {
                while ((currentLine = sr.ReadLine()) != null)
                {
                    fileData.Add(currentLine);
                }
            }

            return fileData.ToArray();
        }

        /// <summary>
        /// Reads a given file to a string
        /// </summary>
        /// <param name="path">The location of the file as a file path</param>
        /// <returns>String containing the full file contents</returns>
        /// <exception cref="FileNotFoundException">If the file doens't exist or no permission</exception>
        public static String ReadFileToString(string path)
        {
            // If the file doesn't exist, don't try and read it
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"No file found at: {path}. Check the path, and permissions");
            }

            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Appends a byte array to a file
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <param name="bytes">The byte array to write to the file</param>
        /// <exception cref="FileNotFoundException">If the file doesn't exist or no permission</exception>
        /// <exception cref="FileLoadException">If the FileStream instance doesn't support writing</exception>"
        public static bool WriteBytes(string path, Byte[] bytes)
        {
            //TODO: Add a checksum to ensure the file was written correctly

            // Check the file exists
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"No file found at: {path}. Check the path, and permissions");
            }


            // Open a file stream in append mode
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                // Only write the bytes if the current instance supports it
                if (!fs.CanWrite)
                {
                    throw new FileLoadException($"Can not write to file {path}");
                }


                // Write the file byte by byte
                for (int i = 0; i < bytes.Length; i++)
                {
                    fs.WriteByte(bytes[i]);
                }

                return true;
            }
        }
    }
}

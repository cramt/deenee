using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Deenee {
    /// <summary>
    /// class to make sure Console.Writeline writes to Debug.Log
    /// it inherits TextWriter
    /// </summary>
    public class UnityLogWriter : TextWriter {
        /// <summary>
        /// define encoding type
        /// </summary>
        public override Encoding Encoding { get; } = Encoding.UTF8;
        /// <summary>
        /// the buffer of the string
        /// </summary>
        string buffer = "";
        /// <summary>
        /// when we get a char
        /// </summary>
        /// <param name="value"></param>
        public override void Write(char value) {
            base.Write(value);
            //add it to buffer
            buffer += value;
            //if we have a newline char
            while (buffer.Contains(Environment.NewLine)) {
                var split = buffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                Debug.Log(split[0]);
                split.RemoveAt(0);
                buffer = string.Join(Environment.NewLine, split.ToArray());
            }
        }
    }
}
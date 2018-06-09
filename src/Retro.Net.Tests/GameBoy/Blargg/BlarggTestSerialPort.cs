using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Interfaces;

namespace Retro.Net.Tests.GameBoy.Blargg
{
    public class BlarggTestSerialPort : ISerialPort
    {
        private readonly IList<char> _currentWord;

        private readonly BlockingCollection<string> _wordQueue;

        public BlarggTestSerialPort()
        {
            _currentWord = new List<char>();
            _wordQueue = new BlockingCollection<string>();
            Words = new List<string>();
        }

        public IList<string> Words { get; }

        public void Connect(ISerialPort serialPort)
        {
        }

        public void Disconnect()
        {
        }

        public byte Transfer(byte value)
        {
            var c = (char)value;
            Console.Write(c);

            if (!char.IsWhiteSpace(c))
            {
                _currentWord.Add(c);
                return 0x00;
            }

            if (!_currentWord.Any())
            {
                return 0x00;
            }

            var word = new string(_currentWord.ToArray());
            _wordQueue.Add(word);
            _currentWord.Clear();
            return 0x00;
        }

        public string WaitForNextWord(CancellationToken cancellationToken)
        {
            var result = _wordQueue.TryTake(out var word, Timeout.Infinite, cancellationToken);
            if (result)
            {
                Words.Add(word);
            }

            return result ? word : null;
        }
    }
}

using System;
using System.Threading;

namespace example.Utilities
{
    public class Spinner : IDisposable
    {
        private readonly string[] _sequence;
        private int _counter = 0;
        private readonly int _delay;
        private bool _active;
        private readonly Thread _thread;
        private readonly string _message;
        private readonly Random _rand;

        public Spinner(string message)
        {
            _sequence = new[] {"/", "-", @"\", "|"};
            _message = message;
            _delay = 75;
            _thread = new Thread(Spin);
            _rand = new Random();
        }

        public Spinner(string message, string[] sequence)
        {
            _sequence = sequence;
            _message = message;
            _delay = -1;
            _thread = new Thread(Spin);
            _rand = new Random();
        }

        public void Start()
        {
            _active = true;
            if (!_thread.IsAlive)
            {
                _thread.Start();
            }
        }

        public void Stop()
        {
            _active = false;
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            Console.Write($"\r{_message}... DONE\n");
        }

        private void Spin()
        {
            while (_active)
            {
                var item = _sequence[++_counter % _sequence.Length];
                
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.Write($"\r{_message}... {item}");

                Thread.Sleep(_delay == -1 ? _rand.Next(500, 2500) : _delay);
            }
        }

   
        public void Dispose()
        {
            Stop();
        }
    }
}
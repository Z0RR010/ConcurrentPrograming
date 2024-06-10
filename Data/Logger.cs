using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Data
{
    public class Logger
    {
        private readonly string _logFilePath;
        private readonly ConcurrentQueue<LogBall> _ballsDataQueue;
        private readonly JArray _jLogArray = new JArray();
        private readonly int _queueSize = 25;
        private CancellationTokenSource _queChange = new CancellationTokenSource();
        private bool _queOverflow = false;
        private bool _saveData;

        private static Logger? _instance = null;
        public static Logger GetInstance()
        {
            _instance ??= new Logger();
            return _instance;
        }
        private Logger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            _logFilePath = Path.Combine(path, "BallsLog.json");
            _ballsDataQueue = new ConcurrentQueue<LogBall>();

            using (FileStream LogFile = File.Create(_logFilePath))
            {
                LogFile.Close();
            }

            _saveData = true;
            Task.Run(CollectData);
        }

        public void AddBallToQueue(IBallType ball, long time)
        {
            LogBall logBall = new LogBall(ball.Position, ball.Speed, time);
            lock (_ballsDataQueue)
            {
                if (_ballsDataQueue.Count < _queueSize)
                {
                    _ballsDataQueue.Enqueue(logBall);
                    _queChange.Cancel();
                }
                else
                {
                    _queOverflow = true;
                }
            }
            
            
        }

        private async void CollectData()
        {
            while (_saveData)
            {
                if (!_ballsDataQueue.IsEmpty)
                {
                    lock (_ballsDataQueue)
                    {
                        while (_ballsDataQueue.TryDequeue(out LogBall serializedObject))
                        {
                            JObject jsonObject = JObject.FromObject(serializedObject);
                            jsonObject["Position"] = serializedObject.Position.ToString();
                            jsonObject["Speed"] = serializedObject.Speed.ToString();
                            _jLogArray.Add(jsonObject);
                            if (_queOverflow)
                            {
                                JObject errorMessage = new JObject
                                {
                                    ["Error"] = "Buffer size is too small skipped logging"
                                };
                                _jLogArray.Add(errorMessage);
                                _queOverflow = false;
                            }
                        }
                    }

                    if (_jLogArray.Count > _queueSize / 2)
                    {
                        SaveToFile();
                    }
                }
                await Task.Delay(Timeout.Infinite, _queChange.Token).ContinueWith(_ => { });

                if (_queChange.IsCancellationRequested)
                {
                    _queChange = new CancellationTokenSource();
                }
            }
        }
        private async void SaveToFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(JsonConvert.SerializeObject(_jLogArray, Formatting.Indented));
            _jLogArray.Clear();
            await File.AppendAllTextAsync(_logFilePath, stringBuilder.ToString(), Encoding.UTF8);
            stringBuilder.Clear();
        }
    }
}
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Base;
using Logic;
using Model;

namespace ViewModel
{
    
        public class ViewModelWindow : ViewModelBase
        {
            public ViewModelWindow() : this(ModelAbstractApi.CreateApi()) { }

            private ViewModelWindow(ModelAbstractApi modelLayer)
            {
                // Fields initialization
                LogicAbstractApi logicLayer = new LogicApi();
                _balls = new ObservableCollection<Ball>();
                _ballRadius = modelLayer.BallRadius;
                _tableWidth = modelLayer.TableWidth;
                _tableHeight = modelLayer.TableHeight;
                var timer = new System.Timers.Timer();

                // Commands initialization
                
                GenerateCommand = new RelayCommand(() => logicLayer.GenerateHandler(Balls, BallsNumber, _ballRadius, _tableWidth - _ballRadius, _ballRadius, _tableHeight - _ballRadius));
                StartMoving = new RelayCommand(() => logicLayer.MovingHandler(Balls, timer, _ballRadius, _tableWidth, _tableHeight));
                StopMoving = new RelayCommand(() => logicLayer.Stop(timer));
                ClearBoard = new RelayCommand(() => logicLayer.ClearBalls(timer, Balls));
            }

            private int _ballsNumber;
            private readonly ObservableCollection<Ball> _balls;
            private int _ballRadius;
            private readonly int _tableWidth;
            private readonly int _tableHeight;

            public int BallsNumber
            {
                get => _ballsNumber;
                set
                {
                    if (value == _ballsNumber) return;
                    _ballsNumber = value;
                    RaisePropertyChanged();
                }
            }

            public int BallRadius
            {
                get => _ballRadius;
                set
                {
                    if (value == _ballRadius) return;
                    _ballRadius = value;
                    RaisePropertyChanged();
                }
            }

            public ObservableCollection<Ball> Balls
            {
                get => _balls;
                set
                {
                    if (value == _balls) return;
                    RaisePropertyChanged();
                }
            }

            public int TableWidth
            {
                get => _tableWidth;
                set
                {
                    if (value.Equals(_tableWidth)) return;
                    RaisePropertyChanged();
                }
            }

            public int TableHeight
            {
                get => _tableHeight;
                set
                {
                    if (value.Equals(_tableHeight)) return;
                    RaisePropertyChanged();
                }
            }

            public ICommand GenerateCommand { get; }
            public ICommand StartMoving { get; }
            public ICommand StopMoving { get; }
            public ICommand ClearBoard { get; }
            

        }
    
}
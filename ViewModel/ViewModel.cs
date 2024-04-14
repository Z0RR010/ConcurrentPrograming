using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Base;
using Logic;
using Model;

namespace ViewModel
{
    
        public class ViewModelWindow : ViewModelBase
        {
            public ViewModelWindow() : this(ModelAbstractApi.CreateApi(),LogicAbstractApi.CreateApi()) { }

            private ViewModelWindow(ModelAbstractApi modelLayer, LogicAbstractApi logicLayer)
            {
                // Fields initialization
                
                _balls = new ObservableCollection<IBallType>();
                _ballRadius = modelLayer.BallRadius;
                _tableWidth = modelLayer.TableWidth;
                _borderWidth = modelLayer.BorderWidth;
                _tableHeight = modelLayer.TableHeight;
                var timer = new System.Timers.Timer();

                // Commands initialization
                
                GenerateCommand = new RelayCommand(() => logicLayer.GenerateHandler(Balls, BallsNumber, _ballRadius, _tableWidth - _ballRadius, _ballRadius, _tableHeight - _ballRadius));
                StartMoving = new RelayCommand(() => logicLayer.MovingHandler(Balls, timer, _ballRadius, _tableWidth, _tableHeight));
                StopMoving = new RelayCommand(() => logicLayer.Stop(timer));
                ClearBoard = new RelayCommand(() => logicLayer.ClearBalls(timer, Balls));
            }

            private int _ballsNumber;
            private readonly ObservableCollection<IBallType> _balls;
            private int _ballRadius;
            private readonly int _tableWidth;
            private readonly int _tableHeight;
            private readonly int _borderWidth;

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

            public ObservableCollection<IBallType> Balls
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

            public int BorderWidth
            {
                get => _borderWidth;
                set
                {
                    if (value.Equals(_borderWidth)) return;
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
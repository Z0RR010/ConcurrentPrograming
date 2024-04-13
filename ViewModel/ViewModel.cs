using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Base;

namespace ViewModel
{
    public class ViewModel
    {
        public class MainWindowViewModel : ViewModelBase
        {
            public MainWindowViewModel() : this(Model.ModelAbstractApi.CreateApi()) { }

            private MainWindowViewModel(Model.ModelAbstractApi modelLayer)
            {
                // Fields initialization
                Logic.LogicAbstractApi logicLayer = new Logic.LogicApi();
                _balls = new ObservableCollection<Logic.Ball>();
                _radius = modelLayer.BallRadius;
                _tableWidth = modelLayer.TableWidth;
                _tableHeight = modelLayer.TableHeight;
                var timer = new Timer();

                // Commands initialization
                GenerateCommand = new RelayCommand(() => logicLayer.GenerateHandler(Balls, BallsNumber, _radius, _tableWidth - _radius, _radius, _tableHeight - _radius));
                StartMoving = new RelayCommand(() => logicLayer.MovingHandler(Balls, timer, BallsNumber, _radius, modelLayer.TableWidth, modelLayer.TableHeight));
                StopMoving = new RelayCommand(() => logicLayer.Stop(timer));
                ClearBoard = new RelayCommand(() => logicLayer.ClearBalls(timer, Balls));
            }

            private int _ballsNumber;
            private readonly ObservableCollection<Logic.Ball> _balls;
            private int _radius;
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

            public int Radius
            {
                get => _radius;
                set
                {
                    if (value == _radius) return;
                    _radius = value;
                    RaisePropertyChanged();
                }
            }

            public ObservableCollection<Logic.Ball> Balls
            {
                get => _balls;
                set
                {
                    if (value == _balls) return;
                    RaisePropertyChanged();
                }
            }

            public int CanvasWidth
            {
                get => _tableWidth;
                set
                {
                    if (value.Equals(_tableWidth)) return;
                    RaisePropertyChanged();
                }
            }

            public int CanvasHeight
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
}
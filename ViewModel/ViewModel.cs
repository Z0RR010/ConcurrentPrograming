using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Base;
using Model;
using System.Collections;
using System.Diagnostics;

namespace ViewModel
{
    
        public class ViewModelWindow : ViewModelBase
        {

        public ViewModelWindow() : this(ModelAbstractApi.CreateApi()) { }


        private ViewModelWindow(ModelAbstractApi modelLayer = null)
            {
            if (modelLayer is null)
            {
                modelLayer = ModelAbstractApi.CreateApi();
            }
            // Fields initialization
            this.modelLayer = modelLayer;
                
            _ballRadius = modelLayer.BallRadius;
            _tableWidth = modelLayer.TableWidth;
            _borderWidth = modelLayer.BorderWidth;
            _tableHeight = modelLayer.TableHeight;
            var timer = new System.Timers.Timer();
            var context = SynchronizationContext.Current;
            timer.Elapsed += (_, _) => context.Send(_ => this.UpdateBalls(), null);
            // Commands initialization
            modelLayer.Initialize(timer);
                GenerateCommand = new RelayCommand(() => modelLayer.GenerateBalls(BallsNumber, _ballRadius, _tableWidth - _ballRadius, _ballRadius, _tableHeight - _ballRadius, new RelayCommand(() => this.UpdateBalls())));
                StopMoving = new RelayCommand(() => modelLayer.Stop());
                ClearBoard = new RelayCommand(() => modelLayer.ClearBalls());
            }

        private void UpdateBalls()
        {
            RaisePropertyChanged(nameof(Balls));
        }

        private int _ballsNumber;
        public ObservableCollection<IVisualBall> Balls => modelLayer.GetVisualBalls();
        private int _ballRadius;
        private readonly int _tableWidth;
        private readonly int _tableHeight;
        private readonly int _borderWidth;
        private ModelAbstractApi modelLayer;

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
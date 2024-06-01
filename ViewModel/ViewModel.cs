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
                
            _tableWidth = modelLayer.TableWidth;
            _borderWidth = modelLayer.BorderWidth;
            _tableHeight = modelLayer.TableHeight;
            GenerateCommand = new RelayCommand(() => modelLayer.GenerateBalls(BallsNumber));
            StopMoving = new RelayCommand(() => modelLayer.Stop());
            }

        //public void UpdateBalls(object? o, EventArgs e)
        //{
        //    RaisePropertyChanged(nameof(Balls));
        //}
        private int _ballsNumber;
        public ObservableCollection<IVisualBall> Balls => modelLayer.GetVisualBalls();
        private readonly float _tableWidth;
        private readonly float _tableHeight;
        private readonly float _borderWidth;
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

            public float TableWidth
            {
                get => _tableWidth;
                set
                {
                    if (value.Equals(_tableWidth)) return;
                    RaisePropertyChanged();
                }
            }

            public float BorderWidth
            {
                get => _borderWidth;
                set
                {
                    if (value.Equals(_borderWidth)) return;
                    RaisePropertyChanged();
                }
            }

        public float TableHeight
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
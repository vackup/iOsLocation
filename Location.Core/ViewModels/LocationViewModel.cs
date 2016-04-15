using MvvmCross.Core.ViewModels;

namespace Location.Core.ViewModels
{
    public class LocationViewModel : MvxViewModel
    {
        readonly ILocationManager _locationManager;

        public LocationViewModel(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        public override void Start()
        {
            _subTotal = 100;
            _generosity = 10;
            Recalcuate();
            base.Start();
        }

        double _subTotal;

        public double SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                RaisePropertyChanged(() => SubTotal);
                Recalcuate();
            }
        }

        int _generosity;

        public int Generosity
        {
            get { return _generosity; }
            set
            {
                _generosity = value;
                RaisePropertyChanged(() => Generosity);
                Recalcuate();
            }
        }

        double _tip;

        public double Tip
        {
            get { return _tip; }
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        void Recalcuate()
        {
            //Tip = _locationManager.TipAmount(SubTotal, Generosity);
        }
    }
}
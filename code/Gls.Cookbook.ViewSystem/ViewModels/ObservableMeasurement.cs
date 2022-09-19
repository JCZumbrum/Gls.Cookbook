using CommunityToolkit.Mvvm.ComponentModel;

namespace Gls.Cookbook.ViewSystem.ViewModels
{
    public class ObservableMeasurement : ObservableObject
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        private string abbreviation;
        public string Abbreviation
        {
            get
            {
                return abbreviation;
            }
            set
            {
                SetProperty(ref abbreviation, value);
            }
        }
    }
}

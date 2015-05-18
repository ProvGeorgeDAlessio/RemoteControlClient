using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControl
{
    public class CameraControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        private string _title = "Title";
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
            }
        }

        
    }
}

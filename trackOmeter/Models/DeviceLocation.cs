using System;
using System.ComponentModel;
using Xamarin.Forms.Maps;

namespace trackOmeter.Models
{
    public class DeviceLocation : INotifyPropertyChanged
    {
        Position _position;
        public string Name { get; set; }
        public Position Position
        {
            get => _position;
            set
            {
                if (!_position.Equals(value))
                {
                    _position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }
        public Boolean IsFocused { get; set; }
        public DeviceLocation(string name, Position position, Boolean isFocused)
        {
            Name = name;
            Position = position;
            IsFocused = isFocused;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

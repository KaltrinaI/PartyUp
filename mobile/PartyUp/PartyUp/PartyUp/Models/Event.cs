using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using PartyUp.ViewModels;
using Xamarin.Forms.Maps;

namespace PartyUp.Models
{

    public class EventFilter : BaseViewModel
    {
        public string UserId { get; set; }
        
        private string _text;
        private ObservableCollection<Tag> _tags;
        private string _locationAddress;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ObservableCollection<Tag> Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public string LocationAddress
        {
            get => _locationAddress;
            set => SetProperty(ref _locationAddress, value);
        }

        public EventFilter()
        {
            Tags = new ObservableCollection<Tag>(Enumerable.Empty<Tag>());
        }
    }
    
    public class Location
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Tag : BaseViewModel
    {
        private int _id;
        private string _name;
        private string _color;
        private bool _isChecked;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        [JsonIgnore]
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
    }
    
    public class Event
    {
        public int EventId { get; set; }
        public BusinessEntity Organizer { get; set; }
        public string EventName { get; set; }
        public DateTime DateTimeOfEvent { get; set; }
        public string PosterUrl{ get; set; }
        public int EventTax{ get; set; }
        public int NumberOfReservations{ get; set; }
        public Location Location{ get; set; }
        public List<Tag> Tags { get; set; }

        public Position Position => new Position(Location.Latitude, Location.Longitude);
        
        public MapSpan MapLocation => MapSpan.FromCenterAndRadius(Position,new Distance(200));
    }
}
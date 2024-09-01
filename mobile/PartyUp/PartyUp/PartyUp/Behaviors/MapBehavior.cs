using System;
using PartyUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PartyUp.Behaviors
{
    public class MapBehavior : Behavior<Map>
    {
        protected override void OnAttachedTo(Map bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            var map = sender as Map;
            if (map?.BindingContext is EventDetailsViewModel viewModel)
            {
                var position = new Position(viewModel.PartyEvent.Location.Latitude, viewModel.PartyEvent.Location.Longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
                map.Pins.Clear();
                map.Pins.Add(new Pin
                {
                    Label = "Address",
                    Position = position,
                    Type = PinType.Place
                });
            }
        }

        protected override void OnDetachingFrom(Map bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }
    }
}
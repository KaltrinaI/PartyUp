using System.Collections.ObjectModel;
using PartyUp.Models;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

public class TagFilterPicker : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<Tag>), typeof(TagFilterPicker), new ObservableCollection<Tag>());

    public static readonly BindableProperty SelectedItemsProperty =
        BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<Tag>), typeof(TagFilterPicker), new ObservableCollection<Tag>());

    public ObservableCollection<Tag> ItemsSource
    {
        get => (ObservableCollection<Tag>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ObservableCollection<Tag> SelectedItems
    {
        get => (ObservableCollection<Tag>)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public TagFilterPicker()
    {
        var selectButton = new Button { Text = "Select Tags" };
        selectButton.Clicked += async (s, e) =>
        {
            var popup = new TagFilterPopup(ItemsSource, SelectedItems);
            await PopupNavigation.Instance.PushAsync(popup);
        };

        var selectedTagsStack = new StackLayout { Orientation = StackOrientation.Horizontal, Spacing = 10 };

        var selectedTags = new ObservableCollection<Tag>();
        SelectedItems.CollectionChanged += (s, e) =>
        {
            selectedTags.Clear();
            foreach (var tag in SelectedItems)
                selectedTags.Add(tag);
        };

        selectedTagsStack.SetBinding(StackLayout.BindingContextProperty, new Binding(nameof(SelectedItems), source: this));
        selectedTagsStack.BindingContextChanged += (s, e) =>
        {
            selectedTagsStack.Children.Clear();
            foreach (var tag in selectedTags)
            {
                var tagStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Color.FromHex(tag.Color),
                    Padding = new Thickness(5, 2),
                    Children =
                    {
                        new Label { Text = tag.Name, VerticalOptions = LayoutOptions.Center },
                        new Button
                        {
                            Text = "X",
                            BackgroundColor = Color.Transparent,
                            Command = new Command(() => SelectedItems.Remove(tag))
                        }
                    }
                };
                selectedTagsStack.Children.Add(tagStack);
            }
        };

        var stack = new StackLayout
        {
            Children = { selectButton, selectedTagsStack }
        };

        Content = stack;
    }
}

public class TagFilterPopup : Rg.Plugins.Popup.Pages.PopupPage
{
    public ObservableCollection<Tag> ItemsSource { get; set; }
    public ObservableCollection<Tag> SelectedItems { get; set; }

    public TagFilterPopup(ObservableCollection<Tag> itemsSource, ObservableCollection<Tag> selectedItems)
    {
        ItemsSource = itemsSource;
        SelectedItems = selectedItems;

        var listView = new ListView
        {
            ItemsSource = ItemsSource,
            ItemTemplate = new DataTemplate(() =>
            {
                var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                var checkBox = new CheckBox();
                checkBox.SetBinding(CheckBox.IsCheckedProperty, nameof(Tag.IsChecked));
                var label = new Label { VerticalOptions = LayoutOptions.Center };
                label.SetBinding(Label.TextProperty, nameof(Tag.Name));

                stackLayout.Children.Add(checkBox);
                stackLayout.Children.Add(label);

                return new ViewCell { View = stackLayout };
            }),
            SelectionMode = ListViewSelectionMode.None
        };

        var closeButton = new Button { Text = "Close" };
        closeButton.Clicked += async (s, e) =>
        {
            foreach (var tag in ItemsSource)
            {
                if (tag.IsChecked && !SelectedItems.Contains(tag))
                    SelectedItems.Add(tag);
                else if (!tag.IsChecked && SelectedItems.Contains(tag))
                    SelectedItems.Remove(tag);
            }
            await PopupNavigation.Instance.PopAsync();
        };

        var stack = new StackLayout
        {
            BackgroundColor = Color.White,
            Opacity = 0.9,
            Margin = new Thickness(20),
            Children = { listView, closeButton },
            Padding = new Thickness(20),
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        Content = stack;
    }
}

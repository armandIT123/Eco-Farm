using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm;

public class SearchBarBehaviour : Behavior<SearchBar>
{
    public static readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(SearchBarBehaviour),
         propertyChanged: OnIsFocusedPropertyChanged);

    SearchBar searchBar;
    protected override void OnAttachedTo(SearchBar searchBar)
    {
        base.OnAttachedTo(searchBar);
        this.searchBar = searchBar;
        this.searchBar.Focused += SearchBar_Focused;
    }

    public bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set
        {
            SetValue(IsFocusedProperty, value);
            if(!value)
                searchBar.Unfocus();
            OnPropertyChanged(nameof(IsFocused));
        }
    }

    private void SearchBar_Focused(object? sender, FocusEventArgs e)
    {
        IsFocused = true;
    }

    protected override void OnDetachingFrom(SearchBar searchBar)
    {
        base.OnDetachingFrom(searchBar);
        searchBar.Focused -= SearchBar_Focused;
    }

    private static void OnIsFocusedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SearchBarBehaviour)bindable;
        control.IsFocused = (bool)newValue;
    }
}

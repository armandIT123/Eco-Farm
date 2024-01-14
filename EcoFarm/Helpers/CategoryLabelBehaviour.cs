using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm
{
    internal class CategoryLabelBehaviour : Behavior<Label>
    {
        public static readonly BindableProperty SelectedCategoryProperty = BindableProperty.Create(nameof(SelectedCategory), typeof(string), typeof(CategoryLabelBehaviour),
         propertyChanged: OnIsSelectedPropertyChanged);

        Label label;
        protected override void OnAttachedTo(Label label)
        {
            base.OnAttachedTo(label);
            this.label = label;
        }

        protected override void OnDetachingFrom(Label label)
        {
            base.OnDetachingFrom(label);
        }

        public string SelectedCategory
        {
            get => (string)GetValue(SelectedCategoryProperty);
            set
            {
                SetValue(SelectedCategoryProperty, value);
                if(value == label?.Text)
                {
                    label.TextColor = Color.FromArgb("#a3d139");
                    label.TextDecorations = TextDecorations.Underline;
                }
                else if (label != null)
                {
                    label.TextColor = Color.FromArgb("#637875");
                    label.TextDecorations = TextDecorations.None;
                }
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CategoryLabelBehaviour)bindable;
            control.SelectedCategory = (string)newValue;
        }
    }
}

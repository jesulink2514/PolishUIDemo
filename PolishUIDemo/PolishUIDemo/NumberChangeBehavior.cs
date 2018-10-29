using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PolishUIDemo
{
    public class NumberChangeBehavior : Behavior<Label>
    {
        private bool _isChanging;
        private Label _control;
        protected override void OnAttachedTo(Label label)
        {
            _control = label;
            label.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(Label label)
        {
            _control = null;
            label.PropertyChanged -= OnPropertyChanged;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Text" || _isChanging) return;

            const int duration = 4000;
            const int step = 10;

            if (!int.TryParse(_control.Text, out var total)) return;

            var increment = total / (duration / step);

            _isChanging = true;

            var number = 0;

            while (number < total)
            {
                number += increment;
                Device.BeginInvokeOnMainThread(() => _control.Text = total.ToString());

                await Task.Delay(step);
            }

            Device.BeginInvokeOnMainThread(() => _control.Text = total.ToString());
            _isChanging = false;

        }
    }
}

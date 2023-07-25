using System.Windows;
using System.Windows.Controls;

namespace ZamzamCo.AttachedProperties
{
    public class PasswordAttachedProperty
    {
        public static readonly DependencyProperty BindPasswordProperty = DependencyProperty
            .RegisterAttached("BindPassword ",
            typeof(bool),
            typeof(PasswordAttachedProperty),
            new PropertyMetadata(true, OnBindPasswordChanged));

        public static bool GetBindPassword(PasswordBox p) => (bool)p.GetValue(BindPasswordProperty);
        public static void SetBindPassword(PasswordBox p, bool value) => p.SetValue(BindPasswordProperty, value);


        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword ",
            typeof(string),
            typeof(PasswordAttachedProperty),
            new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(PasswordBox p) => (string)p.GetValue(BoundPasswordProperty);
        public static void SetBoundPassword(PasswordBox p, string value) => p.SetValue(BoundPasswordProperty, value);


        private static readonly DependencyProperty UpdatingPasswordProperty =
          DependencyProperty.RegisterAttached(
              "UpdatingPassword",
              typeof(bool),
              typeof(PasswordAttachedProperty),
              new PropertyMetadata(false)
              );
        public static bool GetUpdatingPassword(PasswordBox p) => (bool)p.GetValue(UpdatingPasswordProperty);
        public static void SetUpdatingPassword(PasswordBox p, bool value) => p.SetValue(UpdatingPasswordProperty, value);

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event


            if (d is not PasswordBox box)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox? box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (box == null || !GetBindPassword(box))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;

        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox? box = sender as PasswordBox;
            if (box == null)
                return;
            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }
    }
}

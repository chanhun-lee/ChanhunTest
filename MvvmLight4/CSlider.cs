using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MvvmLight4
{
    class CSlider : System.Windows.Controls.Slider
    {
        #region Private Members 

        private Thumb thumb = null;
        private bool isDownOnSlider = false;

        #endregion

        #region override Member 
        public CSlider()
        {
        }

        //private void CSlider_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        //{
        //    var _thumb = (GetTemplateChild("PART_Track") as Track).Thumb;
        //    var _repeatDec = (GetTemplateChild("PART_Track") as Track).DecreaseRepeatButton;

        //    thumb.Width = e.NewSize.Width / (Maximum + 1);

        //    _repeatDec.Margin = new System.Windows.Thickness(0, 0, thumb.Width * -1, 0);
        //}

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (thumb != null)
            {
                thumb.MouseEnter -= thumb_MouseEnter;
                thumb.LostMouseCapture -= thumb_LostMouseCapture;
            }

            thumb = (GetTemplateChild("PART_Track") as Track).Thumb;

            if (thumb != null)
            {
                thumb.MouseEnter += thumb_MouseEnter;
                thumb.LostMouseCapture += thumb_LostMouseCapture;
            }
        }


        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            isDownOnSlider = true;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                base.OnValueChanged(oldValue, newValue);
            }
        }

        #endregion
        #region Private handler 

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && isDownOnSlider)
            {
                // the left button is pressed on mouse enter 
                // so the thumb must have been moved under the mouse 
                // in response to a click on the track. 
                // Generate a MouseLeftButtonDown event. 
                MouseButtonEventArgs args = new MouseButtonEventArgs(
                    e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }
        private void thumb_LostMouseCapture(object sender, EventArgs e)
        {
            isDownOnSlider = false;
            base.OnValueChanged(0, this.Value);
        }
        #endregion



        

    }

}

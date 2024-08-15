using Plugin.Maui.Calendar.Interfaces;
using System.Collections;

namespace FitnessApp.Maui.ViewModels.Calendar
{
    public class WorkoutCollection<T> : List<T>, IPersonalizableDayEvent
    {
        public WorkoutCollection(IEnumerable<T> collection) : base(collection)
        { }
        public WorkoutCollection() : base()
        { }

        #region PersonalizableProperties
        public Color? EventIndicatorColor { get; set; }
        public Color? EventIndicatorSelectedColor { get; set; }
        public Color? EventIndicatorTextColor { get; set; }
        public Color? EventIndicatorSelectedTextColor { get; set; }
        #endregion
    }
}
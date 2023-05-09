using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using trackOmeter.CustomRenderers;
using Xamarin.Forms;

namespace trackOmeter.Helpers
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CardViewExtensions
    {

        public static CustomPanContainer AsCustomPanContainer(this BindableObject bindable)
            => bindable as CustomPanContainer;

        public static CarouselView AsCarouselView(this BindableObject bindable)
            => bindable as CarouselView;

        public static View CreateView(this DataTemplate template)
        {
            var content = template.CreateContent();
            return content is ViewCell cell
                ? cell.View
                : content as View;
        }

        public static DataTemplate SelectTemplate(this DataTemplate template, object context)
        {
            while (template is DataTemplateSelector selector)
            {
                template = selector.SelectTemplate(context, null);
            }
            return template;
        }

        public static int ToCyclicalIndex(this int index, int itemsCount)
        {
            if (itemsCount <= 0)
            {
                return -1;
            }

            var reminder = index % itemsCount;
            return reminder >= 0
                ? reminder
                : reminder + itemsCount;
        }

        public static int FindIndex(this IEnumerable collection, object value)
        {
            if (collection is IList list)
            {
                return list.IndexOf(value);
            }
            var searchIndex = 0;
            foreach (var item in collection)
            {
                if (item == value)
                {
                    return searchIndex;
                }
                ++searchIndex;
            }
            return -1;
        }

        public static object FindValue(this IEnumerable collection, int index)
        {
            if (collection is IList list)
            {
                return index >= 0 && index < list.Count
                    ? list[index]
                    : null;
            }
            var searchIndex = 0;
            foreach (var item in collection)
            {
                if (searchIndex == index)
                {
                    return item;
                }
                ++searchIndex;
            }
            return null;
        }

        public static int Count(this IEnumerable collection)
        {
            if (collection is ICollection list)
            {
                return list.Count;
            }
            var searchIndex = 0;
            foreach (var item in collection)
            {
                ++searchIndex;
            }
            return searchIndex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EasyRemote
{
    /// <summary>
    /// Provides a data template selector which honors data templates targeting interfaces implemented by the
    /// data context.
    /// <see href="http://badecho.com/2012/07/adding-interface-support-to-datatemplates/">Source</see>
    /// <see href="http://stackoverflow.com/questions/6045742/implement-wpf-treeview-with-different-parent-nodes-a-well-as-different-child-nod">HERE</see>
    /// </summary>
    public sealed class InterfaceTemplateSelector : DataTemplateSelector
    {
        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var containerElement = container as FrameworkElement;

            if (null == item || null == containerElement)
                return base.SelectTemplate(item, container);

            var itemType = item.GetType();

            var dataTypes
                = Enumerable.Repeat(itemType, 1).Concat(itemType.GetInterfaces());

            var template
                = dataTypes.Select(t => new DataTemplateKey(t))
                    .Select(containerElement.TryFindResource)
                    .OfType<DataTemplate>()
                    .FirstOrDefault();

            return template ?? base.SelectTemplate(item, container);
        }
    }
}

using System.Windows;
using System.Windows.Media;

namespace EasyRemote.Tools
{
    static class ParentTools
    {
        /// <summary>
        /// Get the first parent of item that is T
        /// </summary>
        /// <typeparam name="T">Class filet</typeparam>
        /// <param name="ob">item</param>
        /// <returns>Parent of item</returns>
        public static T GetParent<T>(this DependencyObject ob)
           where T : DependencyObject
        {
            do
            {
                ob = VisualTreeHelper.GetParent(ob);
                if (ob is T)
                {
                    return (T)ob;
                }
            } while (ob != null);
            return default(T);
        }
    }
}

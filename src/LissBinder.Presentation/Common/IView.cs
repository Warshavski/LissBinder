
namespace Escyug.LissBinder.Presentation.Common
{
    /// <summary>
    /// Base interface for all views
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Show up view
        /// </summary>
        void Show();

        /// <summary>
        /// Close view
        /// </summary>
        void Close();

        /// <summary>
        /// Set error message
        /// </summary>
        string Error { set; }

        /// <summary>
        /// Set notify message
        /// </summary>
        string Notify { set; }

        /// <summary>
        /// Set warning message
        /// </summary>
        string Warning { set; }
    }
}

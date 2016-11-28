
namespace Escyug.LissBinder.Presentation.Common
{
    /// <summary>
    /// Base presenter interface for all presenters
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Run presenter
        /// </summary>
        void Run();
    }

    /// <summary>
    /// Base presenter interface for all presenters with one parameter
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    public interface IPresenter<in TArg>
    {
        /// <summary>
        /// Run presenter with parameter
        /// </summary>
        /// <param name="argument"></param>
        void Run(TArg argument);
    }

}

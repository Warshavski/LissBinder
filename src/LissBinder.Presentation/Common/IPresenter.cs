
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


   /// <summary>
    /// Base presenter interface for all presenters with two parameters
   /// </summary>
   /// <typeparam name="TArg1"></typeparam>
   /// <typeparam name="TArg2"></typeparam>
    public interface IPresenter<in TArg1, in TArg2>
    {
        /// <summary>
        /// Run presenter with parameter
        /// </summary>
        /// <param name="argument"></param>
        void Run(TArg1 argument1, TArg2 argument2);
    }
}

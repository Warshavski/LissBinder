﻿
namespace Escyug.LissBinder.Presentation.Common
{
    public abstract class BasePresenter<TView> : IPresenter
       where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController AppController { get; private set; }

        protected BasePresenter(TView view, IApplicationController appController)
        {
            View = view;
            AppController = appController;
        }

        public void Run()
        {
            View.Show();
        }
    }

    public abstract class BasePresenter<TView, TArgument> : IPresenter<TArgument>
        where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController AppController { get; private set; }

        protected BasePresenter(TView view, IApplicationController appController)
        {
            View = view;
            AppController = appController;
        }

        public abstract void Run(TArgument argument);
    }
}

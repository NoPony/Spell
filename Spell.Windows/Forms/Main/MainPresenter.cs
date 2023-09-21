using Spell.Core;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Spell.Forms.Main
{
    internal class MainPresenter
    {
        private readonly IMainView _view;
        private readonly MainModel _model;

        private readonly SpellService _spellCheck;

        private readonly List<IDisposable> _subscriptions;

        internal IObservable<EventArgs> Exit() => _exit;
        private readonly ISubject<EventArgs> _exit;

        internal MainPresenter()
        {
            _model = new MainModel();
            _view = new MainView();

            _exit = new Subject<EventArgs>();

            _spellCheck = new SpellService();
            _subscriptions = new List<IDisposable>
            {
                _model.Query().Subscribe(i => Model_QueryChange(i)),
                _view.QueryChange().Subscribe(i => View_QueryChange(i)),
                _view.Exit().Subscribe(i => View_Exit(i))
            };

            _view.SetResult(_model.Result());
        }

        internal void Open() => _view
            .Open();

        private void Model_QueryChange(string query) => _model
            .Result()
            .OnNext(_spellCheck.Check(query));

        private void View_QueryChange(string query) => _model
            .Query()
            .OnNext(query);

        private void View_Exit(FormClosedEventArgs e)
        {
            _subscriptions.ForEach(s => s.Dispose());
            _subscriptions.Clear();

            _exit.OnNext(e);
        }
    }
}

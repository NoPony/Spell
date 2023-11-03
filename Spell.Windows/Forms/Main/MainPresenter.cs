using Spell.Core;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Spell.Forms.Main
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly MainModel _model;

        private readonly SpellService _spell;

        private readonly List<IDisposable> _subscriptions;

        internal IObservable<EventArgs> Closed => _closed.AsObservable();
        private readonly ISubject<EventArgs> _closed;

        public MainPresenter(MainModel model, IMainView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _spell = new SpellService();

            _closed = new Subject<EventArgs>();

            _subscriptions = new List<IDisposable>
            {
                _model.Query.Subscribe(Model_QueryChange),
                _model.Result.Subscribe(Model_ResultChange),
                _model.Status.Subscribe(Model_StatusChange),

                _view.QueryChanged.Subscribe(View_QueryChanged),
                _view.ViewClosed.Subscribe(View_Closed)
            };

            //_model.Query.OnNext("");
            //_model.Result.OnNext(Enumerable.Empty<Suggestion>());
            //_model.Status.OnNext("Ready.");
        }

        internal void Show()
        {
            _view.Show();

            _model.Query.OnNext("");
            _model.Result.OnNext(Enumerable.Empty<Suggestion>());
            _model.Status.OnNext("Ready.");
        }

        private void Model_QueryChange(string query)
        {
            Stopwatch sw = Stopwatch.StartNew();

            MatchResult result = _spell.CheckWord(query);
            
            sw.Stop();

            _model.Result.OnNext(result.Suggestions);
            _model.Status.OnNext($"Completed in {sw.Elapsed.TotalMilliseconds}ms.");
        }

        private void Model_ResultChange(IEnumerable<Suggestion> suggestions)
        {
            _view.SetSuggestions(suggestions);
        }

        private void Model_StatusChange(string status)
        {
            _view.SetStatus(status);
        }

        private void View_QueryChanged(string query)
        {
            _model.Query.OnNext(query);
        }

        private void View_Closed(FormClosedEventArgs e)
        {
            _subscriptions.ForEach(s => s.Dispose());
            _subscriptions.Clear();

            _closed.OnNext(e);
        }
    }
}

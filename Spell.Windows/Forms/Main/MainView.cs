using Spell.Core;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Forms;

namespace Spell.Forms.Main
{
    public partial class MainView : Form, IMainView
    {
        public IObservable<string> QueryChange() => _queryChangeSubject.AsObservable();
        private readonly IObservable<EventPattern<EventArgs>> _onQueryChange;
        private readonly ISubject<string> _queryChangeSubject;

        public IObservable<FormClosedEventArgs> Exit() => _exitSubject.AsObservable();
        private readonly IObservable<EventPattern<FormClosedEventArgs>> _onExit;
        private readonly ISubject<FormClosedEventArgs> _exitSubject;

        private readonly List<IDisposable> _subscriptions;

        internal MainView()
        {
            InitializeComponent();

            _subscriptions = new List<IDisposable>();

            _queryChangeSubject = new ReplaySubject<string>(1);
            _onQueryChange = Observable.FromEventPattern<EventArgs>(textQuery, "TextChanged");
            _subscriptions.Add(_onQueryChange.Subscribe(e => _queryChangeSubject.OnNext(textQuery.Text)));

            _exitSubject = new Subject<FormClosedEventArgs>();
            _onExit = Observable.FromEventPattern<FormClosedEventArgs>(this, "FormClosed");
            _subscriptions.Add(_onExit.Subscribe(e => _exitSubject.OnNext(e.EventArgs)));

            listResult.Columns.AddRange(new ColumnHeader[]
            {
                ValueColumnHeader,
                ScoreColumnHeader,
                TokenColumnHeader,
                DistanceColumnHeader,
                FirstColumnHeader
            });
        }

        public void Open() =>
            Show();

        void IMainView.Close() =>
            Close();

        public void SetQuery(IObservable<string> observable) =>
            _subscriptions.Add(observable.Subscribe(i => textQuery.Text = i));

        public void SetResult(IObservable<IEnumerable<Result>> observable) => _subscriptions.Add(observable.Subscribe(
            next =>
            {
                listResult.Items.Clear();
                listResult.Items.AddRange(next
                    .Select((i, r) => new ListViewItem(new string[]
                    {
                        i.Value,
                        $"{i.Score}",
                        $"{i.TokenScore}",
                        $"{i.Distance}",
                        $"{i.FirstLetter}",
                    }))
                    .ToArray());
            }));

        private void Exit(object? sender, FormClosedEventArgs e)
        {
            _subscriptions.ForEach(s => s.Dispose());
            _subscriptions.Clear();

            _exitSubject.OnNext(e);
        }

        // Result list column definitions
        // !! KEEP SORTED !!
        private static ColumnHeader ValueColumnHeader => new()
        {
            DisplayIndex = 0,
            Text = "Value",
            Width = 200
        };

        private static ColumnHeader ScoreColumnHeader => new()
        {
            DisplayIndex = 1,
            Text = "Score"
        };

        private static ColumnHeader TokenColumnHeader => new()
        {
            DisplayIndex = 2,
            Text = "Token"
        };

        private static ColumnHeader DistanceColumnHeader => new()
        {
            DisplayIndex = 3,
            Text = "Distance"
        };

        private static ColumnHeader FirstColumnHeader => new()
        {
            DisplayIndex = 4,
            Text = "First"
        };
    }
}
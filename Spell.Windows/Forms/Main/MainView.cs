using Spell.Core;
using System.Reactive;
using System.Reactive.Linq;

namespace Spell.Forms.Main
{
    public partial class MainView : Form, IMainView
    {
        private readonly ColumnHeader[] _columns = new ColumnHeader[]
        {
            new() { DisplayIndex = 0, Text = "Value", Width = 200 },
            new() { DisplayIndex = 1, Text = "Score" },
            new() { DisplayIndex = 2, Text = "Token" },
            new() { DisplayIndex = 3, Text = "Distance" },
            new() { DisplayIndex = 4, Text = "First" }
        };

        private readonly IObservable<EventPattern<EventArgs>> _queryChanged;
        private readonly IObservable<EventPattern<FormClosedEventArgs>> _formClosed;

        private readonly List<IDisposable> _subscriptions;

        public IObservable<string> QueryChanged => _queryChanged.Select(i => textQuery.Text);
        public IObservable<FormClosedEventArgs> ViewClosed => _formClosed.Select(i => i.EventArgs);

        public MainView()
        {
            InitializeComponent();

            listResult.Columns.AddRange(_columns);

            _queryChanged = Observable.FromEventPattern<EventArgs>(textQuery, "TextChanged");
            _formClosed = Observable.FromEventPattern<FormClosedEventArgs>(this, "FormClosed");

            _subscriptions = new List<IDisposable>
            {
                _formClosed.Subscribe(e => Form_Closed(e.Sender, e.EventArgs))
            };
        }

        void IMainView.Show()
        {
            Show();
        }

        void IMainView.Close()
        {
            Close();
        }

        public void SetQuery(string value)
        {
            throw new NotImplementedException();
        }

        public void SetMatchText(string value)
        {
            MatchText.Text = value;
        }

        public void SetSuggestions(IEnumerable<Suggestion> value)
        {
            listResult.Items.Clear();
            listResult.Items.AddRange(
                value.Select(
                    i => new ListViewItem(
                        new string[]
                        {
                            i.Value,
                            $"{i.Confidence}",
                            $"{i.BigramSearchScore}",
                            $"{i.LevenshteinDistance}",
                            $"{i.FirstLetterMatch}",
                        }))
                .ToArray());
        }

        public void SetStatus(string status)
        {
            StatusLabel.Text = status;
        }

        private void Form_Closed(object? sender, FormClosedEventArgs e)
        {
            _subscriptions.ForEach(s => s.Dispose());
            _subscriptions.Clear();
        }
    }
}

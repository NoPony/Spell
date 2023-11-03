using Spell.Core;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using static System.Net.Mime.MediaTypeNames;

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

        private readonly IObservable<EventPattern<EventArgs>> _onQueryChange;
        private readonly IObservable<EventPattern<FormClosedEventArgs>> _onClosed;

        private readonly List<IDisposable> _subscriptions;

        public IObservable<string> QueryChanged => _onQueryChange.Select(i => textQuery.Text);
        public IObservable<FormClosedEventArgs> ViewClosed => _onClosed.Select(i => i.EventArgs);

        public MainView()
        {
            InitializeComponent();

            listResult.Columns.AddRange(_columns);

            _onQueryChange = Observable.FromEventPattern<EventArgs>(textQuery, "TextChanged");
            _onClosed = Observable.FromEventPattern<FormClosedEventArgs>(this, "FormClosed");

            _subscriptions = new List<IDisposable>
            {
                _onClosed.Subscribe(e => OnFormClosed(e.Sender, e.EventArgs))
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

        private void OnFormClosed(object? sender, FormClosedEventArgs e)
        {
            _subscriptions.ForEach(s => s.Dispose());
            _subscriptions.Clear();
        }

    }
}
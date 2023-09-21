using Spell.Core;
using System.Reactive.Subjects;

namespace Spell.Forms.Main
{
    internal class MainModel
    {
        internal ISubject<string> Query() => _query;
        private readonly ISubject<string> _query;

        internal ISubject<IEnumerable<Result>> Result() => _result;
        private readonly ISubject<IEnumerable<Result>> _result;

        internal MainModel()
        {
            _query = new ReplaySubject<string>(1);
            _result = new ReplaySubject<IEnumerable<Result>>(1);
        }
    }
}

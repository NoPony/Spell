using Spell.Core;
using System.Reactive.Subjects;

namespace Spell.Forms.Main
{
    public class MainModel
    {
        public ISubject<string> Query { get; } = new Subject<string>();
        public ISubject<IEnumerable<Suggestion>> Result { get; } = new Subject<IEnumerable<Suggestion>>();
        public ISubject<string> Status { get; } = new Subject<string>();
    }
}

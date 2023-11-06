using Spell.Core;

namespace Spell.Forms.Main
{
    public interface IMainView
    {
        /// <summary>
        /// Observable that fires on change of the query control
        /// </summary>
        /// <returns></returns>
        IObservable<string> QueryChanged { get; }

        /// <summary>
        /// Observable that fires on View Exit
        /// </summary>
        /// <returns></returns>
        IObservable<FormClosedEventArgs> ViewClosed { get; }

        /// <summary>
        /// Open/Show the View
        /// </summary>
        void Show();

        /// <summary>
        /// Close the View
        /// </summary>
        void Close();

        /// <summary>
        /// Set the Subject of the Query control
        /// </summary>
        /// <param name="subject"></param>
        void SetQuery(string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void SetMatchText(string value);

        /// <summary>
        /// Set the value of the Result control
        /// </summary>
        /// <param name="observable"></param>
        void SetSuggestions(IEnumerable<Suggestion> value);

        /// <summary>
        /// Set the value of the status label
        /// </summary>
        /// <param name="status"></param>
        void SetStatus(string status);
    }
}

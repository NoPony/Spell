using Spell.Core;

namespace Spell.Forms.Main
{
    internal interface IMainView
    {
        /// <summary>
        /// Observable that fires on change of the query control
        /// </summary>
        /// <returns></returns>
        IObservable<string> QueryChange();

        /// <summary>
        /// Observable that fires on View Exit
        /// </summary>
        /// <returns></returns>
        IObservable<FormClosedEventArgs> Exit();

        /// <summary>
        /// Open/Show the View
        /// </summary>
        void Open();

        /// <summary>
        /// Close the View
        /// </summary>
        void Close();

        /// <summary>
        /// Set the Subject of the Query control
        /// </summary>
        /// <param name="subject"></param>
        void SetQuery(IObservable<string> subject);

        /// <summary>
        /// Set the value of the Result control
        /// </summary>
        /// <param name="observable"></param>
        void SetResult(IObservable<IEnumerable<Result>> observable);
    }
}

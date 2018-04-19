namespace CommonErrors.Shared
{
    /// <summary>
    /// An Answer that only has two outcomes: true or false
    /// </summary>
    public class TrueFalseAnswer : IGradable
    {
        /// <summary>
        /// Whether or not the Answer was correct
        /// </summary>
        public bool Correct { get; set; }

        public decimal Grade => Correct ? 100 : 0;

        /// <summary>
        /// Answer that is True or False
        /// </summary>
        /// <param name="correct">The result of the Answer. True or False</param>
        public TrueFalseAnswer(bool correct)
        {
            Correct = correct;
        }
    }
}

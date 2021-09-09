namespace DualTextReader.SentenceExtractors
{
    public interface ISentenceExtractor
    {
        public string Sentence { get; }

        public void GoToNext();

        public void GoToPrevious();
    }
}

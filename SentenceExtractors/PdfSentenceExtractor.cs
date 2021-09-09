using System.Globalization;
using System.Threading;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace DualTextReader.SentenceExtractors
{
    public class PdfSentenceExtractor : ISentenceExtractor
    {
        public string Sentence => _sentences?[_sentenceIndex];

        private readonly PdfDocument _document;

        private int _pageIndex;

        private int _sentenceIndex;

        private string[] _sentences;

        public PdfSentenceExtractor(string pathToFile)
        {
            _document = PdfReader.Open(pathToFile);
            UpdatePage(0);
        }

        public void GoToNext()
        {
            if (_sentenceIndex == _sentences.Length - 1)
            {
                if (_pageIndex == _document.PageCount - 1)
                {
                    return;
                }

                if (UpdatePage(_pageIndex + 1))
                {
                    _sentenceIndex = 0;
                }
            }
            else
            {
                _sentenceIndex++;
            }
        }

        public void GoToPrevious()
        {
            if (_sentenceIndex == 0)
            {
                if (_pageIndex == 0)
                {
                    return;
                }

                if (UpdatePage(_pageIndex - 1))
                {
                    _sentenceIndex = _sentences.Length - 1;
                }
            }
            else
            {
                _sentenceIndex--;
            }
        }

        private bool UpdatePage(int page)
        {
            var rawSentences = _document.Pages[_pageIndex].ExtractText().Split('.');
            if (rawSentences.Length == 0)
            {
                return false;
            }

            _sentences = new string[rawSentences.Length];
            for (var i = 0; i < _sentences.Length; i++)
            {
                _sentences[i] = rawSentences[i].Trim();
            }

            _pageIndex = page;

            return true;
        }
    }
}

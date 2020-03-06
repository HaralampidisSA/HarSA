using DocXToPdfConverter;
using System.Collections.Generic;

namespace HarSA.PdfConverter.Services
{
    public interface IPdfGenerator
    {
        void SavePdf(
            string templateFile,
            string outputPath,
            Dictionary<string, string> textPlaceholders = null,
            List<Dictionary<string, string[]>> tablePlaceholders = null,
            Dictionary<string, ImageElement> imagePlaceholders = null);
    }
}
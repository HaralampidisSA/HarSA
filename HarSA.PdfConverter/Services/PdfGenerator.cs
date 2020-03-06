using DocXToPdfConverter;
using HarSA.PdfConverter.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace HarSA.PdfConverter.Services
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly PdfConverterConfig _config;

        public PdfGenerator(IOptions<PdfConverterConfig> options)
        {
            _config = options.Value;
        }

        public void SavePdf(
            string templateFile,
            string outputPath,
            Dictionary<string, string> textPlaceholders = null,
            List<Dictionary<string, string[]>> tablePlaceholders = null,
            Dictionary<string, ImageElement> imagePlaceholders = null)
        {
            var soffice = _config.SofficeLocationPath;
            if (string.IsNullOrEmpty(soffice))
            {
                throw new ArgumentNullException(nameof(soffice));
            }

            var textTag = _config.TextPlaceholderTag;
            if (string.IsNullOrEmpty(textTag))
            {
                throw new ArgumentNullException(nameof(textTag));
            }

            var tableTag = _config.TablePlaceholderTag;
            if (string.IsNullOrEmpty(tableTag))
            {
                throw new ArgumentNullException(nameof(tableTag));
            }

            var imageTag = _config.ImagePlaceholderTag;
            if (string.IsNullOrEmpty(imageTag))
            {
                throw new ArgumentNullException(nameof(imageTag));
            }

            var placeholders = new Placeholders();

            if (textPlaceholders != null)
            {
                placeholders.TextPlaceholderStartTag = textTag;
                placeholders.TextPlaceholderEndTag = textTag;

                placeholders.TextPlaceholders = textPlaceholders;
            }

            if (tablePlaceholders != null)
            {
                placeholders.TablePlaceholderStartTag = tableTag;
                placeholders.TablePlaceholderEndTag = tableTag;

                placeholders.TablePlaceholders = tablePlaceholders;
            }

            if (imagePlaceholders != null)
            {
                placeholders.ImagePlaceholderStartTag = imageTag;
                placeholders.ImagePlaceholderEndTag = imageTag;

                placeholders.ImagePlaceholders = imagePlaceholders;
            }

            var generator = new ReportGenerator(soffice);

            generator.Convert(templateFile, outputPath, placeholders);
        }
    }
}
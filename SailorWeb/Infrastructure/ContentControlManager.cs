using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace SailorWeb.Infrastructure
{
    public class ContentControlManager
    {
        /// <summary>
        /// Contains the word processing document
        /// </summary>
        private WordprocessingDocument _wordProcessingDocument;

        /// <summary>
        /// Contains the main document part
        /// </summary>
        private MainDocumentPart _mainDocPart;

        /// <summary>
        /// Open an Word XML document 
        /// </summary>
        /// <param name="docname">name of the document to be opened</param>
        public void OpenDocuemnt(string docname)
        {
            // open the word docx
            _wordProcessingDocument = WordprocessingDocument.Open(docname, true);

            // get the Main Document part
            _mainDocPart = _wordProcessingDocument.MainDocumentPart;
        }

        /// <summary>
        /// Close the document
        /// </summary>
        public void CloseDocument()
        {
            _wordProcessingDocument.Close();
        }

        // Updated Text
        //

        /// <summary>
        /// Updated text placeholders with texts.
        /// </summary>
        /// <param name="tagValueDict">Pair of placeholder tagID and text to replace.</param>
        public void UpdateText(Dictionary<string, string> tagValueDict)
        {
            foreach (var pair in tagValueDict)
            {
                var tagID = pair.Key;
                var value = pair.Value;

                foreach (var sdtElement in _mainDocPart.Document.Body.Descendants<SdtElement>())
                {
                    if (sdtElement.SdtProperties.GetFirstChild<Tag>() != null && sdtElement.SdtProperties.GetFirstChild<Tag>().Val == tagID)
                    {
                        OpenXmlElement parantElement = sdtElement.Descendants<Paragraph>().SingleOrDefault();
                        if (null == parantElement)
                        {
                            SdtContentRun cr = sdtElement.Descendants<SdtContentRun>().SingleOrDefault();
                            parantElement = cr;
                        }

                        if (null != parantElement)
                        {
                            Run r = parantElement.Descendants<Run>().SingleOrDefault();
                            if (null != r)
                            {
                                Text t = r.Descendants<Text>().SingleOrDefault();
                                if (null != t)
                                {
                                    r.AppendChild(new Text(value));
                                    r.RemoveChild(t);
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the relationship id of image.
        /// </summary>
        /// <typeparam name="TSdtType">SdtElement type</typeparam>
        /// <param name="sdt">A sdtElement object that may contains image placeholder.</param>
        /// <param name="imageTag">Image placeholder tagID.</param>
        /// <returns>The relationship id of image.</returns>
        internal static string GetImageRelID<TSdtType>(TSdtType sdt, string imageTag) where TSdtType : SdtElement
        {
            // loop through all tags in the document within the sdt element
            foreach (Tag t in sdt.Descendants<Tag>().ToList())
            {
                // Do we have the correct tag?
                if (t.Val.ToString().ToUpper() == imageTag.ToUpper())
                {
                    // Get the BLIP for the image - there is only one image per placeholder so no need to loop through anything
                    Blip b = sdt.Descendants<Blip>().FirstOrDefault();
                    if (null != b)
                    {
                        // return the image id tag
                        return b.Embed.Value;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the original size of placeholder image.
        /// </summary>
        /// <param name="drawingList">Drawing object that may contains the image relationship id.</param>
        /// <param name="relID">The image relationship id.</param>
        /// <param name="width">Width of the image.</param>
        /// <param name="height">Height of the image.</param>
        internal static void GetPlaceholderImageSize(IEnumerable<Drawing> drawingList, string relID, out int width, out int height)
        {
            width = -1;
            height = -1;

            // Loop through all Drawing elements in the document
            foreach (Drawing d in drawingList)
            {
                // Loop through all the pictures (Blip) in the document
                if (d.Descendants<Blip>().ToList().Any(b => b.Embed.ToString() == relID))
                {
                    // The document size is in EMU. 1 pixel = 9525 EMU

                    // The size of the image placeholder is located in the EXTENT element
                    Extent e = d.Descendants<Extent>().FirstOrDefault();
                    if (null != e)
                    {
                        width = (int)(e.Cx / 9525);
                        height = (int)(e.Cy / 9525);
                    }

                    if (width == -1)
                    {
                        // The size of the image is located in the EXTENTS element
                        Extents e2 = d.Descendants<Extents>().FirstOrDefault();
                        if (null != e2)
                        {
                            width = (int)(e2.Cx / 9525);
                            height = (int)(e2.Cy / 9525);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replace the image part with image memory stream.
        /// </summary>
        /// <param name="relID">The relationship id of the placeholder image.</param>
        /// <param name="imageStream">Image memory stream to replace the placeholder image.</param>
        /// <param name="width">Width of placeholder image.</param>
        /// <param name="height">Height of placeholder image.</param>
        private void UpdateImagePart(string relID, MemoryStream imageStream, int width, int height)
        {
            var originalBitmap = Image.FromStream(imageStream);
            var bitmap = originalBitmap;

            // resize image
            if (width != -1)
            {
                bitmap = new Bitmap(originalBitmap, width, height);
            }

            // Save image data to ImagePart
            var stream = new MemoryStream();
            bitmap.Save(stream, originalBitmap.RawFormat);

            // Get the ImagePart
            var imagePart = (ImagePart)_mainDocPart.GetPartById(relID);

            // Create a writer to the ImagePart
            var writer = new BinaryWriter(imagePart.GetStream());

            // Overwrite the current image in the docx file package
            writer.Write(stream.ToArray());

            // Close the ImagePart
            writer.Close();
        }

        /// <summary>
        /// Updated image placeholders with images.
        /// </summary>
        /// <param name="tagValueDict">Pair of placeholder tagID and image to replace.</param>
        public void UpdateImage(Dictionary<string, MemoryStream> tagValueDict)
        {
            foreach (var pair in tagValueDict)
            {
                var tagID = pair.Key;
                var imageStream = pair.Value;

                foreach (SdtElement sdtElement in _mainDocPart.Document.Body.Descendants<SdtElement>())
                {
                    string relID = GetImageRelID(sdtElement, tagID);
                    if (!string.IsNullOrEmpty(relID))
                    {
                        // Get size of image
                        int imageWidth;
                        int imageHeight;
                        GetPlaceholderImageSize(_mainDocPart.Document.Body.Descendants<Drawing>(), relID, out imageWidth, out imageHeight);

                        UpdateImagePart(relID, imageStream, imageWidth, imageHeight);

                        break;
                    }
                }
            }
        }
    }
}
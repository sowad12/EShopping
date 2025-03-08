﻿//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Net.Http.Headers;
//using System;
//using System.IO;
//using System.Text;

//namespace Catalog.Library.UItility
//{
//    public class MultipartUtilities
//    {
//        public static bool IsMultipartContentType(string contentType)
//        {
//            return !string.IsNullOrEmpty(contentType) && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
//        }

//        // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
//        // The spec says 70 characters is a reasonable limit.
//        public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
//        {
//            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;
//            if (string.IsNullOrWhiteSpace(boundary))
//            {
//                throw new InvalidDataException("Missing content-type boundary.");
//            }

//            if (boundary.Length > lengthLimit)
//            {
//                throw new InvalidDataException(
//                    $"Multipart boundary length limit {lengthLimit} exceeded.");
//            }

//            return boundary;
//        }

//        public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
//        {
//            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
//            return contentDisposition != null
//                   && contentDisposition.DispositionType.Equals("form-data")
//                   && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
//                       || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
//        }

//        public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
//        {
//            // Content-Disposition: form-data; name="key";
//            return contentDisposition != null
//                   && contentDisposition.DispositionType.Equals("form-data")
//                   && string.IsNullOrEmpty(contentDisposition.FileName.Value)
//                   && string.IsNullOrEmpty(contentDisposition.FileNameStar.Value);
//        }

//        public static Encoding GetEncoding(MultipartSection section)
//        {
//            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue mediaType);
//            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
//            // most cases.
//            if (!hasMediaTypeHeader || Encoding.UTF8.Equals(mediaType.Encoding))
//            {
//                return Encoding.UTF8;
//            }
//            return mediaType.Encoding;
//        }


//    }
//}

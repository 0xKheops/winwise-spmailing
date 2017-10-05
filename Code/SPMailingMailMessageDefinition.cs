using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Winwise.SPMailing {

    /// <summary>
    /// Wraps all informations required to build a MailMessage corresponding to a mailing
    /// </summary>
    class SPMailingMailMessageDefinition {

        private SPMailingMailMessageDefinition(String fromAdress, String fromDisplayName, String replyToAdress, String replyToDisplayName, String subject, String body, String embeddedBody, SPMailingResource[] embeddedImages)
        {
            _fromAdress = fromAdress;
            _fromDisplayName = fromDisplayName;
            _replyToAdress = replyToAdress;
            _replyToDisplayName = replyToDisplayName;
            _subject = subject;
            _body = body;
            _embeddedBody = embeddedBody;
            _embeddedImages = embeddedImages;
        }

        #region Fields

        private String _fromAdress;
        private String _fromDisplayName;
        private String _replyToAdress;
        private String _replyToDisplayName;
        private String _subject;
        private String _body;
        private String _embeddedBody;
        private SPMailingResource[] _embeddedImages;
        private static Regex REG_IMG_TAGS = new Regex(@"(<IMG\s[^>]*?src\s*=\s*[\""']?)([^\""']*?)([\""'][^>]*?>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex REG_HREF = new Regex(@"(href=)([\""'])([^\""']*?)([\""'])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Properties

        public String FromAdress {
            get { return _fromAdress; }
        }

        public String FromDisplayName {
            get { return _fromDisplayName; }
        }

        public String ReplyToAdress {
            get { return _replyToAdress; }
        }

        public String ReplyToDisplayName {
            get { return _replyToDisplayName; }
        }

        /// <summary>
        /// Mail subject
        /// </summary>
        public String Subject
        {
            get { return _subject; }
        }

        /// <summary>
        /// Mail body
        /// </summary>
        public String Body
        {
            get { return _body; }
        }

        /// <summary>
        /// Mail body transformed to use embedded images
        /// </summary>
        public String EmbeddedBody
        {
            get { return _embeddedBody; }
            set { _embeddedBody = value; }
        }

        /// <summary>
        /// Images to embed
        /// </summary>
        public SPMailingResource[] EmbeddedImages
        {
            get { return _embeddedImages; }
        }

        #endregion

        #region Generation

        /// <summary>
        /// Generates a Mailing object
        /// </summary>
        /// <param name="appUrl">web application url</param>
        /// <param name="item">mailing item</param>
        /// <returns></returns>
        public static SPMailingMailMessageDefinition CreateMailing(SPMailingContext ctx, SPListItem item)
        {
            Uri webUri = new Uri(ctx.Web.Url);
            String appUrl = webUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);

            String fromAdress = item[ctx.FieldIds.From_Adress] as String;
            String fromDisplayName = item[ctx.FieldIds.From_DisplayName] as String;
            String replyToAdress = item[ctx.FieldIds.ReplyTo_Adress] as String;
            String replyToDisplayName = item[ctx.FieldIds.ReplyTo_DisplayName] as String;
            String subject = item[ctx.FieldIds.MailSubject] as String;
            String body = item[ctx.FieldIds.MailBody] as String;

            Dictionary<String, SPMailingResource> dicImages = new Dictionary<String, SPMailingResource>();

            //saves current cert manager
            RemoteCertificateValidationCallback certCallback = ServicePointManager.ServerCertificateValidationCallback;

            //Temporary allow invalid certificates
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate(Object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
            {
                return true;
            });

            foreach (Match match in REG_IMG_TAGS.Matches(body))
                if (match.Success && !dicImages.ContainsKey(match.Groups[2].Value))
                {

                    String imageUrl = match.Groups[2].Value;
                    String imageFullUrl = imageUrl;
                    if (imageFullUrl.StartsWith("/"))
                        imageFullUrl = SPUrlUtility.CombineUrl(appUrl, imageUrl);

                    //Saves the content of the image
                    string contentType = null;
                    Byte[] imgContent = DownloadFile(imageFullUrl, out contentType);

                    dicImages.Add(imageUrl, new SPMailingResource(Guid.NewGuid().ToString(), imgContent, contentType));

                }

            ServicePointManager.ServerCertificateValidationCallback = certCallback;

            //Replaces images links by resource ids
            String embeddedBody = REG_IMG_TAGS.Replace(body, new MatchEvaluator(delegate(Match match)
            {
                String imageUrl = match.Groups[2].Value;
                if (dicImages.ContainsKey(imageUrl))
                    return String.Format("{0}cid:{1}{2}", match.Groups[1].Value, dicImages[imageUrl].ContentId, match.Groups[3].Value);
                return String.Format("{0}{1}{2}", match.Groups[1].Value, imageUrl, match.Groups[3].Value);
            }
            ));

            //Replaces app relative urls
            embeddedBody = REG_HREF.Replace(embeddedBody, new MatchEvaluator(delegate(Match match)
            {
                String url = match.Groups[3].Value;
                if (!url.StartsWith("http") && !url.StartsWith("mailto:"))
                    return String.Format("{0}{1}{2}", match.Groups[1].Value + "\"", SPUrlUtility.CombineUrl(appUrl, url), match.Groups[4].Value);
                return match.Value;
            }
            ));

            List<SPMailingResource> images = new List<SPMailingResource>(dicImages.Values);

            return new SPMailingMailMessageDefinition(fromAdress,fromDisplayName, replyToAdress,replyToDisplayName, subject, body, embeddedBody, images.ToArray());

        }

        public static Byte[] DownloadFile(String remoteFilename, out string contentType)
        {

            Stream remoteStream = null;
            WebResponse response = null;
            Byte[] bytesProcessed = new byte[0];
            contentType = String.Empty;

            try
            {
                // Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                request.Credentials = CredentialCache.DefaultCredentials;
                if (request != null)
                {

                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();
                        bytesProcessed = new Byte[response.ContentLength];
                        contentType = response.ContentType;
                        int bytesRead = 0;
                        while (bytesRead < bytesProcessed.Length)
                        {
                            bytesProcessed[bytesRead] = Byte.Parse(remoteStream.ReadByte().ToString());
                            bytesRead++;
                        }
                    }
                }
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;

        }

        #endregion

    }
}

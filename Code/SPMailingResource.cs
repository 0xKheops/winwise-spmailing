using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace Winwise.SPMailing {
    /// <summary>
    /// Describes a resource to be embeded in an MailMessage
    /// </summary>
    class SPMailingResource {
        
        internal SPMailingResource(String contentId, Byte[] content, String mediaType)
        {
            _contentId = contentId;
            _content = content;
            _mediaType = mediaType;
        }

        #region Fields

        private String _contentId;
        private Byte[] _content;
        private String _mediaType;

        #endregion

        #region Properties

        public String ContentId
        {
            get { return _contentId; }
        }

        public Byte[] Content
        {
            get { return _content; }
        }

        public String MediaType
        {
            get { return _mediaType; }
        }

        #endregion

        #region Methods

        public LinkedResource GetLinkedResource()
        {
            
            MemoryStream ms = new MemoryStream(Content);
            LinkedResource res = new LinkedResource(ms, MediaType);
            res.ContentId = ContentId;
            return res;

        }

        #endregion

    }
}

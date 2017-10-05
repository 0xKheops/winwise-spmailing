using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace Winwise.SPMailing {

    /// <summary>
    /// Context class used to ease lists manipulation within a SPMailing site
    /// </summary>
    class SPMailingContext : IDisposable {

        #region Constructors

        private SPMailingContext(SPWeb web) {
            _web = web;
        }

        #endregion

        #region Fields

        private SPWeb _web = null;
        private SPWeb _rootWeb = null;
        private SPSite _site = null;
        private SPDocumentLibrary _libCategoryTemplates = null;
        private SPDocumentLibrary _libMailingTemplates = null;
        private SPList _listCategories = null;
        private SPList _listMailings = null;
        private SPList _listMailingDefinitions = null;
        private SPList _listRecipientsLists = null;
        private SPList _listContactRecipients = null;
        private SPMailingFieldIds _fieldIds = null;

        #endregion

        #region Properties

        public static SPMailingContext Current {
            get {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Items["SPMailingContext"] == null)
                    HttpContext.Current.Items["SPMailingContext"] = new SPMailingContext(SPContext.Current.Web);
                return HttpContext.Current.Items["SPMailingContext"] as SPMailingContext;
            }
        }

        public SPWeb Web {
            get { return _web; }
        }

        public SPSite Site {
            get {
                if (_site == null)
                    _site = _web.Site;
                return _site;
            }
        }

        public SPWeb RootWeb {
            get {
                if (_rootWeb == null)
                    _rootWeb = Site.RootWeb;
                return _rootWeb;
            }
        }

        public SPMailingFieldIds FieldIds {
            get {
                if (_fieldIds == null)
                    _fieldIds = new SPMailingFieldIds(Web);
                return _fieldIds;
            }
        }

        public SPDocumentLibrary CategoryTemplates {
            get {
                if (_libCategoryTemplates == null)
                    _libCategoryTemplates = SPMailingHelper.GetListFromWeb(Web, "CategoryTemplates") as SPDocumentLibrary;
                return _libCategoryTemplates;
            }
        }

        public SPDocumentLibrary MailingTemplates {
            get {
                if (_libMailingTemplates == null)
                    _libMailingTemplates = SPMailingHelper.GetListFromWeb(Web, "MailingTemplates") as SPDocumentLibrary;
                return _libMailingTemplates;
            }
        }

        public SPList Categories {
            get {
                if (_listCategories == null)
                    _listCategories = SPMailingHelper.GetListFromWeb(Web, "Lists/Categories");
                return _listCategories;
            }
        }


        public SPList Mailings {
            get {
                if (_listMailings == null)
                    _listMailings = SPMailingHelper.GetListFromWeb(Web, "Lists/Mailings");
                return _listMailings;
            }
        }

        public SPList MailingDefinitions {
            get {
                if (_listMailingDefinitions == null)
                    _listMailingDefinitions = SPMailingHelper.GetListFromWeb(Web, "Lists/MailingDefinitions");
                return _listMailingDefinitions;
            }
        }

        public SPList RecipientsLists {
            get {
                if (_listRecipientsLists == null)
                    _listRecipientsLists = SPMailingHelper.GetListFromWeb(Web, "Lists/RecipientsLists");
                return _listRecipientsLists;
            }
        }

        public SPList ContactRecipients {
            get {
                if (_listContactRecipients == null)
                    _listContactRecipients = SPMailingHelper.GetListFromWeb(Web, "Lists/ContactRecipients");
                return _listContactRecipients;
            }
        }

        #endregion

        #region Methods

        public static SPMailingContext GetContext(SPWeb web) {
            return new SPMailingContext(web);
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {

            if (_web != null && (SPContext.Current == null || SPContext.Current.Web != _web))
                _web.Dispose();

            if (_rootWeb != null && (SPContext.Current == null || SPContext.Current.Web != _rootWeb))
                _rootWeb.Dispose();

            if (_site != null && (SPContext.Current == null || SPContext.Current.Site != _site))
                _site.Dispose();

        }

        #endregion

    }
}

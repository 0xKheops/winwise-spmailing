using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Winwise.SPMailing {

    /// <summary>
    /// This class exposes guids of SPMailing site columns within the context of an SPWeb
    /// </summary>
    class SPMailingFieldIds {

        internal SPMailingFieldIds(SPWeb web) {

            CategoryTemplate = SPMailingHelper.GetLookupFieldId(web, CATEGORY_TEMPLATE_PROPERTY_KEY);
            Categories = SPMailingHelper.GetLookupFieldId(web, CATEGORIES_PROPERTY_KEY);
            ContactRecipients = SPMailingHelper.GetLookupFieldId(web, CONTACT_RECIPIENTS_PROPERTY_KEY);
            MailingDefinition = SPMailingHelper.GetLookupFieldId(web, MAILING_DEFINITION_PROPERTY_KEY);
            MailingTemplate = SPMailingHelper.GetLookupFieldId(web, MAILING_TEMPLATE_PROPERTY_KEY);
            RecipientsLists = SPMailingHelper.GetLookupFieldId(web, RECIPIENTS_LISTS_PROPERTY_KEY);
         
        }

        #region Lookup Fields keys for property bag

        public const String CATEGORIES_PROPERTY_KEY = "CATEGORIES";
        public const String CATEGORY_TEMPLATE_PROPERTY_KEY = "CATEGORY_TEMPLATE";
        public const String CONTACT_RECIPIENTS_PROPERTY_KEY = "CONTACT_RECIPIENTS";
        public const String RECIPIENTS_LISTS_PROPERTY_KEY = "RECIPIENTS_LISTS";
        public const String MAILING_DEFINITION_PROPERTY_KEY = "MAILING_DEFINITION";
        public const String MAILING_TEMPLATE_PROPERTY_KEY = "MAILING_TEMPLATE";

        #endregion

        #region Fields

        public Guid CategoryTemplate;
        public Guid Categories;
        public Guid RecipientsLists;
        public Guid MailingTemplate;
        public Guid MailingDefinition;
        public Guid ContactRecipients;

        public Guid AutoGenerate = new Guid("{651ED2BD-485A-4B33-8F3B-B024722A2E5C}");
        public Guid DaysOffset = new Guid("{b907ac62-55ba-49d5-8f4b-5b56869897c2}");
        public Guid Footer = new Guid("{ba93fd94-a566-45cb-8c02-5db8591edd6e}");
        public Guid Header = new Guid("{791ec214-371b-4cfa-baa6-522ad9555a16}");
        public Guid Label = new Guid("{f330b6d9-ddd0-42e4-8dc5-7149e8966574}");
        public Guid Log = new Guid("{82f5fa2d-36a8-4492-b644-cb882516cbc6}");
        public Guid MailBody = new Guid("{4acbd4d1-09d0-4a37-8c38-0b72bf73c96c}");
        public Guid MailSubject = new Guid("{ca808133-e705-49d0-8e99-e41c835afc44}");
        public Guid Recipients = new Guid("{35831CB4-007F-4F1F-B04C-6BD3B8D1FF01}");
        public Guid PeopleAndGroupsRecipients = new Guid("{a3fa41db-8459-465d-b765-da99465dcc86}");
        public Guid Query = new Guid("{68cc8631-6945-4e36-92f9-5d6146f0d451}");
        public Guid Sent = new Guid("{b1fd3565-fc9a-4e3e-8ad8-746b4ab75371}");
        public Guid Type = new Guid("{91e460f8-7b39-4ae7-848e-f7264fc5345e}");
        public Guid Url = new Guid("{7270e400-190a-4929-a9b4-047a18c13de8}");
        public Guid ViewFields = new Guid("{51634169-1a97-4dce-b82b-a36946785e6c}");
        public Guid GenerationUser = new Guid("{cb17075f-3583-41b5-b650-c6910fd58c21}");
        public Guid From_Adress = new Guid("{CD01008D-3242-40B5-B9BD-4AABD7D474DE}");
        public Guid From_DisplayName = new Guid("{E4DB45C5-8576-411C-AE84-4B6CEF9C864D}");
        public Guid ReplyTo_Adress = new Guid("{E3D0F43E-CE3C-4108-BA4C-AEBE583DC065}");
        public Guid ReplyTo_DisplayName = new Guid("{8A1DAA53-6E83-4FAE-B360-084DCD0756F2}");

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Utility.Constants
{
    public static class StaticEnum
    {
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum TransactionType
        {
            Purchase,
            Payout,
            Income
        }

        public enum StripeAccountType
        {
            express,
            standard,
            custom
        }

        public enum StripeAccountLinkType
        {
            account_onboarding,
            account_update
        }
    }

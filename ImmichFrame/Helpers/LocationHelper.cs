using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ImmichFrame.Helpers
{
    public class LocationHelper
    {
        static List<RegionInfo> countries_info = new List<RegionInfo>();
        static Dictionary<string, string> usa_states = new Dictionary<string, string>();
        static Dictionary<string, string> canada_states = new Dictionary<string, string>();

        static LocationHelper()
        {
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                countries_info.Add(new RegionInfo(culture.TextInfo.CultureName));
            }

            usa_states.Add("Alabama", "AL");
            usa_states.Add("Alaska", "AK");
            usa_states.Add("Arizona", "AZ");
            usa_states.Add("Arkansas", "AR");
            usa_states.Add("California", "CA");
            usa_states.Add("Colorado", "CO");
            usa_states.Add("Connecticut", "CT");
            usa_states.Add("Delaware", "DE");
            usa_states.Add("District of Columbia", "DC");
            usa_states.Add("Florida", "FL");
            usa_states.Add("Georgia", "GA");
            usa_states.Add("Hawaii", "HI");
            usa_states.Add("Idaho", "ID");
            usa_states.Add("Illinois", "IL");
            usa_states.Add("Indiana", "IN");
            usa_states.Add("Iowa", "IA");
            usa_states.Add("Kansas", "KS");
            usa_states.Add("Kentucky", "KY");
            usa_states.Add("Louisiana", "LA");
            usa_states.Add("Maine", "ME");
            usa_states.Add("Maryland", "MD");
            usa_states.Add("Massachusetts", "MA");
            usa_states.Add("Michigan", "MI");
            usa_states.Add("Minnesota", "MN");
            usa_states.Add("Mississippi", "MS");
            usa_states.Add("Missouri", "MO");
            usa_states.Add("Montana", "MT");
            usa_states.Add("Nebraska", "NE");
            usa_states.Add("Nevada", "NV");
            usa_states.Add("New Hampshire", "NH");
            usa_states.Add("New Jersey", "NJ");
            usa_states.Add("New Mexico", "NM");
            usa_states.Add("New York", "NY");
            usa_states.Add("North Carolina", "NC");
            usa_states.Add("North Dakota", "ND");
            usa_states.Add("Ohio", "OH");
            usa_states.Add("Oklahoma", "OK");
            usa_states.Add("Oregon", "OR");
            usa_states.Add("Pennsylvania", "PA");
            usa_states.Add("Rhode Island", "RI");
            usa_states.Add("South Carolina", "SC");
            usa_states.Add("South Dakota", "SD");
            usa_states.Add("Tennessee", "TN");
            usa_states.Add("Texas", "TX");
            usa_states.Add("Utah", "UT");
            usa_states.Add("Vermont", "VT");
            usa_states.Add("Virginia", "VA");
            usa_states.Add("Washington", "WA");
            usa_states.Add("West Virginia", "WV");
            usa_states.Add("Wisconsin", "WI");
            usa_states.Add("Wyoming", "WY");

            canada_states.Add("Alberta", "AB");
            canada_states.Add("British Columbia", "BC");
            canada_states.Add("Manitoba", "MB");
            canada_states.Add("New Brunswick", "NB");
            canada_states.Add("Newfoundland and Labrador", "NL");
            canada_states.Add("Northwest Territories", "NT");
            canada_states.Add("Nova Scotia", "NS");
            canada_states.Add("Nunavut", "NU");
            canada_states.Add("Ontario", "ON");
            canada_states.Add("Prince Edward Island", "PE");
            canada_states.Add("Quebec", "QC");
            canada_states.Add("Saskatchewan", "SK");
            canada_states.Add("Yukon", "YT");
        }

        public static string GetCountryCode(string country)
        {
            string target_country = country;
            if (country == "United States of America")
            {
                target_country = "United States";
            }
            RegionInfo? country_info = countries_info.Where(info => info.EnglishName == target_country).FirstOrDefault();
            return country_info?.ThreeLetterISORegionName ?? country;
        }

        public static string GetUSAStateCode(string state)
        {
            return usa_states.ContainsKey(state) ? usa_states[state] : state;
        }

        public static string GetCANStateCode(string state)
        {
            return canada_states.ContainsKey(state) ? canada_states[state] : state;
        }

        public static string GetLocationString(ImmichFrame.Models.ExifResponseDto exifInfo)
        {
            string country = GetCountryCode(exifInfo.Country);
            string state = exifInfo.State?.Split(", ").Last() ?? string.Empty;
            if (country == "USA")
            {
                state = GetUSAStateCode(state);
            }
            else if (country == "CAN")
            {
                state = GetCANStateCode(state);
            }

            var locationData = new[] {
                    exifInfo.City,
                    state,
                    country
                }.Where(x => !string.IsNullOrWhiteSpace(x));
            return string.Join(", ", locationData);
        }
    }
}

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TPToolsLibrary
{
    public class GSheets
    {

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        static readonly string ApplicationName = "TPTools";

        static readonly string SpreadsheetId = "1prz7bB3qG9m9h1kZR81ECLTC3BosoE_YvyRmnGli8dU";

        static readonly string DemoSheet = "DemoPortals";

        static SheetsService sheetsService;


        static void Initialize()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(@"./Resources/client_secrets_sheets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

        }

        public static void CreateEntry(string portalName, string portalId, string portalURL, string datecreated)
        {
            Initialize();
            try
            {
                var range = $"{DemoSheet}!A:D";
                var valueRange = new ValueRange();

                var oblist = new List<object>() { portalName, portalId, portalURL, datecreated };
                valueRange.Values = new List<IList<object>> { oblist };

                var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendReponse = appendRequest.Execute();
            }
            catch(Exception e)
            {
                Logger.LogError(e.ToString());
            }
            
        }


        static void ReadEntries()
        {
            var range = $"{DemoSheet}!A:F";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    sheetsService.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Print columns A to F, which correspond to indices 0 and 4.
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", row[0], row[1], row[2], row[3], row[4], row[5]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
    }
}
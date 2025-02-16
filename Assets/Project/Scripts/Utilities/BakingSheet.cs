using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class BakingSheet 
{
    public static async Task<string>  GetCsv(string linkSheet, string tabName) {
        // Create the full URL for the specific tab in the Google Sheet
        string csvUrl = $"{linkSheet}/gviz/tq?tqx=out:csv&sheet={tabName}";

        // Use HttpClient to fetch the CSV content
        using (HttpClient client = new HttpClient()) {
            try {
                HttpResponseMessage response = await client.GetAsync(csvUrl);
                response.EnsureSuccessStatusCode();

                // Read the content as a string
                string csvContent = await response.Content.ReadAsStringAsync();

                return csvContent;
            } catch (HttpRequestException e) {
                // Handle any potential errors here
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
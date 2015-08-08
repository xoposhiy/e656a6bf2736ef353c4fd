﻿using System;
using System.Net;
using System.Text;
using SomeSecretProject.IO;

namespace Emulator.Posting
{
    public static class HttpHelper
    {
        private static readonly WebClient client = new WebClient();

        public static Problem GetProblem(int problemnum)
        {
            var url = string.Format("http://icfpcontest.org/problems/problem_{0}.json", problemnum);
            var data = client.DownloadString(url);
            return data.ParseAsJson<Problem>();
        }

        public static bool SendOutput(DavarAccount account, Output output)
        {
            var authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(":" + account.ApiToken));
            client.Headers.Add("Authorization", "Basic " + authInfo);

            client.Headers.Add("Content-Type", "application/json");

            var url = string.Format("https://davar.icfpcontest.org/teams/{0}/solutions", account.TeamId);
            var data = "[" + output.ToJson() + "]";
            var result = client.UploadString(url, "POST", data);

            return result == "created";
        }
    }
}
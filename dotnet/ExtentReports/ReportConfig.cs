﻿namespace RelevantCodes.ExtentReports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Source;
    using Support;

    public class ReportConfig
    {
        private ReportInstance report;

        public ReportConfig InsertJs(string Script)
        {
            Script = "<script type='text/javascript'>" + Script + "</script>";
            report.UpdateSource(report.Source.Replace(ExtentFlag.GetPlaceHolder("customscript"), Script + ExtentFlag.GetPlaceHolder("customscript")));

            return this;
        }

        public ReportConfig InsertStyles(string Styles)
        {
            Styles = "<style type='text/css'>" + Styles + "</style>";
            report.UpdateSource(report.Source.Replace(ExtentFlag.GetPlaceHolder("customcss"), Styles + ExtentFlag.GetPlaceHolder("customcss")));

            return this;
        }

        public ReportConfig AddStylesheet(string StylesheetPath)
        {
            string link = "<link href='file:///" + StylesheetPath + "' rel='stylesheet' type='text/css' />";

            if (StylesheetPath.Substring(0, 1).Equals(".") || StylesheetPath.Substring(0, 1).Equals("/"))
            {
                link = "<link href='" + StylesheetPath + "' rel='stylesheet' type='text/css' />";
            }

            report.UpdateSource(report.Source.Replace(ExtentFlag.GetPlaceHolder("customcss"), link + ExtentFlag.GetPlaceHolder("customcss")));

            return this;
        }

        public ReportConfig ReportHeadline(string Headline)
        {
            int maxlength = 70;

            if (Headline.Length >= maxlength)
                Headline = Headline.Substring(0, maxlength - 1);

            string pattern = ExtentFlag.GetPlaceHolder("headline") + ".*" + ExtentFlag.GetPlaceHolder("headline");
            Headline = pattern.Replace(".*", Headline);
            report.UpdateSource(report.Source.Replace(RegexMatcher.GetNthMatch(report.Source, pattern, 0), Headline));

            return this;
        }

        public ReportConfig ReportName(string Name)
        {
            int maxlength = 20;

            if (Name.Length >= maxlength)
                Name = Name.Substring(0, maxlength - 1);

            string pattern = ExtentFlag.GetPlaceHolder("logo") + ".*" + ExtentFlag.GetPlaceHolder("logo");
            Name = pattern.Replace(".*", Name);
            report.UpdateSource(report.Source.Replace(RegexMatcher.GetNthMatch(report.Source, pattern, 0), Name));

            return this;
        }

        public ReportConfig DocumentTitle(string Title)
        {
            string docTitle = "<title>.*</title>";
            report.UpdateSource(report.Source.Replace(RegexMatcher.GetNthMatch(report.Source, docTitle, 0), docTitle.Replace(".*", Title)));

            return this;
        }

        public ReportConfig(ReportInstance report)
        {
            this.report = report;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestPDF.Infrastructure
{
    internal class PageContext : IPageContext
    {
        public const string DocumentLocation = "document";
        
        private List<DocumentLocation> Locations { get; } = new();
        public int CurrentPage { get; private set; }

        internal void ResetPageNumber()
        {
            CurrentPage = 0;
        }
        
        internal void IncrementPageNumber()
        {
            CurrentPage++;
            SetSectionPage(DocumentLocation);
        }

        public void SetSectionPage(string name)
        {
            var location = GetLocation(name);

            if (location == null)
            {
                location = new DocumentLocation
                {
                    Name = name,
                    PageStart = CurrentPage,
                    PageEnd = CurrentPage
                };
                
                Locations.Add(location);
            }

            if (location.PageEnd < CurrentPage)
                location.PageEnd = CurrentPage;
        }

        public DocumentLocation? GetLocation(string name)
        {
            return Locations.Find(x => x.Name == name);
        }
    }
}
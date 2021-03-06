using System;
using System.Collections.Generic;
using System.Text;

namespace KittyCatApp.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string InputText { get; set; }
        public string Language { get; set; }
        public string TranslatedText { get; set; }
     
    }
}

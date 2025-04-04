using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Text.RegularExpressions;

namespace SICExcelAddInPRO
{
    public partial class ThisAddIn
    {
        private Dictionary<string, string> originalValues = new Dictionary<string, string>();

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        // Convert to Alphanumeric - remove non-alphanumeric characters
        public void ConvertToAlphanumeric(Excel.IRange range)
        {
            foreach (Excel.Range cell in range.Cells)
            {
                if (cell.Value2 != null)
                {
                    string originalValue = cell.Value2.ToString();
                    originalValues[cell.Address] = originalValue;  // Store original value

                    // Sanitize value
                    string sanitizedValue = Regex.Replace(originalValue, @"[^a-zA-Z0-9]", "");
                    cell.Value2 = sanitizedValue;
                }
            }
        }

        // Revert to Original - restore the original values
        public void RevertToOriginal(Excel.IRange range)
        {
            foreach (Excel.Range cell in range.Cells)
            {
                if (originalValues.ContainsKey(cell.Address))
                {
                    // Restore the original value
                    cell.Value2 = originalValues[cell.Address];
                }
            }
        }

        #endregion
    }
}

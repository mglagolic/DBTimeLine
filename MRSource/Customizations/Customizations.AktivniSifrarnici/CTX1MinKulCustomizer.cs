using System.Linq;
using System.Windows.Forms;
using Customizations.Core.Attributes;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;

namespace AktivniSifrarnici
{
    [Customization(CustomizationKey = "Ctx1MinKul")]
    public class Ctx1MinKulCustomizer : Customizations.Core.Customizer
    {
        #region Database customization

        
        [MethodActivationCustomization(ActivationKey = "CreateTimeLine")]
        public void CreateTimeLine(Dictionary<string, object> inputs)
        {
            DBTimeLiner dbTimeLiner = (DBTimeLiner)inputs["DBTimeLiner"];

            var modules = dbTimeLiner.DBModules.FindAll(m => m.ModuleKey == "dbo");
            foreach (var module in modules)
            {
                CreateTimeLine(module);
            }
        }

        private void CreateTimeLine(IDBModule module)
        {
            IDBSchema sch = module.AddSchema(module.DefaultSchemaName, new DBSchemaDescriptor());

            IDBTable tblCases = sch.AddTable("tblCases", new DBTableDescriptor());

            tblCases.AddField("Ctx1MinKul_Opis", new DBFieldDescriptor() {  FieldType = new DBFieldTypeNvarchar(), Nullable = true, Size = 512 }, 
                new DBRevision(new DateTime(2016,9,19), 1, eDBRevisionType.Create));
            
                        
            var view = sch.AddView("vw_cus_cus_CustomView", new DBViewDescriptor() { Body = 
@"SELECT 
    Broj = 1
"
, WithSchemaBinding = true },
                    new DBRevision(new DateTime(2016, 9, 20), 0, eDBRevisionType.Create));

            view.AddRevision(new DBRevision(new DateTime(2016, 9, 20), 1, eDBRevisionType.Modify),
                new DBViewDescriptor() { Body = 

@"SELECT 
Broj = 2

", WithSchemaBinding = false });
            
        }

        #endregion

        #region UI Customization
        
        [MethodActivationCustomization(ActivationKey = "FormLoaded")]
        public void FormLoaded(Dictionary<string, object> inputs)
        {
            Form form = (Form) inputs["Form"];
            
            Panel mainPanel = (Panel) form.Controls.Cast<Control>().ToList().Find(ctrl =>  ctrl.Tag != null && (string) ctrl.Tag == "mainPanel");
            var frmCustomizationContainer = new FormUICustomizationsContainer();
            var gb = frmCustomizationContainer.Ctx1MinKul_groupBox1;
            mainPanel.Controls.Add(gb);
            gb.BringToFront();

        }

        #endregion
    }
}

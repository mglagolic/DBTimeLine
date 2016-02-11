using MRFramework.MRPersisting.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRFramework.MRPersisting.Core;
using MRFramework.MRPersisting;

namespace Client
{
    class Program
    {
        public class myPersister : MRFramework.MRPersisting.MRPersister
        {
            public myPersister()
            {
             
            }

            public override string DataBaseTableName
            {
                get 
                {
                    return "Common.BigOne";
                }
            }
            public override string SQLBase
            {
                get
                {
                    return "select * from common.bigone" ;
                }
            }
            
            protected override string SYS_GUID
            {
                get
                {
                    return base.SYS_GUID;
                }
                set
                {
                    base.SYS_GUID = value;
                }
            }
        }
        

        static void Main(string[] args)
        {
            MRC.GetInstance().ConnectionString = Properties.Settings.Default[Properties.Settings.Default.DefaultConnectionString].ToString();
            MRC.GetInstance().ProviderName = Properties.Settings.Default[Properties.Settings.Default.DefaultProvider].ToString();

            var per = new myPersister();
            Object o;
            using (var cnn = MRC.GetConnection())
            {
                cnn.Open();
                per.CNN = cnn;
                per.PageSize = 100000;
                //o = per.GetDataPage(0);

                
                

                //dlo.ColumnValues.Add("Naziv", "pero321123");
                var trn = per.CNN.BeginTransaction();
                //per.Insert(dlo, trn );
                
                
                //per.Where = "naziv = 'pero321123'";
                var dlo2 = per.GetDataPage(0, trn);
                

                //var key = dlo2.Keys.First<Object>();
                //var dlo22 = dlo2[key];
                foreach (var kv in dlo2)
                {
                    per.Delete(kv.Value, trn, false);
                }

                //3
                
                //per.Delete(dlo2, trn);

                //per.Insert(dlo, trn);
                //per.Delete(dlo2[0], trn);
                //trn.Commit();
                trn.Rollback();
                
            }
            

        }
    }
}

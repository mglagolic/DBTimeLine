using MRFramework.MRPersisting.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MRFramework.MRPersisting.Factory.MRC.GetInstance().ConnectionString = Properties.Settings.Default.ConnectionStringTest;
            MRFramework.MRPersisting.Factory.MRC.GetInstance().ProviderName = Properties.Settings.Default.ProviderSqlServer;

            var per = new myPersister();
            Object o;
            using (var cnn = MRC.GetConnection())
            {
                cnn.Open();
                per.CNN = cnn;
                o = per.GetDataPage(0);

                var dlo = new MRFramework.MRPersisting.MRDLO();

                dlo.ColumnValues.Add("Naziv", "pero321123");
                var trn = per.CNN.BeginTransaction();
                per.Insert(dlo, trn );
                
                
                per.Where = "naziv = 'pero321123'";
                var dlo2 = per.GetData(trn);

                var key = dlo2.Keys.First<Object>();
                var dlo22 = dlo2[key];
                
                //per.Delete(dlo2, trn);

                //per.Insert(dlo, trn);
                //per.Delete(dlo2[0], trn);
                trn.Commit();
                
            }
            

        }
    }
}

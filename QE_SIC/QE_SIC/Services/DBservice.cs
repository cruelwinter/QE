using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using QE.Models;

namespace QE.Services
{
    public class DBservice:IDBservice
    {
        public T findRecordByID<T>(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID", id.ToString()}
            };

            return Query<T>(DS).FirstOrDefault();
        }


        public List<T> findRecordsBySingleParm<T>(string keyParm, int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {keyParm, id.ToString()}
            };

            return Query<T>(DS);
        }


        public T findActiveRecordByID<T>(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "1"},
                {"ID", id.ToString()}
            };

            return Query<T>(DS).FirstOrDefault();
        }


        public List<T> findActiveRecordsByID<T>(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID", id.ToString()},
                {"ACTIVE", "1"}
            };

            return Query<T>(DS);
        }


        public List<T> findActiveRecords<T>()
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "1"}
            };

            return Query<T>(DS);
        }


        public List<T> findActiveCurrentRecords<T>()
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "1"},
                {"TERM", GetCurrentTerm().ID.ToString()}
            };

            return Query<T>(DS);
        }

        public List<T> findActiveCurrentRecordsByID<T>(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID", id.ToString()},
                {"ACTIVE", "1"},
                {"TERM", GetCurrentTerm().ID.ToString()}
            };

            return Query<T>(DS);
        }

        public List<T> findActiveCurrentRecordsBySingleParm<T>(string keyParm, int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {keyParm, id.ToString()},
                {"ACTIVE", "1"},
                {"TERM", GetCurrentTerm().ID.ToString()}
            };

            return Query<T>(DS);
        }

        public List<T> findActiveRecordsBySingleParm<T>(string keyParm, int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "1"},
                {keyParm, id.ToString()}
            };

            return Query<T>(DS);
        }


        public List<T> findActiveRecordsByTerm<T>(int term_id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "1"},
                {"TERM", term_id.ToString()}
            };

            return Query<T>(DS);
        }


        public List<T> findALLRecords<T>()
        {
            Dictionary<string, string> DS = null;
            return Query<T>(DS);
        }


        public List<T> Query<T>(Dictionary<string, string> DS)
        {
            string table = typeof(T).Name;
            
            try { return WillORM.GetDBSource(table, WillORM.QueryBuilder(null, table, DS, null)).Cast<T>().ToList();}
            catch(Exception e){ return new List<T>(); }
        }


        public bool addRecord(object newRecord)
        {
            try { return WillORM.InsertRecord(newRecord)> 0? true:false; }
            catch { return false; }
        }

        public int addRecordReturnID(object newRecord)
        {
            try { return WillORM.InsertRecord(newRecord); }
            catch { return 0; }
        }


        public bool removeRecord<T>(int id)
        {
            string table = typeof(T).Name;

            try { return WillORM.DeleteRecord(table, id); }
            catch { return false; }
        }


        public bool updateRecord(object record)
        {
            try { return WillORM.UpdateRecord(record); }
            catch { return false; }
        }


        public bool updateRecordByID<T>(Dictionary<string, string> parms, int id)
        {
            string table = typeof(T).Name;
            
            try { return WillORM.UpdateRecord(table, parms, id); }
            catch { return false; }
        }


        public bool InactiveRecord(string table, int id, int user_id)
        {

            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ACTIVE", "0"},
                {"MODIFY_BY", user_id.ToString()},
                {"MODIFY_DATE", DateTime.Now.ToString()},
            };

            try { return WillORM.UpdateRecord(table, DS, id); }
            catch { return false; }
        }

        
        public static TERM GetCurrentTerm()
        {
            List<MutliQuery> MQ = new List<MutliQuery>()
            {
                {new MutliQuery("ACTIVE","=","1") },
                {new MutliQuery("TERM_START","<=","CURRENT_TIMESTAMP") },
                {new MutliQuery("TERM_END",">=","CURRENT_TIMESTAMP") }
            };

            try { return WillORM.GetDBSource("TERM", WillORM.QueryBuilder(null, "TERM", MQ, null)).Cast<TERM>().First(); }
            catch (Exception e) { return new TERM(); }
        }
    }
}
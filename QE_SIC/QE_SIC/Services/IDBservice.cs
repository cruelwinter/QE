using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Services
{
    public interface IDBservice
    {
        T findRecordByID<T>(int id);
        T findActiveRecordByID<T>(int id);
        List<T>findRecordsBySingleParm<T>(string keyParm, int id);
        List<T> findActiveRecords<T>();
        List<T> findALLRecords<T>();
        List<T> findActiveRecordsByID<T>(int id);
        List<T> findActiveCurrentRecords<T>();
        List<T> findActiveCurrentRecordsByID<T>(int id);
        List<T> findActiveCurrentRecordsBySingleParm<T>(string keyParm, int id);
        List<T> findActiveRecordsByTerm<T>(int term_id);
        List<T> findActiveRecordsBySingleParm<T>(string keyParm, int id);
        List<T> Query<T>(Dictionary<string, string> DS);
        bool addRecord(object newRecord);
        int addRecordReturnID(object newRecord);
        bool removeRecord<T>(int id);
        bool updateRecord(object record);
        bool updateRecordByID<T>(Dictionary<string, string> parms, int id);
        bool InactiveRecord(string table, int id, int user_id);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;
using QE.Models;
using System.ComponentModel.DataAnnotations;

namespace QE.Services
{
    public class WillORM
    {

        //query DB get all. easy use
        public static List<object> GetDBSource(string table)
        {
            string query = "select * from " + table; return GetDBSource(table, query);
        }

        //query DB, work with query builder 
        public static List<object> GetDBSource(string table, string query)
        {
            List<object> list = new List<object>();

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();


                if (rdr.HasRows)
                {
                    List<PropertyInfo> properties = Type.GetType("QE.Models." + table).GetProperties().ToList(); //get properties of the model
                    List<PropertyInfo> requiredProperties = new List<PropertyInfo>();
                    List<string> requiredColumns = new List<string>();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        requiredColumns.Add(rdr.GetName(i));
                    }

                    foreach (var property in properties)
                    {
                        if (requiredColumns.Contains(property.Name))
                            requiredProperties.Add(property);
                    }

                    while (rdr.Read())
                    {
                        var model = Activator.CreateInstance(Type.GetType("QE.Models." + table)); //create new instance for each db record read

                        foreach (var property in requiredProperties) //convert db record value base on property type
                        {
                            if (property.PropertyType == typeof(string))
                            {
                                if (!string.IsNullOrEmpty(rdr[property.Name].ToString()))
                                    property.SetValue(model, rdr[property.Name].ToString(), null);
                                else
                                    property.SetValue(model, Constant.DEF_STRING);
                            }
                            else if (property.PropertyType == typeof(int))
                            {
                                int a = Constant.DEF_INT;
                                int.TryParse(rdr[property.Name].ToString(), out a);
                                property.SetValue(model, a, null);
                            }
                            else if (property.PropertyType == typeof(bool))
                            {
                                property.SetValue(model, bool.Parse(rdr[property.Name].ToString()), null);
                            }
                            else if (property.PropertyType == typeof(decimal))
                            {
                                    decimal a = Constant.DEF_DEC;
                                    decimal.TryParse(rdr[property.Name].ToString(), out a);
                                    property.SetValue(model, a, null);
                            }
                            else if (property.PropertyType == typeof(DateTime))
                            {
                                if (!string.IsNullOrEmpty(rdr[property.Name].ToString()))
                                    property.SetValue(model, Convert.ToDateTime(rdr[property.Name]), null);
                                else
                                    property.SetValue(model, Constant.DEF_DATETIME);
                            }
                        }
                        list.Add(model);
                    }
                    con.Close();
                    return list;
                }
                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0} ", e.Message);
                return null;
            }

        } //e.g.GetDBSource(QE_USER, QueryBuilder(null, QE_USER, new Dictionary(admin, password), null));

        //use for complicated query, easy use
        public static string QueryBuilder(string parameter, string table, Dictionary<string, string> parameters, string[] orders)
        {
            List<MutliQuery> parm = new List<MutliQuery>();
            if (parameters != null && !string.IsNullOrEmpty(parameters.FirstOrDefault().Key))
            {
                foreach (var p in parameters)
                {
                    MutliQuery model = new MutliQuery(p.Key, null, p.Value);
                    parm.Add(model);
                }
            }

            return QueryBuilder(parameter, table, parm, orders);
        }

        //use for complicated query
        public static string QueryBuilder(string parameter, string table, List<MutliQuery> parameters, string[] orders)
        {
            parameter = string.IsNullOrEmpty(parameter) ? " * " : parameter; //select * if not specifed
            string query = "select " + parameter + " from " + table;

            if (parameters != null && parameters.Any() && !string.IsNullOrEmpty(parameters.FirstOrDefault().field1))
            {
                query += " where ";

                foreach (var parm in parameters)
                {

                    query += parm.field1;
                    query += " ";
                    query += string.IsNullOrEmpty(parm.function) ? "=" : parm.function; //e.g. =/<>/between/in/like. use = if not specifed
                    query += " ";
                    query += parm.field2;
                    query += " and ";

                }
                query = query.Substring(0, query.Count() - 5); //remove the last 'and'
            }

            if (orders != null && orders.Any() && !string.IsNullOrEmpty(orders.FirstOrDefault()))
            {
                query += " order by ";

                foreach (var order in orders)
                {
                    query += order;
                    query += ", ";
                }

                query = query.Substring(0, query.Count() - 2); //remove the last ','
            }

            return query;
        }

        //modify a record. easy use
        public static bool UpdateRecord(object obj)
        {
            try
            {
                Type objType = obj.GetType();

                List<MutliQuery> parms = new List<MutliQuery>();
                List<MutliQuery> keys = new List<MutliQuery>();

                foreach (var prop in obj.GetType().GetProperties()) //get properties of the model
                {
                    var attribute = Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) as KeyAttribute;
                    if (attribute == null) //check if it is key(auto increment), do not update
                    {
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            parms.Add(new MutliQuery(prop.Name, null, dateCheck(prop.GetValue(obj))));
                        }
                        else
                        {
                            string key = prop.GetValue(obj, null) != null ? prop.GetValue(obj, null).ToString() : string.Empty;
                            parms.Add(new MutliQuery(prop.Name, null, key));
                        }
                    }
                    else
                    {
                        keys.Add(new MutliQuery(prop.Name, null, prop.GetValue(obj, null).ToString()));
                    }
                }

                return UpdateRecord(objType.Name, parms, keys);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0} ", e.Message);
                return false;
            }
        }

        //inactive a record. easy use
        public static bool InactiveRecord(string table, int id, int user_id)
        {
            List<MutliQuery> parm = new List<MutliQuery>();
            parm.Add(new MutliQuery("ACTIVE", null, "False"));
            parm.Add(new MutliQuery("MODIFY_BY", null, user_id.ToString()));
            parm.Add(new MutliQuery("MODIFY_DATE", null, DateTime.Now.ToString()));

            List<MutliQuery> key = new List<MutliQuery>(); key.Add(new MutliQuery("ID", null, id.ToString()));

            return UpdateRecord(table, parm, key);
        }
        
        //modify a record. quick use
        public static bool UpdateRecord(string table, Dictionary<string, string>parms, int id)
        {
            List<MutliQuery> conditions = new List<MutliQuery>();
            List<MutliQuery> keys = new List<MutliQuery>();

            if (parms != null && !string.IsNullOrEmpty(parms.FirstOrDefault().Key))
            {
                foreach (var p in parms)
                {
                    MutliQuery model = new MutliQuery(p.Key, null, p.Value);
                    conditions.Add(model);
                }
            }
            
            MutliQuery key = new MutliQuery("ID", null, id.ToString());
            keys.Add(key);

            return UpdateRecord(table, conditions, keys);
        }

        //modify or inactive a record for complicated conditions
        public static bool UpdateRecord(string table, List<MutliQuery> parameters, List<MutliQuery> keys)
        {
            try
            {
                string query = "update " + table;

                if (parameters.Any())
                {

                    query += " set ";

                    foreach (var p in parameters)
                    {
                        if (p.field2.Any())
                        {
                            if (p.field2.GetType() == typeof(DateTime))
                            {
                                p.field2 = dateCheck(p.field2);
                            }

                            query += p.field1;
                            query += string.IsNullOrEmpty(p.function) ? " = '" : p.function;
                            query += p.field2;
                            query += "', ";
                        }
                    }
                    query = query.Substring(0, query.Count() - 2); //remove the last ','
                }

                if (keys.Any())
                {
                    query += " where ";

                    foreach (var k in keys)
                    {

                        query += k.field1;
                        query += string.IsNullOrEmpty(k.function) ? " = '" : k.function;
                        query += k.field2;
                        query += "', ";
                    }
                    query = query.Substring(0, query.Count() - 2); //remove the last ','
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                bool result = (cmd.ExecuteNonQuery() == 1);
                con.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0} ", e.Message);
                return false;
            }
        }

        //delete Record, easy use
        public static bool DeleteRecord(string table, int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>() {
                {"ID", id.ToString()}
            };

            return DeleteRecord(table, DS);
        }

        //delete Record, easy use
        public static bool DeleteRecord(string table, Dictionary<string, string> parms)
        {
            List<MutliQuery> conditions = new List<MutliQuery>();

            if (parms != null && !string.IsNullOrEmpty(parms.FirstOrDefault().Key))
            {
                foreach (var p in parms)
                {
                    MutliQuery model = new MutliQuery(p.Key, null, p.Value);
                    conditions.Add(model);
                }
            }
            
            return DeleteRecord(table, conditions);
        }

        //delete Record
        public static bool DeleteRecord(string table, List<MutliQuery> parameters) {
            
            try
            {
                string query = "Delete From " + table;

                if (parameters.Any())
                {

                    query += " where ";

                    foreach (var p in parameters)
                    {
                        if (p.field2.Any())
                        {
                            if (p.field2.GetType() == typeof(DateTime))
                            {
                                p.field2 = dateCheck(p.field2);
                            }

                            query += p.field1;
                            query += string.IsNullOrEmpty(p.function) ? " = '" : p.function;
                            query += p.field2;
                            query += "', ";
                        }
                    }
                    query = query.Substring(0, query.Count() - 2); //remove the last ','
                }
                
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //cmd.CommandText = "SELECT @@IDENTITY";

                //check existance
                query = QueryBuilder(null, table, parameters, null); 
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                bool result = rdr.HasRows? false : true;
                con.Close();
                return result;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        //Insert Record
        public static int InsertRecord(object obj)
        {
            try
            {
                Type objType = obj.GetType();

                string column = "";
                string value = "";

                foreach (var prop in obj.GetType().GetProperties()) //get properties of the model
                {
                    var attribute = Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) as KeyAttribute;
                    if (attribute == null) //check if it is key(auto increment), do not insert
                    {
                        column += prop.Name + ",";
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            value += "'" + dateCheck(prop.GetValue(obj)) + "',";
                        }
                        else
                        {
                            value += "'" + prop.GetValue(obj, null) + "',";
                        }
                    }
                }
                if (column != "")
                    column = column.Substring(0, column.Count() - 1);
                if (value != "")
                    value = value.Substring(0, value.Count() - 1);

                string query = "insert into " + objType.Name + " (" + column + ") values (" + value + ");";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0} ", e.Message);
                return 0;
            }
        }

        public static string dateCheck(object date)
        {
            string dt = "";

            DateTime dateValue = Constant.DEF_DATETIME;
            DateTime.TryParse(date.ToString(), out dateValue);
            dt = dateValue.ToString(Constant.DATETIME_FORMAT);

            return dt;
        }

        //count record, work with query buider
        public static int CountRecord(string table, string query) {

            int count = 0;
            
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                count = (Int32)cmd.ExecuteScalar();

                con.Close();
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0} ", e.Message);
                return count;
            }
        }
    }
}

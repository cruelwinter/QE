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
    public class KennyORM
    {

        //e.g. GetDBSource("QE_USER")
        public static List<object> GetDBSource(string table) //get all active record from table
        {
            List<object> list = new List<object>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
                con.Open();

                string query = "select * from " + table + " where active=1";
                
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    PropertyInfo[] properties = Type.GetType("QE.Models." + table).GetProperties(); //get properties of the model

                    while (rdr.Read())
                    {
                        var model = Activator.CreateInstance(Type.GetType("QE.Models." + table)); //create new instance for each db record read

                        foreach (var property in properties) //convert db record value base on property type
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
            
        } 

        //e.g. InsertRecord(QE_USER)
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
                            DateTime dateValue = Constant.DEF_DATETIME;
                            DateTime.TryParse(prop.GetValue(obj).ToString(), out dateValue);

                            value += "'" + dateValue.ToString(Constant.DATETIME_FORMAT) + "',";
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

        } //insert record to related table

        public static bool UpdateRecord(object obj)
        {
            try
            {
                Type objType = obj.GetType();

       
                string value = "";
  
                string key = "";
                string keyValue = "";

                foreach (var prop in obj.GetType().GetProperties()) //get properties of the model
                {
                    var attribute = Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) as KeyAttribute;
                    if (attribute == null) //check if it is key(auto increment), do not update
                    {
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            DateTime dateValue = Constant.DEF_DATETIME;
                            DateTime.TryParse(prop.GetValue(obj).ToString(), out dateValue);

                            value += prop.Name + "='" + dateValue.ToString(Constant.DATETIME_FORMAT) + "',";
                        }
                        else
                        {
                            value += prop.Name + "='" + prop.GetValue(obj, null) + "',";
                        }
                        
                    }
                    else
                    {
                        key = prop.Name;
                        keyValue = prop.GetValue(obj, null).ToString();
                    }
                }

                if (value != "")
                    value = value.Substring(0, value.Count() - 1);

                string query = "update "+objType.Name+" set "+value+" where "+key+"="+keyValue;

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

        //e.g. InactiveRecord(QE_USER)
        public static bool InactiveRecord(object obj)
        {
            try
            {
                Type objType = obj.GetType();
               
                string key = "";
                string keyName = "";
                foreach (var prop in obj.GetType().GetProperties()) //get properties of the model
                {
                    var attribute = Attribute.GetCustomAttribute(prop, typeof(KeyAttribute)) as KeyAttribute;
                    if (attribute != null) 
                    {
                        keyName = prop.Name;
                        key = prop.GetValue(obj, null).ToString();
                        break;
                    }
                }
                string query = "update " + objType.Name + " set active=0 where " + keyName + "=" + key;

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
        } //set active=0 to the record

        //e.g. GetDBSource("QE_USER", "select * from QE_USER where active=1 and USER_ID='123456'")
        public static List<object> GetDBSource(string table, string query) //query for single table
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

                    foreach(var property in properties)
                    {
                        if (requiredColumns.Contains(property.Name))
                            requiredProperties.Add(property);
                    }

                    while (rdr.Read())
                    {
                        var model = Activator.CreateInstance(Type.GetType("QE.Models." + table)); //create new instance for each db record read

                        foreach (var property in requiredProperties) //convert db record value base on property type
                        {
                            //if (requiredColumns.Contains(property.Name))
                            //{
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
                                else if (property.PropertyType == typeof(DateTime))
                                {
                                    if (!string.IsNullOrEmpty(rdr[property.Name].ToString()))
                                        property.SetValue(model, Convert.ToDateTime(rdr[property.Name]), null);
                                    else
                                        property.SetValue(model, Constant.DEF_DATETIME);
                                }
                            //}

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

        }


        //e.g. GetDBSource(new string[]{"QE_USER","STUDENT"}, "select * from QE_USER and STUDENT")
        public static List<object> GetDBSource(string[] tables, string query) //query for mutiple table
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
                        while (rdr.Read())
                        {
                            List<object> modelList = new List<object>();
                            foreach (var table in tables)
                            {

                                var model = Activator.CreateInstance(Type.GetType("QE.Models." + table));

                                PropertyInfo[] properties = Type.GetType("QE.Models." + table).GetProperties(); //get properties of the model
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

                            
                                foreach (var property in requiredProperties) //convert db record value base on property type
                                {
                                    if (property.PropertyType == typeof(string))
                                    {
                                        if (!string.IsNullOrEmpty(rdr[property.Name].ToString()))
                                            property.SetValue(model, rdr[property.Name].ToString(), null);
                                        else
                                            property.SetValue(model, string.Empty);
                                    }
                                    else if (property.PropertyType == typeof(int))
                                    {
                                        int a = 0;
                                        int.TryParse(rdr[property.Name].ToString(), out a);
                                        property.SetValue(model, a, null);
                                    }
                                    else if (property.PropertyType == typeof(bool))
                                    {
                                        property.SetValue(model, bool.Parse(rdr[property.Name].ToString()), null);
                                    }
                                    else if (property.PropertyType == typeof(DateTime))
                                    {
                                        if (!string.IsNullOrEmpty(rdr[property.Name].ToString()))
                                            property.SetValue(model, Convert.ToDateTime(rdr[property.Name]), null);
                                        else
                                            property.SetValue(model, new DateTime(1900, 1, 1));
                                    }
                                }
                                modelList.Add(model);

                            }
                            
                            list.Add(modelList);
                            
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

            
      
            
        }
    }
}
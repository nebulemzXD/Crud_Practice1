using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.UI;

namespace Crud_Practice1
{
    public class clsUtil
    {
        public static void toast(string Msg, string MsgType, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "sweetAlert", "toastMsg('" + Msg + "','" + MsgType + "'); ", true);
        }
        public static void alert(string Msg, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "alert('" + Msg + "'); ", true);
        }

        public static void alert2(string Msg, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "alert(`" + Msg + "`); ", true);
        }
        public static void callWaitScreen(Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "$('#waitscreen').modal({ backdrop: 'static' });", true);

        }
        public static void callModal(string modal_id, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "$('#" + modal_id + "').modal({ backdrop: 'static' });", true);

        }

        public static string replaceQuote(string tmpStr)
        {
            tmpStr = tmpStr.Replace("'", "''");
            return tmpStr;
        }
        /// <summary>
        /// Escapes quotes in string to prevent error in SQL Statements and returns Uppercase version of string.
        /// </summary>
        /// <param name="tmpStr"></param>
        /// <returns>formatted string</returns>
        public static string replaceQuoteU(string tmpStr)
        {
            tmpStr = tmpStr.Replace("'", "''");
            return tmpStr.ToUpper();
        }
        /// <summary>
        /// Checks whether the object is null
        /// </summary>
        /// <param name="str"></param>
        /// <returns>boolean</returns>
        public static bool isNull(object str)
        {
            if (str == DBNull.Value || str == null)
                return true;
            else
                return false;
        }
        public static object convertEmptyStringToNull(string str)
        {
            if (str == "")
                return Convert.DBNull;
            else
                return str;
        }
        public static string convertNullToEmptyString(object str)
        {
            if (str == DBNull.Value || str == null)
                return "";
            else
                return str.ToString().Trim();
        }
        /// <summary>
        /// Convert String to Date Format yyyy-MM-dd
        /// </summary>
        /// <param name="str"></param>
        /// <returns>formatted string</returns>
        public static string convertToDateFormat(string str)
        {
            try
            {
                return Convert.ToDateTime(str).ToString("yyyy-MM-dd");
            }
            catch (Exception x)
            {
                return "";
            }
        }

    }
    public static class toastType
    {
        public const string success = "success";
        public const string error = "error";
        public const string info = "info";
        public const string warning = "warning";
    }
}
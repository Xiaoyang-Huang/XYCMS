using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Tools {
    public class Entity {

        public static string GetTypeName(string inTypeString) {
            System.Data.DbType _type = (System.Data.DbType)Enum.Parse(typeof(System.Data.DbType), inTypeString.Split('|')[0].Split('.')[3]);
            switch (_type) {
                case System.Data.DbType.Binary:
                case System.Data.DbType.DateTime2:
                case System.Data.DbType.DateTimeOffset:
                case System.Data.DbType.Decimal:
                case System.Data.DbType.Time:
                    return _type.ToString() + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.AnsiString:
                    return "VarChar" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.AnsiStringFixedLength:
                    return "Char" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.String:
                    return "Text";
                case System.Data.DbType.VarNumeric:
                    return "Numeric" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.Boolean:
                    return "Bit";
                case System.Data.DbType.Byte:
                    return "TinyInt";
                case System.Data.DbType.Currency:
                    return "Money";
                case System.Data.DbType.Double:
                    return "Float";
                case System.Data.DbType.Guid:
                    return "Uniqueidentifier";
                case System.Data.DbType.Int16:
                case System.Data.DbType.UInt16:
                    return "SmallInt";
                case System.Data.DbType.Int32:
                case System.Data.DbType.UInt32:
                    return "Int";
                case System.Data.DbType.Int64:
                case System.Data.DbType.UInt64:
                    return "BigInt";
                case System.Data.DbType.Single:
                    return "Real";
                case System.Data.DbType.Object:
                case System.Data.DbType.SByte:
                case System.Data.DbType.StringFixedLength:
                    throw new ArgumentException("can not found a correspond SQL type.", "inTypeString");
                default:
                    return _type.ToString();
            }
        }
    }
}

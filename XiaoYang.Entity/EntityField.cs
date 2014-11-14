using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityField : Xy.Data.IDataModel {

        public EntityTable Table { get; internal set; }

        private string _sqlDeclare = string.Empty;
        public string SqlDeclare {
            get {
                if (string.IsNullOrEmpty(_sqlDeclare)) {
                    System.Data.DbType _type = (System.Data.DbType)Enum.Parse(typeof(System.Data.DbType), Type.Split('|')[0].Split('.')[3]);
                    switch (_type) {
                        case System.Data.DbType.Binary:
                        case System.Data.DbType.DateTime2:
                        case System.Data.DbType.DateTimeOffset:
                        case System.Data.DbType.Decimal:
                        case System.Data.DbType.Time:
                            _sqlDeclare = _type.ToString() + "(" + Type.Split('|')[1] + ")";
                            break;
                        case System.Data.DbType.AnsiString:
                            _sqlDeclare = "nvarchar" + "(" + Type.Split('|')[1] + ")";
                            break;
                        case System.Data.DbType.AnsiStringFixedLength:
                            _sqlDeclare = "char" + "(" + Type.Split('|')[1] + ")";
                            break;
                        case System.Data.DbType.String:
                            _sqlDeclare = "text";
                            break;
                        case System.Data.DbType.VarNumeric:
                            _sqlDeclare = "numeric" + "(" + Type.Split('|')[1] + ")";
                            break;
                        case System.Data.DbType.Boolean:
                            _sqlDeclare = "bit";
                            break;
                        case System.Data.DbType.Byte:
                            _sqlDeclare = "tinyInt";
                            break;
                        case System.Data.DbType.Currency:
                            _sqlDeclare = "money";
                            break;
                        case System.Data.DbType.Double:
                            _sqlDeclare = "float";
                            break;
                        case System.Data.DbType.Guid:
                            _sqlDeclare = "uniqueidentifier";
                            break;
                        case System.Data.DbType.Int16:
                        case System.Data.DbType.UInt16:
                            _sqlDeclare = "smallInt";
                            break;
                        case System.Data.DbType.Int32:
                        case System.Data.DbType.UInt32:
                            _sqlDeclare = "int";
                            break;
                        case System.Data.DbType.Int64:
                        case System.Data.DbType.UInt64:
                            _sqlDeclare = "bigInt";
                            break;
                        case System.Data.DbType.Single:
                            _sqlDeclare = "real";
                            break;
                        case System.Data.DbType.Object:
                        case System.Data.DbType.SByte:
                        case System.Data.DbType.StringFixedLength:
                            throw new Exception("can not found a correspond SQL type.");
                        default:
                            _sqlDeclare = _type.ToString();
                            break;
                    }
                }
                return _sqlDeclare;
            }
        }

        static void RegisterEvents() { }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public interface IHandler {
        void Init(long inTypeID);
        object Add(System.Collections.Specialized.NameValueCollection inForm);
        int Del(System.Collections.Specialized.NameValueCollection inForm);
        System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection inForm);
        int Update(System.Collections.Specialized.NameValueCollection inForm);
        System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection inForm);
        //System.Data.DataTable GetList(int inPageIndex, int inPageSize, string inWhere, string inOrder, out int inTotalCount);
    }

    public class DefaultHandler : IHandler {

        private Cache _cache;

        public void Init(long inTypeID) {
            _cache = CacheManager.Get(inTypeID);
        }

        public object Add(System.Collections.Specialized.NameValueCollection inForm) {
            StringBuilder _command = new StringBuilder();
            List<EntityField> _insertFields = new List<EntityField>();
            foreach (EntityField _field in _cache.FieldList) {
                if (_field.Foreign) {
                    _insertFields.Add(_field);
                } else {
                    foreach (string _key in inForm.Keys) {
                        if (string.Compare(_field.Key, _key) == 0) {
                            _insertFields.Add(_field);
                        }
                    }
                }
            }
            _command.Append("DECLARE @PrimaryKey ").Append(_cache.PrimaryField.SqlDeclare).AppendLine(";");
            foreach (EntityTable _table in _cache.TableList) {
                int _tempCommandStart = _command.Length;
                _command.Append("Insert into [").Append(_table.Key).Append("]").Append("(");
                int _parameterOffset = _command.Length;
                _command.AppendLine(")").Append("values(");
                int _parameterValueOffset = _command.Length;
                _command.AppendLine(");");
                int _tempCommandLength = _command.Length;
                bool _edited = false;
                int _tempOffset = 0;
                foreach (EntityField _field in _insertFields) {
                    if (_field.TableID == _table.ID) {
                        if (_field.Increase) {
                            continue;
                        }
                        _command.Insert(_parameterOffset, string.Concat(_edited ? "," : string.Empty, _field.Key));
                        _tempOffset = _command.Length - _tempCommandLength;
                        _parameterOffset += _tempOffset;
                        _parameterValueOffset += _tempOffset;
                        _tempCommandLength = _command.Length;
                        if (_field.Foreign) {
                            _command.Insert(_parameterValueOffset, string.Concat(_edited ? "," : string.Empty, "@PrimaryKey"));
                        } else {
                            _command.Insert(_parameterValueOffset, string.Concat(_edited ? "," : string.Empty, "'", inForm[_field.Key], "'"));
                        }
                        _tempOffset = _command.Length - _tempCommandLength;
                        _parameterValueOffset += _tempOffset;
                        _tempCommandLength = _command.Length;
                        _edited = true;
                    }
                }
                if (!_edited) {
                    _command.Remove(_tempCommandStart, _command.Length - _tempCommandStart);
                } else {
                    if (_table.Main) {
                        _command.AppendLine("set @PrimaryKey = SCOPE_IDENTITY();");
                    }
                }
            }
            Console.Write(_command.ToString());
            throw new NotImplementedException();
        }

        public int Del(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public int Update(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }


    }
}

using System;
using System.Data;
using System.Data.Common;

namespace AT.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class MappedDataReader : DbDataReader
    {
        private readonly DbDataReader _dataReader;
        private readonly MultipleResultMapper _multipleResultMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleResultMapper"></param>
        /// <param name="reader"></param>
        public MappedDataReader(MultipleResultMapper multipleResultMapper, DbDataReader reader)
        {
            _dataReader = reader;
            _multipleResultMapper = multipleResultMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Close()
        {
            _dataReader.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Depth
        {
            get { return _dataReader.Depth; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DataTable GetSchemaTable()
        {
            DataTable schemaTable = _dataReader.GetSchemaTable();
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                schemaTable.Rows[i][0] = _multipleResultMapper.GetName(i);
            }
            schemaTable.AcceptChanges();
            return schemaTable;

        }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsClosed
        {
            get { return _dataReader.IsClosed; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool NextResult()
        {
            return _dataReader.NextResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Read()
        {
            return _dataReader.Read();
        }

        /// <summary>
        /// 
        /// </summary>
        public override int RecordsAffected
        {
            get { return _dataReader.RecordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public new void Dispose()
        {
            _dataReader.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public override int FieldCount
        {
            get { return _dataReader.FieldCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override bool GetBoolean(int i)
        {
            return _dataReader.GetBoolean(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override byte GetByte(int i)
        {
            return _dataReader.GetByte(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="fieldOffset"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferoffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override char GetChar(int i)
        {
            return _dataReader.GetChar(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="fieldoffset"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferoffset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public new DbDataReader GetData(int i)
        {
            return _dataReader.GetData(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override string GetDataTypeName(int i)
        {
            return _dataReader.GetDataTypeName(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override DateTime GetDateTime(int i)
        {
            return _dataReader.GetDateTime(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override decimal GetDecimal(int i)
        {
            return _dataReader.GetDecimal(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override double GetDouble(int i)
        {
            return _dataReader.GetDouble(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override Type GetFieldType(int i)
        {
            return _dataReader.GetFieldType(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override float GetFloat(int i)
        {
            return _dataReader.GetFloat(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override Guid GetGuid(int i)
        {
            return _dataReader.GetGuid(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override short GetInt16(int i)
        {
            return _dataReader.GetInt16(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override int GetInt32(int i)
        {
            return _dataReader.GetInt32(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override long GetInt64(int i)
        {
            return _dataReader.GetInt64(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override string GetName(int i)
        {
            return _dataReader.GetName(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override int GetOrdinal(string name)
        {
            return _multipleResultMapper.GetOrdinal(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override string GetString(int i)
        {
            return _dataReader.GetString(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override object GetValue(int i)
        {
            return _dataReader.GetValue(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public override int GetValues(object[] values)
        {
            return _dataReader.GetValues(values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override bool IsDBNull(int i)
        {
            return _dataReader.IsDBNull(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override object this[string name]
        {
            get { return this[GetOrdinal(name)]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public override object this[int i]
        {
            get { return _dataReader[i]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _dataReader.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool HasRows
        {
            get { return _dataReader.HasRows; }
        }
    }
}